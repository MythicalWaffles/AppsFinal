using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using static UnityEditor.Experimental.GraphView.GraphView;
using System;
using Unity.UI;
using TMPro;
using UnityEngine.UI;

public class SaveFunction : MonoBehaviour
{
    XmlDocument pleezy = new XmlDocument();
    XmlDocument eezy = new XmlDocument();
    XmlDocument geezy = new XmlDocument();

    public GameObject Player;
    public GameObject[] Enemies;

    List<string> nodenames = new List<string>();


    List<string> playersaves = new List<string>();
    string playerInput;
    bool error;
    public GameObject saveErrorMenu;
    string directory = "DataFolder";

    public InputField inputfield;


    public void FileName()
    {
        
    }
    public void setplayerInput()
    {
        Debug.Log("We reached the setplayerInput");
        playerInput = inputfield.text;
        checkList();
        Debug.Log(playerInput);
    }

    void checkList() {
        for (int i = 0; i < playersaves.Count; i++)
        {
            if (playersaves[i] == playerInput)
            {
                error = true;
  
            } else if (playersaves[i] != playerInput)
            {
                Debug.Log("We reached the else if statement");
                error = false;
                add2List();
            }
        }
    }

    void add2List()
    {
        if (error == false)
        {
            Debug.Log("We reached the add2List Part");
            playersaves.Add(playerInput);
            SaveP();
        }
    }

    void SaveP()
    {
        //string[] nodes = { "name", "xPos", "yPos", "zPos", "xRot",
        //    "yRot", "zRot", "xScale", "yScale", "zScale" };

        if (Player != null)
        {
            XmlNode root = pleezy.FirstChild;
            foreach (XmlNode node in root.ChildNodes)
            {
                switch (node.Name)
                {
                    case "xPos":
                        node.InnerText = Player.transform.position.x.ToString();
                        break;
                    case "yPos":
                        node.InnerText = Player.transform.position.y.ToString();
                        break;
                    case "zPos":
                        node.InnerText = Player.transform.position.z.ToString();
                        break;
                    case "xRot":
                        node.InnerText = Player.transform.rotation.x.ToString();
                        break;
                    case "yRot":
                        node.InnerText = Player.transform.rotation.y.ToString();
                        break;
                    case "zRot":
                        node.InnerText = Player.transform.rotation.z.ToString();
                        break;
                    case "xScale":
                        node.InnerText = Player.transform.localScale.x.ToString();
                        break;
                    case "yScale":
                        node.InnerText = Player.transform.localScale.y.ToString();
                        break;
                    case "zScale":
                        node.InnerText = Player.transform.localScale.z.ToString();
                        break;
                }
            }
            Debug.Log("We reached the save part");
            pleezy.Save(directory + @"\" + "data "+ playerInput);
        }
    }

    void writeFileProcess()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        pleezy.LoadXml(File.ReadAllText(directory + @"\" + "xTPlayerData.xml"));
        playersaves.Add("SystemCoreSave");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
