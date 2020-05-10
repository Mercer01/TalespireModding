using BepInEx;
using System;
using TMPro;
using UnityEngine.SceneManagement;

namespace DontAnnoyBaggers
{
    [BepInPlugin("org.generic.plugins.setInjectionFlag", "Set Injection Flag", "1.0.0")]
    [BepInProcess("TaleSpire.exe")]
    class SetInjectionFlag : BaseUnityPlugin
    {
        void Awake()
        {
            UnityEngine.Debug.Log("SetInjectionFlag Plug-in loaded");
            AppStateManager.UsingCodeInjection = true;
        }

        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            UnityEngine.Debug.Log("Loading Scene: " + scene.name);
            TextMeshProUGUI[] texts = FindObjectsOfType<TextMeshProUGUI>();

            foreach (var t in texts)
            {
                if (scene.name == "UI" && t.name == "BETA")
                {
                    t.text = "INJECTED BUILD - unstable mods";
                }
                if (scene.name == "Login" && t.name == "TextMeshPro Text")
                {
                    BepInPlugin bepInPlugin = (BepInPlugin)Attribute.GetCustomAttribute(this.GetType(), typeof(BepInPlugin));
                    if (t.text.EndsWith("</size>"))
                    {
                        t.text += "\n\nMods Currently Installed:\n";
                    }
                    t.text += "\n" + bepInPlugin.Name + " - " + bepInPlugin.Version;
                }
            }
        }
    }
}