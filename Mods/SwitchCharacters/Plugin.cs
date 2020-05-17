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
            ModdingTales.ModdingUtils.Initialize(this);
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
    }
}
