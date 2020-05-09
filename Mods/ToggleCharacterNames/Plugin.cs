using System;
using BepInEx;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ToggleCharacterNames
{
    [BepInPlugin("org.mercer.plugins.ToggleCharacterNames", "Toggle Character Names", "1.1.0")]
    [BepInProcess("TaleSpire.exe")]
    public class Plugin : BaseUnityPlugin
    {
        private bool _enabled = false;
        void Awake()
        {
            Debug.Log("Starting Toggle Characters Plugin");
        }

        private void ToggleTextLayer()
        {
            ToolIndicatorUI.TextLayerEnabled = !_enabled;
            this._enabled = !_enabled;
        }

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.F4))
            {
                ToggleTextLayer();
            }
        }

        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            UnityEngine.Debug.Log("Loading Scene: " + scene.name);
            TextMeshProUGUI[] texts = FindObjectsOfType<TextMeshProUGUI>();

            for (int i = 0; i < texts.Length; i++)
            {
                if (scene.name == "UI" && texts[i].name == "BETA")
                {
                    texts[i].text = "INJECTED BUILD - unstable mods";
                }
                if (scene.name == "Login" && texts[i].name == "TextMeshPro Text")
                {
                    BepInPlugin bepInPlugin = (BepInPlugin)Attribute.GetCustomAttribute(this.GetType(), typeof(BepInPlugin));
                    if (texts[i].text.EndsWith("</size>"))
                    {
                        texts[i].text += "\n\nMods Currently Installed:\n";
                    }
                    texts[i].text += "\n" + bepInPlugin.Name + " - " + bepInPlugin.Version;
                }
            }
        }
    }
}
