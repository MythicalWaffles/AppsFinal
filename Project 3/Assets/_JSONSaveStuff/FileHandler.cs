using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public class FileHandler
{ 

    public void SaveToJson<T>(List<T> tosave, string filename)
    {
        string content = "";
        WriteFile(GetPath(filename), content);
    }

    public void ReadFromJson()
    {

    }

    private string GetPath(string filename)
    {
        //return a location to put the file
        return Application.persistentDataPath + "/" + filename;
    }

    private void WriteFile(string path, string content)
    {
        //FileMode.Create creates a new file if one does not exist or overwrite it if it does exist
        Stream newStream = File.Open("data.json", FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();


        /*
         * CODE SNIPPET BELOW IS GOINNG TO BE USE TO SERIALIZE AN OBJECT LIKE AN
         * INTEGER OR STRING
         */

        //bf.Serialize(newStream, *something *);

        newStream.Close();

    }

    private string ReadFile()
    {
        return "";
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

//the purpose of this code is to allow the list to be propperly converted to JSON 
