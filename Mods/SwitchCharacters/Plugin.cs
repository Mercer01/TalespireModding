using System;
using BepInEx;
using Bounce.Unmanaged;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SwitchCharacters
{
    [BepInPlugin("org.Mercer.plugins.SwitchCharacters", "Switch Characters", "1.0.0")]
    [BepInProcess("TaleSpire.exe")]
    class SwitchCharacters : BaseUnityPlugin
    {

        void Awake()
        {
            UnityEngine.Debug.Log("SwitchCharacters Plugin loaded");
        }

        private void Update()
        {
            NGuid[] creatureIds;
            if(Input.GetKeyUp(KeyCode.LeftBracket) && BoardSessionManager.Board.TryGetPlayerOwnedCreatureIds(LocalPlayer.Id.Value, out creatureIds))
            {
                var i = 0;
                while (i < creatureIds.Length)
                {
                    if (creatureIds[i] == LocalClient.SelectedCreatureId)
                    {
                        if (i + 1 < creatureIds.Length)
                        {
                            LocalClient.SelectedCreatureId = creatureIds[i + 1];
                            break;
                        }
                        LocalClient.SelectedCreatureId = creatureIds[0];
                        break;
                    }
                    else
                    {
                        i++;
                    }
                }
                CameraController.LookAtCreature(LocalClient.SelectedCreatureId);
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
