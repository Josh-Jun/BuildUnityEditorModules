using System;
using System.Collections;
using System.Collections.Generic;
using App.Core.Helper;
using App.Core.Master;
using App.Core.Tools;
using UnityEngine;
using UnityEngine.UI;
using SuperScrollView;
using XCharts.Runtime;
using TMPro;

namespace App.Modules
{
    [ViewOf("BuildModules", ViewMold.UI2D, AssetPath.BuildModulesView, true, 0)]
    public class BuildModulesView : ViewBase
    {
		public RectTransform EditorVersionRectTransform;
		public ImagePro EditorVersionImagePro;
		public TMP_InputField EditorVersionTMP_InputField;
		public RectTransform PlatformRectTransform;
		public ImagePro PlatformImagePro;
		public TMP_Dropdown PlatformTMP_Dropdown;
		public RectTransform ArchitectureRectTransform;
		public ImagePro ArchitectureImagePro;
		public TMP_Dropdown ArchitectureTMP_Dropdown;
		public RectTransform GetModulesRectTransform;
		public ImagePro GetModulesImagePro;
		public Button GetModulesButton;
		public RectTransform ModulesRectTransform;
		public ImagePro ModulesImagePro;
		public ScrollRect ModulesScrollRect;
		public RectTransform SaveModulesRectTransform;
		public ImagePro SaveModulesImagePro;
		public Button SaveModulesButton;
		public RectTransform ModulesTextRectTransform;
		public TextMeshProUGUI ModulesTextTextMeshProUGUI;

        protected override void InitView()
        {
            base.InitView();
			EditorVersionRectTransform = this.FindComponent<RectTransform>("Panel/LV_EditorVersion");
			EditorVersionImagePro = this.FindComponent<ImagePro>("Panel/LV_EditorVersion");
			EditorVersionTMP_InputField = this.FindComponent<TMP_InputField>("Panel/LV_EditorVersion");
			PlatformRectTransform = this.FindComponent<RectTransform>("Panel/LV_Platform");
			PlatformImagePro = this.FindComponent<ImagePro>("Panel/LV_Platform");
			PlatformTMP_Dropdown = this.FindComponent<TMP_Dropdown>("Panel/LV_Platform");
			ArchitectureRectTransform = this.FindComponent<RectTransform>("Panel/LV_Architecture");
			ArchitectureImagePro = this.FindComponent<ImagePro>("Panel/LV_Architecture");
			ArchitectureTMP_Dropdown = this.FindComponent<TMP_Dropdown>("Panel/LV_Architecture");
			GetModulesRectTransform = this.FindComponent<RectTransform>("Panel/LV_GetModules");
			GetModulesImagePro = this.FindComponent<ImagePro>("Panel/LV_GetModules");
			GetModulesButton = this.FindComponent<Button>("Panel/LV_GetModules");
			ModulesRectTransform = this.FindComponent<RectTransform>("Panel/LV_Modules");
			ModulesImagePro = this.FindComponent<ImagePro>("Panel/LV_Modules");
			ModulesScrollRect = this.FindComponent<ScrollRect>("Panel/LV_Modules");
			SaveModulesRectTransform = this.FindComponent<RectTransform>("Panel/LV_Modules/LV_SaveModules");
			SaveModulesImagePro = this.FindComponent<ImagePro>("Panel/LV_Modules/LV_SaveModules");
			SaveModulesButton = this.FindComponent<Button>("Panel/LV_Modules/LV_SaveModules");
			ModulesTextRectTransform = this.FindComponent<RectTransform>("Panel/LV_Modules/Viewport/Content/LV_ModulesText");
			ModulesTextTextMeshProUGUI = this.FindComponent<TextMeshProUGUI>("Panel/LV_Modules/Viewport/Content/LV_ModulesText");

        }

        protected override void RegisterEvent()
        {
            base.RegisterEvent();
			EditorVersionTMP_InputField.onValueChanged.AddListener((arg) => { SendEventMsg("EditorVersionTMP_InputFieldEvent", arg); });
			EditorVersionTMP_InputField.onSubmit.AddListener((arg) => { SendEventMsg("EditorVersionTMP_InputFieldSubmitEvent", arg); });
			EditorVersionTMP_InputField.onSelect.AddListener((arg) => { SendEventMsg("EditorVersionTMP_InputFieldSelectEvent", arg); });
			EditorVersionTMP_InputField.onDeselect.AddListener((arg) => { SendEventMsg("EditorVersionTMP_InputFieldDeselectEvent", arg); });
			PlatformTMP_Dropdown.onValueChanged.AddListener((arg) => { SendEventMsg("PlatformTMP_DropdownEvent", arg); });
			ArchitectureTMP_Dropdown.onValueChanged.AddListener((arg) => { SendEventMsg("ArchitectureTMP_DropdownEvent", arg); });
			GetModulesButton.onClick.AddListener(() => { SendEventMsg("GetModulesButtonEvent"); });
			SaveModulesButton.onClick.AddListener(() => { SendEventMsg("SaveModulesButtonEvent"); });

        }
    }
}
