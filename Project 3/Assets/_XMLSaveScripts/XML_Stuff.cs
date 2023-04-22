using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using static UnityEditor.Experimental.GraphView.GraphView;
using System;

public class XML_Stuff : MonoBehaviour
{
    XmlDocument pleezy = new XmlDocument();
    XmlDocument eezy = new XmlDocument();
    XmlDocument geezy = new XmlDocument();

    string Gamefile;

    string directory;

    public GameObject Player;
    public GameObject[] Enemies;
    public float fov;
    public string shadows;


   

    List<string> nodeNames = new List<string>();


    void SaveP()
    {
        if(Player != null)
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
            pleezy.Save(directory + @"\" + "data" + Gamefile);
        }
    }

    void LoadPlayer()
    {
        float xPos = 0.00f;
        float yPos = 0.00f;
        float zPos = 0.00f;
        float xRot = 0.00f;
        float yRot = 0.00f;
        float zRot = 0.00f;
        float xScale = 0.00f;
        float yScale = 0.00f;
        float zScale = 0.00f;

        if (Player != null)
        {
            XmlNode root = pleezy.FirstChild;
            foreach (XmlNode node in root.ChildNodes)
            {
                switch (node.Name)
                {
                    case "xPos":
                        xPos = Convert.ToSingle(node.InnerText);
                        break;
                    case "yPos":
                        yPos = Convert.ToSingle(node.InnerText);
                        break;
                    case "zPos":
                        zPos = Convert.ToSingle(node.InnerText);
                        break;
                    case "xRot":
                        xRot = Convert.ToSingle(node.InnerText);
                        break;
                    case "yRot":
                        yRot = Convert.ToSingle(node.InnerText);
                        break;
                    case "zRot":
                        zRot = Convert.ToSingle(node.InnerText);
                        break;
                    case "xScale":
                        xScale = Convert.ToSingle(node.InnerText);
                        break;
                    case "yScale":
                        yScale = Convert.ToSingle(node.InnerText);
                        break;
                    case "zScale":
                        zScale = Convert.ToSingle(node.InnerText);
                        break;
                }
            }
            Player.transform.position = new Vector3(xPos, yPos, zPos);
            Player.transform.rotation = new Quaternion(xRot, yRot, zRot, 0.00f);
            Player.transform.localScale = new Vector3(xScale, yScale, zScale);
        }
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
