/* *
 * ===============================================
 * author      : Josh@win
 * e-mail      : shijun_z@163.com
 * create time : 2026年2月28 16:32
 * function    : 
 * ===============================================
 * */
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using App.Core.Helper;
using App.Core.Master;
using App.Core.Tools;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace App.Modules
{
    [LogicOf("BuildModules", AssetPath.Global)]
    public class BuildModulesLogic : EventBase, ILogic
    {
        private BuildModulesView View => ViewMaster.Instance.GetView<BuildModulesView>();
        private readonly Encoding UTF8NoBOM = new UTF8Encoding(false);
        private readonly string SavePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private string GraphqlUrl => "https://live-platform-api.prd.ld.unity3d.com/graphql";
        private string FetchReleaseQuery;
        private JArray Modules = new();
        public BuildModulesLogic()
        {
	        AddEventMsg<object>("OpenBuildModulesView", OpenBuildModulesView);
	        AddEventMsg("CloseBuildModulesView", CloseBuildModulesView);
			AddEventMsg<string>("EditorVersionTMP_InputFieldEvent", (arg)=>{ });
			AddEventMsg<string>("EditorVersionTMP_InputFieldSubmitEvent", (arg)=>{ });
			AddEventMsg<string>("EditorVersionTMP_InputFieldSelectEvent", (arg)=>{ });
			AddEventMsg<string>("EditorVersionTMP_InputFieldDeselectEvent", (arg)=>{ });
			AddEventMsg<int>("PlatformTMP_DropdownEvent", (arg)=>{ });
			AddEventMsg<int>("ArchitectureTMP_DropdownEvent", (arg)=>{ });
			AddEventMsg("GetModulesButtonEvent", OnGetModulesEvent);
			AddEventMsg("SaveModulesButtonEvent", OnSaveModulesEvent);

        }
        
        #region Life Cycle
        
        public void Begin()
        {
            
        }
        public void End()
        {
            
        }
        
        public void AppPause(bool pause)
        {
            
        }
        public void AppFocus(bool focus)
        {
            
        }
        public void AppQuit()
        {
            
        }
        
        #endregion

        #region Logic
        
        private void OnGetModulesEvent()
        {
            if (string.IsNullOrEmpty(View.EditorVersionTMP_InputField.text))
            {
                ViewMaster.Instance.OpenView<AskView>(new AskData()
                {
                    connect = "请输入Unity Editor版本号",
                    mold = AskMold.Toast,
                });
                return;
            }
            var pastData = new JObject()
            {
                { "query", FetchReleaseQuery },
                {
                    "variables", new JObject()
                    {
                        { "architecture", new JArray() { View.ArchitectureTMP_Dropdown.captionText.text } },
                        { "platform", new JArray() { View.PlatformTMP_Dropdown.captionText.text } },
                        { "stream", new JArray() { "LTS" } },
                        { "entitlements", new JArray() { "XLTS" } },
                        { "version", View.EditorVersionTMP_InputField.text },
                    }
                },
            };
            var uwr = HttpsMaster.Uwr;
            uwr.AddHeader("Content-Type", "application/json");
            uwr.Post(GraphqlUrl, pastData.ToString(), result =>
            {
                Modules = new JArray();
                View.ModulesTextTextMeshProUGUI.text = result;
                var response = JToken.Parse(result);
                var modules = response["data"]?["getUnityReleases"]?["edges"]?[0]?["node"]?["downloads"]?[0]?["modules"];
                if (modules == null)
                {
                    return;
                }
                foreach (var module in modules)
                {
                    FeatchModulesFromRelease(Modules, module);
                }
            });
        }
        private void FeatchModulesFromRelease(JArray modules, JToken modulesOnline, string _pid = "")
        {
            modules.Add(JObject.FromObject(new Module(modulesOnline, _pid)));

            var subModules = modulesOnline["subModules"];
            if (subModules == null || !subModules.HasValues)
            {
                return;
            }

            var pid = modulesOnline["id"]?.ToString();
            foreach (var item in subModules)
            {
                FeatchModulesFromRelease(modules, item, pid);
            }
        }
        private void OnSaveModulesEvent()
        {
            if (Modules == null || !Modules.HasValues)
            {
                ViewMaster.Instance.OpenView<AskView>(new AskData()
                {
                    connect = "请先获取模块",
                    mold = AskMold.Toast,
                });
                return;
            }
            if (!Directory.Exists($"{SavePath}/{View.EditorVersionTMP_InputField.text}"))
            {
                Directory.CreateDirectory($"{SavePath}/{View.EditorVersionTMP_InputField.text}");
            }
            var filePath = $"{SavePath}/{View.EditorVersionTMP_InputField.text}/modules.json";
            File.WriteAllText(filePath, Modules.ToString(), UTF8NoBOM);

            ViewMaster.Instance.OpenView<AskView>(new AskData()
            {
                connect = "保存成功",
                mold = AskMold.Toast,
            });
        }
        
        #endregion

        #region View Logic
        
        private void OpenBuildModulesView(object obj)
        {
            FetchReleaseQuery = Resources.Load<TextAsset>("fetch_release_query").text;
        }
        private void CloseBuildModulesView()
        {
	        
        }
        
        #endregion
    }
}