using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using TMPro;

public class FileHandler
{
    public void SaveToJson(LinkedList<ItemClass> list, string filename)
    {
        Debug.Log("I have arrived");
        string content = "";
        ItemClass[] savearray = new ItemClass[14];
        list.CopyTo(savearray, 0);
        WriteFile(GetPath(filename), savearray);
    }

    public void ReadFromJson()
    {

    }

    private string GetPath(string filename)
    {
        Debug.Log("hello there");
        //return a location to put the file
        return Application.persistentDataPath + "/" + filename;
    }

    private void WriteFile(string path, ItemClass[] content)
    {
        Debug.Log("bye bye");
        //FileMode.Create creates a new file if one does not exist or overwrite it if it does exist
        Stream newStream = File.Open("data.json", FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();


        /*
         * CODE SNIPPET BELOW IS GOINNG TO BE USE TO SERIALIZE AN OBJECT LIKE AN
         * INTEGER OR STRING
         */

        bf.Serialize(newStream, content);

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

    private void Awake()
    {
       
    }
}


//the purpose of this code is to allow the list to be propperly converted to JSON 
