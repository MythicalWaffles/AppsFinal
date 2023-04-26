using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using TMPro;

public class FileHandler
{
    ItemClass[] mainInv;
    ItemClass[] quickInv;



    public void SaveToJson(LinkedList<ItemClass> listMain, LinkedList<ItemClass> listSecond)
    {
        Debug.Log("I have arrived");
        ItemClass[] savearray = new ItemClass[14];
        ItemClass[] saveQarray = new ItemClass[4];
        listMain.CopyTo(savearray, 0);
        listSecond.CopyTo(saveQarray, 0);
        WriteFile(savearray, saveQarray);
    }

    public void ReadFromJson()
    {
        Stream newStream = File.OpenRead("Inventory.json");
        BinaryFormatter bf = new BinaryFormatter();

        mainInv = (ItemClass[])(bf.Deserialize(newStream));
        quickInv = (ItemClass[])(bf.Deserialize(newStream));

    }

    private string GetPath(string filename)
    {
        Debug.Log("hello there");
        //return a location to put the file
        return Application.persistentDataPath + "/" + filename;
    }

    private void WriteFile(ItemClass[] content, ItemClass[] quickcontent)
    {
        Debug.Log("bye bye");
        //FileMode.Create creates a new file if one does not exist or overwrite it if it does exist
        Stream newStream = File.Open("Inventory.json", FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();
        /*
         * CODE SNIPPET BELOW IS GOINNG TO BE USE TO SERIALIZE AN OBJECT LIKE AN
         * INTEGER OR STRING
         */
        bf.Serialize(newStream, content);
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
