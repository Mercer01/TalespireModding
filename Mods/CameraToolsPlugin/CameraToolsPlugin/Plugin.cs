using System;
using System.Reflection;
using BepInEx;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace CameraToolsPlugin
{
    [BepInPlugin("org.mercer.plugins.CameraTools", "Camera Tools", "1.2.0")]
    [BepInProcess("TaleSpire.exe")]
    public class CameraTools : BaseUnityPlugin
    {

        private bool _enabled = false;
        private bool _orthEnabled = false;

        const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic |
                                   BindingFlags.Static;

        // Awake is called once when both the game and the plug-in are loaded
        void Awake()
        {
            UnityEngine.Debug.Log("Camera Tool loaded");
            ModdingTales.ModdingUtils.Initialize(this);
        }

        private void ToggleCamera()
        {
            try
            {
                if (!_enabled)
                {
                    UnityEngine.Debug.Log("The Cam overrides are disabled enabling!");

                    if (!CameraController.HasInstance) return;

                    var cam = CameraController.Instance;
                    UnityEngine.Debug.Log("CAM: " + cam);

                    cam.GetType().GetField("_minTilt", flags)?.SetValue(cam, -100);
                    cam.GetType().GetField("_maxTilt", flags)?.SetValue(cam, 120.0f);
                    //cam.GetType().GetField("_maxFov", flags).SetValue(cam, 100f);
                    this._enabled = true;
                }
                else
                {
                    UnityEngine.Debug.Log("The Cam overrides are enabled disabling!");
                    Camera.main.orthographic = false;
                    Camera.main.orthographicSize = 5f;

                    if (!CameraController.HasInstance) return;

                    var cam = CameraController.Instance;
                    UnityEngine.Debug.Log("CAM: " + cam);

                    cam.GetType().GetField("_minTilt", flags)?.SetValue(cam, -10);
                    cam.GetType().GetField("_maxTilt", flags)?.SetValue(cam, 40);
                    //cam.GetType().GetField("_maxFov", flags).SetValue(cam, 30);
                    this._enabled = false;

                }
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError("handled crash");
                UnityEngine.Debug.LogError(ex.Message);
                UnityEngine.Debug.LogError(ex.StackTrace);
                UnityEngine.Debug.LogError(ex.InnerException);
                UnityEngine.Debug.LogError(ex.Source);
            }
        }

        private void ToggleOrthographicCamera()
        {
            try
            {
                if (!_orthEnabled)
                {
                    Debug.Log("The Orthographic overrides are disabled enabling!");

                    Camera.main.orthographic = true;
                    Camera.main.orthographicSize = 50.0f;

                    if (CameraController.HasInstance)
                    {

                        var cam = CameraController.Instance;
                        cam.GetType().GetField("_minTilt", flags)?.SetValue(cam, 90.0f);
                        cam.GetType().GetField("_maxTilt", flags)?.SetValue(cam, 90.0f);
                        //cam.GetType().GetField("_maxFov", flags).SetValue(cam, 120.0f);
                        this._orthEnabled = true;
                    }

                    if (SingletonBehaviour<AtmosphereManager>.HasInstance)
                    {
                        var atm = SingletonBehaviour<AtmosphereManager>.Instance;
                        var fields = atm.GetType().GetFields(flags);
                        AtmosphereApplier aa;
                        foreach (var field in fields)
                        {
                            if (field.Name == "_applier")
                            {
                                aa = (AtmosphereApplier) field.GetValue(atm);
                                //UnityEngine.Debug.Log("Aperture Min: " + t.FieldType.GetField("_dofApertureMin", flags).GetValue(aa));
                                //UnityEngine.Debug.Log("Aperture Max: " + t.FieldType.GetField("_dofApertureMax", flags).GetValue(aa));
                                field.FieldType.GetField("_dofApertureMin", flags)?.SetValue(aa, 0.0f);
                                field.FieldType.GetField("_dofApertureMax", flags)?.SetValue(aa, 1000.0f);
                            }
                        }

                    }
                }
                else
                {
                    UnityEngine.Debug.Log("The Cam overrides are enabled disabling!");
                    Camera.main.orthographic = false;
                    Camera.main.orthographicSize = 5f;

                    if (CameraController.HasInstance)
                    {
                        var cam = CameraController.Instance;
                        UnityEngine.Debug.Log("CAM: " + cam);


                        cam.GetType().GetField("_minTilt", flags)?.SetValue(cam, -10);
                        cam.GetType().GetField("_maxTilt", flags)?.SetValue(cam, 40);
                        cam.GetType().GetField("_maxFov", flags)?.SetValue(cam, 30);
                        this._orthEnabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError("handled crash");
                UnityEngine.Debug.LogError(ex.Message);
                UnityEngine.Debug.LogError(ex.StackTrace);
                UnityEngine.Debug.LogError(ex.InnerException);
                UnityEngine.Debug.LogError(ex.Source);
            }
        }


        void Update()
        {
            if (Input.GetKeyUp(KeyCode.F5))
            {
                ToggleCamera();
            }

            if (Input.GetKeyUp(KeyCode.F6))
            {
                ToggleOrthographicCamera();
            }

            if (_orthEnabled)
            {
                if (Input.GetAxis("Mouse ScrollWheel") > 0)
                {
                    Camera.main.orthographicSize -= 1f;
                }
                else if (Input.GetAxis("Mouse ScrollWheel") < 0)
                {
                    Camera.main.orthographicSize += 1f;

                }
            }
        }
    }
}