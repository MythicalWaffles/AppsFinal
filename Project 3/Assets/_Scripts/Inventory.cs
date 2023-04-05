using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory: MonoBehaviour
{
    public GameObject spell1;
    public GameObject spell2;
    public GameObject spell3;
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
    private LinkedListNode<ItemClass> node; //A node to traverse linked list
    ItemClass[] arrayMainInv; //array for main inventory items
    ItemClass[] arrayQuickInv; //array for quick items
    GameObject[] itemSlots; // array to house the Gameobjects item1-12
    GameObject[] quickitemSlots;// array to house the Gameobjects quickitem1-4

    private int CurrentOccupiedInventorySlots = 0; // integer that changes based on the number of items that have useable game items in them
    private int inventorySize = 12; // main inventory size

    bool changing; //bool that changes based on whether or not the user is assigning items to quick inv
    string assign = ""; //used to grab text of the button that acts as an inventory slot
    ItemClass tempItem; //temp item to used in reassigning quick inv

    public Player_Stats Player_Stats;
    /// <summary>
    /// Boot inventory initializes the arrays for quick inventory and main inventort. 
    /// It populates the array with blank ItemClass objects. 
    /// It then adds those empty items classes to the linked lists. 
    /// </summary>
    public void bootInventory()
    {
        arrayMainInv = new ItemClass[inventorySize];
        arrayQuickInv = new ItemClass[4];
        for (int i = 0; i < inventorySize; i++)
        {
            arrayMainInv[i] = new ItemClass(empty, 0, "nothing");
            primaryINV.AddLast(arrayMainInv[i]);

            if (i < 4)
            {
                arrayQuickInv[i] = arrayMainInv[i];
                quickINV.AddLast(arrayQuickInv[i]);
                quickitemSlots[i].GetComponentInChildren<TextMeshProUGUI>().text = arrayQuickInv[i].Name;
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
        //Debug.Log("It has begun");
        //LinkedListNode<ItemClass> head = primaryINV.First;
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
                    primaryINV.AddFirst(new ItemClass(gameobj, count, gameobj.name));
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

        //for (int i = 0; i < arrayPrimary.Length; i++)
        //{
        //    if (obj.GetComponentInChildren<TextMeshProUGUI>().text == arrayPrimary[i].Name && arrayPrimary[i].Quantity > 0)
        //    {
        //        int tempV = arrayPrimary[i].Quantity - count;
        //        arrayPrimary[i].Quantity = tempV;
        //        UpdateMainArray();
        //    }
        //    else if (obj.GetComponentInChildren<TextMeshProUGUI>().text == arrayPrimary[i].Name && arrayPrimary[i].Quantity == 0)
        //    {
        //        Debug.Log("Main Quantity should equal 0: " + arrayPrimary[i].Quantity);
        //        primaryINV.Remove(arrayPrimary[i]);
        //        primaryINV.AddLast(new ItemClass(empty, 0, "nothing"));
        //        CurrentOccupiedInventorySlots--;
        //        UpdateMainArray();
        //    }
        //}
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
                //Debug.Log("Main Quantity should equal 0: " + arrayQuick[i].Quantity);
                quickINV.Remove(arrayQuick[i]);
                quickINV.AddLast(new ItemClass(empty, 0, "nothing"));
                UpdateQuickArray();

                for (int j = 0; j < arrayPrimary.Length; j++)
                {
                    if (arrayQuick[i].Name == arrayPrimary[j].Name && arrayPrimary[j].Quantity == 0)
                    {
                        Debug.Log("Main Quantity should equal 0: " + arrayPrimary[j].Quantity);
                        primaryINV.Remove(arrayPrimary[j]);
                        primaryINV.AddLast(new ItemClass(empty, 0, "nothing"));
                        Debug.Log(primaryINV.Count);
                        CurrentOccupiedInventorySlots--;
                        UpdateMainArray();
                    }
                }
            }
        }
        //foreach (ItemClass iC in primaryINV)
        //{
        //    if (obj.GetComponentInChildren<TextMeshProUGUI>().text == iC.Name && iC.Quantity > 0)
        //    {
        //        int tempV = iC.Quantity - count;
        //        iC.Quantity = tempV;
        //        UpdateMainArray();
        //    } else if (obj.GetComponentInChildren<TextMeshProUGUI>().text == iC.Name && iC.Quantity == 0)
        //    {
        //        Debug.Log("Main Quantity should equal 0: " + iC.Quantity);
        //        primaryINV.Remove(iC);
        //        primaryINV.AddLast(new ItemClass(empty, 0, "nothing"));
        //        CurrentOccupiedInventorySlots--;
        //        UpdateMainArray();
        //    }
        //}
        //foreach (ItemClass iC in quickINV)
        //{
        //    if (obj.GetComponentInChildren<TextMeshProUGUI>().text == iC.Name && iC.Quantity > 0)
        //    {
        //        int tempV = iC.Quantity - count;
        //        iC.Quantity = tempV;
        //        UpdateQuickArray();
        //    }
        //    else if (obj.GetComponentInChildren<TextMeshProUGUI>().text == iC.Name && iC.Quantity == 0)
        //    {
        //        Debug.Log("Quick Quantity should equal 0: " + iC.Quantity);
        //        //primaryINV.Remove(iC);
        //        quickINV.Remove(iC);
        //        //primaryINV.AddLast(new ItemClass(empty, 0, "nothing"));
        //        quickINV.AddLast(new ItemClass(empty, 0, "nothing"));
        //        UpdateQuickArray();
        //    }
        //}
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
        //int count = 0;
        ItemClass[] temparr = new ItemClass[inventorySize];
        primaryINV.CopyTo(temparr, 0);
        for(int i=0; i < temparr.Length; i++)
        {
            if (i < inventorySize)
            {
                arrayMainInv[i] = temparr[i];
            }
           // count++;
        }


        //foreach (ItemClass iC in primaryINV)
        //{
        //    if (count < inventorySize)
        //    {
        //        arrayMainInv[count] = iC;
        //    }
        //    count++;
        //}


    }

    void UpdateQuickArray()
    {
        // int count = 0;

        ItemClass[] temparray = new ItemClass[4];
        quickINV.CopyTo(temparray, 0);
        for (int i = 0; i < temparray.Length; i++)
        {
            if (i < 4)
            {
                arrayQuickInv[i] = temparray[i];
                //Debug.Log(arrayMainInv.ToString());
            }
            //count++;
        }

        //foreach (ItemClass iC in quickINV)
        //{
        //    if (count < inventorySize)
        //    {
        //        arrayQuickInv[count] = iC;
        //    }
        //    count++;
        //}
    }

    void updateQuickDisplay()
    {
        for (int i = 0; i < arrayQuickInv.Length; i++)
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

    void Revealinventory()
    {
        if (CurrentOccupiedInventorySlots >= 7)
        {
            item7.SetActive(true);
        }
        if (CurrentOccupiedInventorySlots >= 8)
        {
            item8.SetActive(true);
        }
        if (CurrentOccupiedInventorySlots >= 9)
        {
            item9.SetActive(true);
        }
        if (CurrentOccupiedInventorySlots >= 10)
        {
            item10.SetActive(true);
        }
        if (CurrentOccupiedInventorySlots >= 11)
        {
            item11.SetActive(true);
        }
        if (CurrentOccupiedInventorySlots >= 12)
        {
            item12.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Health_item_1"))
        {
            addToInventory(other.gameObject, 5);
            CheckQuick(other.gameObject, 5);
            other.gameObject.SetActive(false);
        }else if (other.tag.Equals("Health_item_2"))
        {
            addToInventory(other.gameObject, 5);
            CheckQuick(other.gameObject, 5);
            other.gameObject.SetActive(false);
        }
        else if (other.tag.Equals("Health_item_3"))
        {
            addToInventory(other.gameObject, 5);
            CheckQuick(other.gameObject, 5);
            other.gameObject.SetActive(false);
        }
        else if (other.tag.Equals("AmmoBox1"))
        {
            addToInventory(spell1, 5);
            addToInventory(spell2, 5);
            addToInventory(spell3, 5);
            CheckQuick(spell1, 5);
            CheckQuick(spell2, 5);
            CheckQuick(spell3, 5);
            other.gameObject.SetActive(false);
        }
        else if (other.tag.Equals("AmmoBox2"))
        {
            addToInventory(spell1, 10);
            addToInventory(spell2, 10);
            addToInventory(spell3, 10);
            CheckQuick(spell1, 10);
            CheckQuick(spell2, 10);
            CheckQuick(spell3, 10);
            other.gameObject.SetActive(false);
        }
        else if (other.tag.Equals("AmmoBox3"))
        {
            addToInventory(spell1, 20);
            addToInventory(spell2, 20);
            addToInventory(spell3, 20);
            CheckQuick(spell1, 20);
            CheckQuick(spell2, 20);
            CheckQuick(spell3, 20);
            other.gameObject.SetActive(false);
        }
        else if (other.tag.Equals("Random"))
        {
            addToInventory(other.gameObject, 5);
            CheckQuick(other.gameObject, 5);
            other.gameObject.SetActive(false);
        }
    }

    public void grabbingItem(GameObject obj)
    {

        if (changing == true)
        {
            assign = obj.GetComponentInChildren<TextMeshProUGUI>().text;

            foreach (ItemClass iC in primaryINV)
            {
                if (iC.Name == assign)
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
            LinkedListNode<ItemClass> firstnode = quickINV.First;
            if (obj.name == "QItem1")
            {
                quickINV.RemoveFirst();
                quickINV.AddFirst(tempItem);
                UpdateQuickArray();
            } else if (obj.name == "QItem2")
            {
                quickINV.Remove(firstnode.Next);
                quickINV.AddAfter(firstnode, tempItem);
                UpdateQuickArray();
            } else if (obj.name == "QItem3")
            {
                quickINV.Remove(firstnode.Next.Next);
                quickINV.AddAfter(firstnode.Next, tempItem);
                UpdateQuickArray();
            } else if (obj.name == "QItem4")
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
                case "Health_item_1":
                    reduceItems(obj, 1);
                    AdjustHealth(10);
                    break;
                case "Health_item_2":
                    reduceItems(obj, 1);
                    AdjustHealth(20);
                    break;
                case "Health_item_3":
                    reduceItems(obj, 1);
                    AdjustHealth(30);
                    break;
                case "Spell1":
                    reduceItems(obj, 1);
                    Blasting(obj);
                    break;
                case "Spell2":
                    reduceItems(obj, 1);
                    Blasting(obj);
                    break;
                case "Spell3":
                    reduceItems(obj, 1);
                    Blasting(obj);
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

    void AdjustHealth(int hp)
    {
        float adjust = hp + Player_Stats.Health;
        Player_Stats.Health = adjust;

    }
    void Blasting(GameObject obj)
    {
        if (obj.GetComponentInChildren<TextMeshProUGUI>().text == "Spell1")
        {
            Instantiate(spell1, spawn.transform.position, spawn.transform.rotation);
        } else if (obj.GetComponentInChildren<TextMeshProUGUI>().text == "Spell2")
        {
            Instantiate(spell2, spawn.transform.position, spawn.transform.rotation);
        }
        else if(obj.GetComponentInChildren<TextMeshProUGUI>().text == "Spell3")
        {
            Instantiate(spell3, spawn.transform.position, spawn.transform.rotation);
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

    public void openInventory(GameObject obj)
    {
        obj.SetActive(true);
    }
    public void closeInventory(GameObject obj)
    {
        obj.SetActive(false);
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
    void Start()
    {
        itemSlots = new GameObject[] { item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, item11, item12 };
        quickitemSlots = new GameObject[] { quickitem1, quickitem2, quickitem3, quickitem4 };
        bootInventory();
        changing = false;
        StartingInventory();
    }
    

    // Update is called once per frame
    void Update()
    {
        
        updateDisplayInv();
        updateQuickDisplay();
        Revealinventory();
    }

    private void Awake()
    {

        Player_Stats = GetComponent<Player_Stats>();
    }

    //void printarray()
    //{
    //    for (int i = 0; i < arrayMainInv.Length; i++)
    //    {
    //        Debug.Log("index " + i + ": " + arrayMainInv[i] + "");
    //    }
    //}
}
