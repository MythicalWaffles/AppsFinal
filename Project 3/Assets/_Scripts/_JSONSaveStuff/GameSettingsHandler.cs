using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using TMPro;
using System.Runtime.InteropServices.ComTypes;


[Serializable]
public class GameSettingsHandler
{
    int shadows;
    int resolution;
    int windowRes;
    float fov;
    int fullscreen;

    int[] basesettings = new int[] { 0, 2, 90, 0, 0 };

    public int Shadows
    {
        get { return shadows; }   // get method
        set { shadows = value; }  // set method
    }

    public int Resolution
    {
        get { return resolution; }   // get method
        set { resolution = value; }  // set method
    }
    public int WindowRes
    {
        get { return windowRes; }   // get method
        set { windowRes = value; }  // set method
    }
    public float Fov
    {
        get { return fov; }   // get method
        set { fov = value; }  // set method
    }
    public int Fullscreen
    {
        get { return fullscreen; }   // get method
        set { fullscreen = value; }  // set method
    }

    public int[] ReturnBase()
    {
        return basesettings;
    }

    public void SavetoJson(int shadows, int resolution, int windowres, float fov, int fullscreen)
    {
        Debug.Log("bye bye");
        //FileMode.Create creates a new file if one does not exist or overwrite it if it does exist
        Stream newStream = File.Open("Gamesettings.json", FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();

        bf.Serialize(newStream, shadows);
        bf.Serialize(newStream, resolution);
        bf.Serialize(newStream, windowres);
        bf.Serialize(newStream, fov);
        bf.Serialize(newStream, fullscreen);

        newStream.Close();
    }

    public void LoadfromJson()
    {
        Debug.Log("Hello");
        Stream newStream = File.OpenRead("Gamesettings.json");
        BinaryFormatter bf = new BinaryFormatter();

        shadows = (int)(bf.Deserialize(newStream));
        resolution = (int)(bf.Deserialize(newStream));
        windowRes = (int)(bf.Deserialize(newStream));
        fov = (float)(bf.Deserialize(newStream));
        fullscreen = (int)(bf.Deserialize(newStream));

        newStream.Close();
    }
}
