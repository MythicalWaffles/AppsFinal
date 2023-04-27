using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadBehaviors: MonoBehaviour
{

    bool loadready;


    public bool Load
    {
        get { return loadready; }
        set { loadready = value; }
    }


    public void SaveToJson()
    {
        Load = true;
        Debug.Log("I have arrived");
        WriteFile(Load);
    }

    private void WriteFile(bool load)
    {
        Debug.Log("bye bye");
        //FileMode.Create creates a new file if one does not exist or overwrite it if it does exist
        Stream newStream = File.Open("Load.json", FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();
        /*
         * CODE SNIPPET BELOW IS GOINNG TO BE USE TO SERIALIZE AN OBJECT LIKE AN
         * INTEGER OR STRING
         */
        bf.Serialize(newStream, load);

        newStream.Close();

        SwitchScene();
    }

    public void SwitchScene()
    {
        SceneManager.LoadScene(sceneName: "Level_1");
    }

    public void LoadNormalGame()
    {
        bool load = false;
        Debug.Log("bye bye");
        //FileMode.Create creates a new file if one does not exist or overwrite it if it does exist
        Stream newStream = File.Open("Load.json", FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();
        /*
         * CODE SNIPPET BELOW IS GOINNG TO BE USE TO SERIALIZE AN OBJECT LIKE AN
         * INTEGER OR STRING
         */
        bf.Serialize(newStream, load);

        newStream.Close();

        SwitchScene();
    }
}
