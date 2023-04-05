using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemClass
{

    private int quantity;
    private GameObject gameObj;
    private string name;


    public ItemClass(GameObject gameOb, int count, string named)
    {
        quantity = count;
        gameObj = gameOb;
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
