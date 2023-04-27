using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using UnityEngine.UI;
using System.IO;

public class Inventory: MonoBehaviour
{
    public GameObject earthspellsmall;
    public GameObject earthspellmedium;
    public GameObject earthspelllarge;

    public GameObject icespellsmall;
    public GameObject icespellmedium;
    public GameObject icespelllarge;

    public GameObject firespellsmall;
    public GameObject firespellmedium;
    public GameObject firespelllarge;

    public GameObject spawn;
    /*
     Game objects item1-12 are representations of the
    buttons on screen that house item info
     */
    public GameObject item1;
    public GameObject item2;
    public GameObject item3;
    public GameObject item4;
    public GameObject item5;
    public GameObject item6;
    public GameObject item7;
    public GameObject item8;
    public GameObject item9;
    public GameObject item10;
    public GameObject item11;
    public GameObject item12;
    public GameObject item13;
    public GameObject item14;


    /*
  Game objects quickitem1-4 are representations of the
 buttons on screen that house item info
  */
    public GameObject quickitem1;
    public GameObject quickitem2;
    public GameObject quickitem3;
    public GameObject quickitem4;


    public GameObject empty; // empty game object for filling linked list
    private LinkedList<ItemClass> primaryINV = new LinkedList<ItemClass>(); //linked list for the main inventory
    private LinkedList<ItemClass> quickINV = new LinkedList<ItemClass>(); //linked list for quick inventory
 
    ItemClass[] arrayMainInv; //array for main inventory items FOR GUI DISPLAY PURPOSES
    ItemClass[] arrayQuickInv; //array for quick items FOR GUI DISPLAY PURPOSES

    GameObject[] itemSlots; // array to house the Gameobjects item1-12
    GameObject[] quickitemSlots;// array to house the Gameobjects quickitem1-4

    private int CurrentOccupiedInventorySlots = 0; // integer that changes based on the number of items that have useable game items in them
    private int inventorySize = 14; // main inventory size

    bool changing; //bool that changes based on whether or not the user is assigning items to quick inv
    string assign = ""; //used to grab text of the button that acts as an inventory slot
    ItemClass tempItem; //temp item to used in reassigning quick inv

    public Player_Stats Player_Stats;

    FileHandler fh;

    bool setter;

    void Start()
    {
        itemSlots = new GameObject[] { item1, item2, item3, item4, item5, item6, item7,
            item8, item9, item10, item11, item12, item13, item14 };
        quickitemSlots = new GameObject[] { quickitem1, quickitem2, quickitem3, quickitem4 };

        Player_Stats = GetComponent<Player_Stats>();
        fh = new FileHandler();
        sets();
        changing = false;
        StartingInventory();




    }

    private void Awake()
    {

    }

    void FixedUpdate()
    {

        updateDisplayInv();
        updateQuickDisplay();
        Revealinventory();
    }

    /// <summary>
    /// Boot inventory initializes the arrays for quick inventory and main inventort. 
    /// It populates the array with blank ItemClass objects. 
    /// It then adds those empty items classes to the linked lists. 
    /// </summary>
    public void bootInventory()
    {
        /*
         * IM THINKING SOMEWHERE IN HERE I CAN MAKE IT TO WHERE BASE ON A BOOLEAN 
         * THE INVENTORY IS BOOTED FROM A FILE OR FROM SCRATCH
         */
        arrayMainInv = new ItemClass[inventorySize];
        arrayQuickInv = new ItemClass[4];
        for (int i = 0; i < inventorySize; i++)
        {
            arrayMainInv[i] = new ItemClass( 0, "nothing");
            primaryINV.AddLast(arrayMainInv[i]);

            if (i < 4)
            {
                arrayQuickInv[i] = arrayMainInv[i];
                quickINV.AddLast(arrayQuickInv[i]);
                //quickitemSlots[i].GetComponentInChildren<TextMeshProUGUI>().text = arrayQuickInv[i].Name;
            }

        }
    }

    /// <summary>
    /// take in a game objectg and count and either adds the items to the linklinked and by another method add it  to the corresponding array
    /// or adjust the quantity of the item in your array. 
    /// </summary>
    /// <param name="gameobj"></param>
    /// <param name="count"></param>
    void addToInventory(GameObject gameobj, int count)
    {

        if (CurrentOccupiedInventorySlots >= inventorySize)
        {
            Debug.Log("You're Done Kid");

        }
        else {

            foreach (ItemClass iC in primaryINV)
            {
                if (iC.Name != "nothing")
                {
                    if (gameobj.name == iC.Name)
                    {
                        Debug.Log("The 'double if' option for add2Inv was taken");
                        int newValue = iC.Quantity;
                        iC.Quantity = newValue;
                        UpdateMainArray();
                        break;
                    }
                }
                else
                {
                    Debug.Log("The else option for add2Inv was taken");
                    primaryINV.AddFirst(new ItemClass(count, gameobj.name));
                    primaryINV.RemoveLast();
                    Debug.Log("add2 prime inv size: " + primaryINV.Count);
                    CurrentOccupiedInventorySlots++;
                    Debug.Log("Occupied Inv" +CurrentOccupiedInventorySlots);
                    UpdateMainArray();
                    break;
                }
            }

        }



    }

    void reduceItems(GameObject obj, int count)
    {
        ItemClass[] arrayPrimary = new ItemClass[inventorySize];
        primaryINV.CopyTo(arrayPrimary, 0);
        ItemClass[] arrayQuick = new ItemClass[4];
        quickINV.CopyTo(arrayQuick, 0);

        for (int i = 0; i < arrayQuick.Length; i++)
        {
            if (obj.GetComponentInChildren<TextMeshProUGUI>().text == arrayQuick[i].Name && arrayQuick[i].Quantity > 0)
            {
                int tempV = arrayQuick[i].Quantity - count;
                arrayQuick[i].Quantity = tempV;
                UpdateQuickArray();

                for (int j = 0; j < arrayPrimary.Length;j++)
                {
                    if (arrayQuick[i].Name == arrayPrimary[j].Name && arrayPrimary[j].Quantity > 0)
                    {
                        int tempVEE = arrayPrimary[j].Quantity - count;
                        arrayPrimary[j].Quantity = tempVEE;
                        UpdateMainArray();
                    }
                }

            }
            else if (obj.GetComponentInChildren<TextMeshProUGUI>().text == arrayQuick[i].Name && arrayQuick[i].Quantity == 0)
            {
                quickINV.Remove(arrayQuick[i]);
                quickINV.AddLast(new ItemClass( 0, "nothing"));
                UpdateQuickArray();

                for (int j = 0; j < arrayPrimary.Length; j++)
                {
                    if (arrayQuick[i].Name == arrayPrimary[j].Name && arrayPrimary[j].Quantity == 0)
                    {
                        Debug.Log("Main Quantity should equal 0: " + arrayPrimary[j].Quantity);
                        primaryINV.Remove(arrayPrimary[j]);
                        primaryINV.AddLast(new ItemClass( 0, "nothing"));
                        Debug.Log(primaryINV.Count);
                        CurrentOccupiedInventorySlots--;
                        UpdateMainArray();
                    }
                }
            }
        }
    }

    /// <summary>
    /// Checking quick inventory to see if you need to update the quantity after an object is picked up
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="count"></param>
    void CheckQuick(GameObject obj, int count)
    {
        foreach (ItemClass iC in quickINV)
        {
            if (iC.Name != "nothing")
            {
                if (obj.name == iC.Name)
                {
                    Debug.Log("The 'double if' option for add2Inv was taken");
                    int newValue = iC.Quantity;
                    iC.Quantity = newValue;
                    UpdateQuickArray();
                    break;
                }
            }
        }
    }

    void UpdateMainArray()
    {
        Debug.Log("This Happened");
        ItemClass[] temparr = new ItemClass[inventorySize];
        primaryINV.CopyTo(temparr, 0);
        for(int i=0; i < temparr.Length; i++)
        {
            if (i < inventorySize)
            {
                arrayMainInv[i] = temparr[i];
            }
        }

    }
    void UpdateQuickArray()
    {
        ItemClass[] temparray = new ItemClass[4];
        quickINV.CopyTo(temparray, 0);
        for (int i = 0; i < temparray.Length; i++)
        {
            if (i < 4)
            {
                arrayQuickInv[i] = temparray[i];
            }
        }
    }

    void updateQuickDisplay()
    {
        for (int i = 0; i < quickitemSlots.Length; i++)
        {
            quickitemSlots[i].GetComponentInChildren<TextMeshProUGUI>().text = arrayQuickInv[i].Name;
        }
    }

    void updateDisplayInv()
    {
        for (int i = 0; i < arrayMainInv.Length; i++)
        {
            itemSlots[i].GetComponentInChildren<TextMeshProUGUI>().text = arrayMainInv[i].Name;
        }
    }
    public void grabbingItem(GameObject obj)
    {

        if (changing == true)
        {
            assign = obj.GetComponentInChildren<TextMeshProUGUI>().text;

            foreach (ItemClass iC in primaryINV)
            {
                if ( iC.Name == assign)
                {
                    Debug.Log("Its working");
                    tempItem = iC;
                }
            }

        }
    }
    public void puttingItem(GameObject obj)
    {
        if (changing == true)
        {
            Debug.Log("puttingItems");
            LinkedListNode<ItemClass> firstnode = quickINV.First;
            if (obj.name == "Quick1")
            {
                Debug.Log("Q1");
                quickINV.RemoveFirst();
                quickINV.AddFirst(tempItem);
                UpdateQuickArray();
            } else if (obj.name == "QI1 (1)")
            {
                quickINV.Remove(firstnode.Next);
                quickINV.AddAfter(firstnode, tempItem);
                UpdateQuickArray();
            } else if (obj.name == "QI1 (2)")
            {
                quickINV.Remove(firstnode.Next.Next);
                quickINV.AddAfter(firstnode.Next, tempItem);
                UpdateQuickArray();
            } else if (obj.name == "QI1 (3)")
            {
                quickINV.RemoveLast();
                quickINV.AddLast(tempItem);
                UpdateQuickArray();
            }
        }
    }

    public void UseItem(GameObject obj)
    {
        if (changing == false)
        {
            string nameofitem = obj.GetComponentInChildren<TextMeshProUGUI>().text;
            switch (nameofitem)
            {
                case "H1":
                    reduceItems(obj, 1);
                    AdjustHealth(10);
                    break;
                case "H2":
                    reduceItems(obj, 1);
                    AdjustHealth(20);
                    break;
                case "H3":
                    reduceItems(obj, 1);
                    AdjustHealth(30);
                    break;
                case "lvl1_EarthSpell":
                    reduceItems(obj, 1);
                    Blasting("lvl1_EarthSpell");
                    break;
                case "lvl2_EarthSpell":
                    reduceItems(obj, 1);
                    Blasting("lvl2_EarthSpell");
                    break;
                case "lvl3_EarthSpell":
                    reduceItems(obj, 1);
                    Blasting("lvl3_EarthSpell");
                    break;
                case "lvl1_IceSpell":
                    reduceItems(obj, 1);
                    Blasting("lvl1_IceSpell");
                    break;
                case "lvl2_IceSpell":
                    reduceItems(obj, 1);
                    Blasting("lvl2_IceSpell");
                    break;
                case "lvl3_IceSpell":
                    reduceItems(obj, 1);
                    Blasting("lvl3_IceSpell");
                    break;
                case "lvl1_firespell":
                    reduceItems(obj, 1);
                    Blasting("lvl1_firespell");
                    break;
                case "lvl2_firespell":
                    reduceItems(obj, 1);
                    Blasting("lvl2_firespell");
                    break;
                case "lvl3_firespell":
                    reduceItems(obj, 1);
                    Blasting("lvl3_firespell");
                    break;
            }
        } 
    }
    public void UseItemFromAlphas(int num)
    {
        switch (num)
        {
            case 1:
                reduceItems(quickitem1, 1);
                UseItem(quickitem1);
                break;
            case 2:
                reduceItems(quickitem2, 1);
                UseItem(quickitem2);
                break;
            case 3:
                reduceItems(quickitem3, 1);
                UseItem(quickitem3);
                break;
            case 4:
                reduceItems(quickitem4, 1);
                UseItem(quickitem4);
                break;

        }
    }

    void Blasting(string itemName)
    {
        if (itemName == "lvl1_EarthSpell")
        {
            Instantiate( earthspellsmall, spawn.transform.position, spawn.transform.rotation);
        }
        else if (itemName == "lvl2_EarthSpell")
        {
            Instantiate(  earthspellmedium   , spawn.transform.position, spawn.transform.rotation);
        }
        else if (itemName == "lvl3_EarthSpell")
        {
            Instantiate(   earthspelllarge , spawn.transform.position, spawn.transform.rotation);
        }
        else if (itemName == "lvl1_IceSpell")
        {
            Instantiate(   icespellsmall , spawn.transform.position, spawn.transform.rotation);
        }
        else if (itemName == "lvl2_IceSpell")
        {
            Instantiate(   icespellmedium , spawn.transform.position, spawn.transform.rotation);
        }
        else if (itemName == "lvl3_IceSpell")
        {
            Instantiate(   icespelllarge , spawn.transform.position, spawn.transform.rotation);
        }
        else if (itemName == "lvl1_firespell")
        {
            Instantiate(   firespellsmall , spawn.transform.position, spawn.transform.rotation);
        }
        else if (itemName == "lvl2_firespell")
        {
            Instantiate(  firespellmedium  , spawn.transform.position, spawn.transform.rotation);
        }
        else if (itemName == "lvl3_firespell")
        {
            Instantiate(   firespelllarge , spawn.transform.position, spawn.transform.rotation);
        }


    }

    public void setChangingTrue()
    {
        changing = true; 
    }
    public void setChangingFalse()
    {
        changing = false;
        tempItem = null;
    }

    public void StartingInventory()
    {
        item7.SetActive(false);
        item8.SetActive(false);
        item9.SetActive(false);
        item10.SetActive(false);
        item11.SetActive(false);
        item12.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("H1"))
        {
            addToInventory(other.gameObject, 5);
            CheckQuick(other.gameObject, 5);
            other.gameObject.SetActive(false);
        }
        else if (other.tag.Equals("H2"))
        {
            addToInventory(other.gameObject, 5);
            CheckQuick(other.gameObject, 5);
            other.gameObject.SetActive(false);
        }
        else if (other.tag.Equals("H3"))
        {
            addToInventory(other.gameObject, 5);
            CheckQuick(other.gameObject, 5);
            other.gameObject.SetActive(false);
        }
        else if (other.tag.Equals("W1"))
        {
            addToInventory(earthspellsmall, 10);
            addToInventory(icespellsmall, 10);
            addToInventory(firespellsmall, 10);
            CheckQuick(earthspellsmall, 10);
            CheckQuick(icespellsmall, 10);
            CheckQuick(firespellsmall, 10);
            other.gameObject.SetActive(false);
        }
        else if (other.tag.Equals("W2"))
        {
            addToInventory(earthspellmedium, 10);
            addToInventory(icespellmedium, 10);
            addToInventory(firespellmedium, 10);
            CheckQuick(earthspellmedium, 10);
            CheckQuick(icespellmedium, 10);
            CheckQuick(firespellmedium, 10);
            other.gameObject.SetActive(false);
        }
        else if (other.tag.Equals("W3"))
        {
            addToInventory(earthspelllarge, 10);
            addToInventory(icespelllarge, 10);
            addToInventory(firespelllarge, 10);
            CheckQuick(earthspelllarge, 10);
            CheckQuick(icespelllarge, 10);
            CheckQuick(firespelllarge, 10);
            other.gameObject.SetActive(false);
        }

    }

    public void sets()
    {
        Stream newStream = File.OpenRead("Load.json");
        BinaryFormatter bf = new BinaryFormatter();
        setter = (bool)(bf.Deserialize(newStream));

        newStream.Close();

        LoadPresavedInventory();
    }

    public void LoadPresavedInventory()
    {
        if (setter.Equals(true)){
            fh.ReadFromJson();
            arrayMainInv = fh.mainInv;
            arrayQuickInv = fh.quickInv;
            for (int i = 0; i < inventorySize; i++)
            {
                arrayMainInv[i] = new ItemClass(0, "nothing");
                primaryINV.AddLast(arrayMainInv[i]);

                if (i < 4)
                {
                    arrayQuickInv[i] = arrayMainInv[i];
                    quickINV.AddLast(arrayQuickInv[i]);
                    //quickitemSlots[i].GetComponentInChildren<TextMeshProUGUI>().text = arrayQuickInv[i].Name;
                }

            }
        } else
        {
            bootInventory();
        }
    }
   
    void Revealinventory()
    {
        if (CurrentOccupiedInventorySlots >= 7)
        {
            item7.SetActive(true);
        }
        else if (CurrentOccupiedInventorySlots < 7)
        {
            item7.SetActive(false);
        }

        if (CurrentOccupiedInventorySlots >= 8)
        {
            item8.SetActive(true);
        }
        else if (CurrentOccupiedInventorySlots < 8)
        {
            item8.SetActive(false);
        }

        if (CurrentOccupiedInventorySlots >= 9)
        {
            item9.SetActive(true);
        }
        else if (CurrentOccupiedInventorySlots < 9)
        {
            item9.SetActive(false);
        }

        if (CurrentOccupiedInventorySlots >= 10)
        {
            item10.SetActive(true);
        }
        else if (CurrentOccupiedInventorySlots < 10)
        {
            item10.SetActive(false);
        }

        if (CurrentOccupiedInventorySlots >= 11)
        {
            item11.SetActive(true);
        }
        else if (CurrentOccupiedInventorySlots < 11)
        {
            item11.SetActive(false);
        }

        if (CurrentOccupiedInventorySlots >= 12)
        {
            item12.SetActive(true);
        }
        else if (CurrentOccupiedInventorySlots < 12)
        {
            item12.SetActive(false);
        }

        if (CurrentOccupiedInventorySlots >= 13)
        {
            item13.SetActive(true);
        }
        else if (CurrentOccupiedInventorySlots < 13)
        {
            item13.SetActive(false);
        }

        if (CurrentOccupiedInventorySlots >= 14)
        {
            item14.SetActive(true);
        }
        else if (CurrentOccupiedInventorySlots < 14)
        {
            item14.SetActive(false);
        }
    }
    void AdjustHealth(int hp)
    {
        float adjust = hp + Player_Stats.Health;
        Player_Stats.Health = adjust;

    }


    public void saveinventory()
    {
        Debug.Log("We Here");
        fh.SaveToJson(primaryINV, quickINV);
    }
}
