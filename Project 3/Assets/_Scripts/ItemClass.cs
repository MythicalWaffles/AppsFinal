using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ItemClass
{

    private int quantity;
    private string name;


    public ItemClass(int count, string named)
    {
        quantity = count;
        name = named;
    }

    public int Quantity
    {
        get { return quantity; }
        set { quantity = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
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
