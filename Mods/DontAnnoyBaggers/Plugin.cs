using BepInEx;
using UnityEngine;

namespace DontAnnoyBaggers
{
    [BepInPlugin("org.generic.plugins.setInjectionFlag", "Set Injection Flag Plugin", "1.0.0.0")]
    [BepInProcess("TaleSpire.exe")]
    class SetInjectionFlag : BaseUnityPlugin
    {
        void Awake()
        {
            UnityEngine.Debug.Log("SetInjectionFlag Plug-in loaded");
            AppStateManager.UsingCodeInjection = true;
        }
    }
}