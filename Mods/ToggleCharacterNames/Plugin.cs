using BepInEx;
using UnityEngine;


namespace KeybindingAtmosphereControls
{
    [BepInPlugin("org.mercer.plugins.ToggleCharacterNames", "Toggle Character Names", "1.2.0")]
    [BepInProcess("TaleSpire.exe")]
    public class Plugin : BaseUnityPlugin
    {
        private bool _enabled = false;
        void Awake()
        {
            Debug.Log("Starting Toggle Characters Plugin");
            ModdingTales.ModdingUtils.Initialize(this);
        }

        private void ToggleTextLayer()
        {
            this._enabled = !_enabled;
        }

        void Update()
        {
            if (this._enabled)
            {
                // Force it to always be displayed if the user enables the plugin, otherwise ignore it.
                ToolIndicatorUI.TextLayerEnabled = _enabled;
            }
            if (Input.GetKeyUp(KeyCode.F4))
            {
                ToggleTextLayer();
            }
        }
    }
}
