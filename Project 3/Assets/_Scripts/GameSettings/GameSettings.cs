using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine.UI;

public class GameSettings: MonoBehaviour
{
    GameSettingsHandler gameH = new GameSettingsHandler();
    public Slider slide;

    int[] basesettings = new int[] { 0, 2, 90, 0, 0 };

    public void ToggleShadows(int newToggle)
    {
        gameH.Shadows = newToggle;
        Light[] lights = GameObject.FindObjectsOfType<Light>();
        foreach (Light light in lights)
        {
            Debug.Log("Hello");
            if (gameH.Shadows == 0)
                light.shadows = LightShadows.None;
            else if (gameH.Shadows == 1)
                light.shadows = LightShadows.Hard;
            else if (gameH.Shadows == 2)
                light.shadows = LightShadows.Soft;
        }
    }
    public void Resolution(int r)
    {
        gameH.Resolution = r;
        switch (gameH.Resolution)
        {
            case 0:
                Screen.SetResolution(1920, 1080, true);
                break;
            case 1:
                Screen.SetResolution(1600, 900, true);
                break;
            case 2:
                Screen.SetResolution(1280, 1024, true);
                break;
        }
    }

    public void ChangeFov()
    {
        gameH.Fov = slide.value;
        Camera.main.fieldOfView = gameH.Fov;
    }

    public void FullScreen()
    {

    }

    public void SaveGameSetting()
    {
        gameH.SavetoJson(gameH.Shadows, gameH.Resolution, gameH.WindowRes, gameH.Fov, gameH.Fullscreen);
    }
    // Start is called before the first frame update
    void Start()
    {
      

    }

    // Update is called once per frame
    void Update()
    {
        ChangeFov();
    }
}
