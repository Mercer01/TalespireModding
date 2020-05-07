using BepInEx;
using UnityEngine;

namespace ToggleCharacterNames
{
    [BepInPlugin("org.mercer.plugins.ToggleCharacterNames", "Toggle Character Names", "1.0.0")]
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
            if (Input.GetKeyUp(KeyCode.F7))
            {
                ToggleTextLayer();
            }
        }
    }
}
