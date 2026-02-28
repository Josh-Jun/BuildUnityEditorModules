/* *
 * ===============================================
 * author      : Josh@win
 * e-mail      : shijun_z@163.com
 * create time : 2026年2月28 16:37
 * function    : 
 * ===============================================
 * */
using System.Linq;
using Newtonsoft.Json.Linq;

namespace App.Modules
{
    public class Module
    {
        public string url = "";
        public string integrity = "";
        public string type = "";
        public string id = "";
        public string name = "";
        public string slug = "";
        public string description = "";
        public string category = "";

        public long downloadSize = 0; //
        public long installedSize = 0; //

        public bool required = false;
        public bool hidden = false;
        public JObject extractedPathRename = null;
        public bool preSelected = false;
        public string destination = null;
        public JArray eula = null;
        public JArray subModules = new JArray();

        public string downloadUrl = "";
        public bool visible = true;
        public bool selected = false; //is_installed
        public string sync = "";
        public string parent = "";
        public string eulaUrl1 = "";
        public string eulaLabel1 = "";
        public string eulaMessage = "";
        public string renameTo = "";
        public string renameFrom = "";
        public bool preselected = false;

        public Module(JToken _module, string parent_id = "")
        {
            url = _module["url"]?.ToString();
            integrity = _module["integrity"]?.ToString();
            type = _module["type"]?.ToString();
            id = _module["id"]?.ToString();
            if (id == "android")
            {
                selected = true; //默认已安装安卓支持，方便直接使用
            }

            name = _module["name"]?.ToString();
            slug = _module["slug"]?.ToString();
            description = _module["description"]?.ToString();
            category = _module["category"]?.ToString();

            downloadSize = _module["downloadSize"].Value<long>("value");
            installedSize = _module["installedSize"].Value<long>("value");

            required = _module.Value<bool>("required");
            hidden = _module.Value<bool>("hidden");

            var epr = _module["extractedPathRename"]?.ToObject<JObject>();
            if (epr != null && epr.HasValues)
            {
                extractedPathRename = JObject.FromObject(epr);
            }

            preSelected = _module.Value<bool>("preSelected");
            destination = _module.Value<string>("destination");
            var eu = _module["eula"]?.ToObject<JArray>();
            if (eu != null && eu.HasValues)
            {
                eula = JArray.FromObject(eu);
            }

            subModules = JArray.FromObject(_module["subModules"]);

            downloadUrl = url;
            visible = !hidden;

            if (parent_id == "android-sdk-ndk-tools") //暂定，未找到对应的定义和赋值
            {
                sync = parent_id; //
            }

            parent = parent_id; //

            if (eula != null && eula.Count() > 0)
            {
                var eula1 = eula[0];
                eulaUrl1 = eula1["url"]?.ToString();
                eulaLabel1 = eula1["label"]?.ToString();
                eulaMessage = eula1["message"]?.ToString();
            }

            if (extractedPathRename != null && extractedPathRename.HasValues)
            {
                renameTo = extractedPathRename["to"]?.ToString();
                renameFrom = extractedPathRename["from"]?.ToString();
            }

            preselected = preSelected;
        }
    }
}
