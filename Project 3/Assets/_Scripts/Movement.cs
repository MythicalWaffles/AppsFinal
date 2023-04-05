using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Movement : MonoBehaviour
{
    bool changeKeys = false;
    //bool isControllerConnected = false;
    //public string Controller = "";

    public GameObject BigMenu;
    public GameObject congfig1Menu;
    public GameObject congfig2Menu;
    public GameObject congfig3Menu;

    string currentP = "profile 1";

    float speed = 15f;
    float RotSpeed = 100f;

    //public Rigidbody bullet;
    //public Rigidbody bulkBullet;
    //public Transform spawn;

    public Inventory inventory;

    /* This sections allows for the use of the variable in place of strings to signify
 * the use of a key code when implementing input.getkeycode
 * so i should be able to update these when its time to switch  inputs around
 */
    KeyCode UKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), "W");
    KeyCode DKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), "S");
    KeyCode LKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), "A");
    KeyCode RKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), "D");


    /* this section consist of strings for diffierent configs that make up the user profiles*/
   // string UKeycode = "W", DKeycode = "S", LKeycode = "A", RKeycode = "D"; 
    string UKeyCode_Con1 = "W", DKeyCode_Con1 = "S", LKeyCode_Con1 = "A", RKeyCode_Con1 = "D";
    string UKeyCode_Con2 = "T", DKeyCode_Con2 = "G", LKeyCode_Con2 = "F", RKeyCode_Con2 = "H";
    string UKeyCode_Con3 = "joystick button 3", DKeyCode_Con3 = "joystick button 0",
        LKeyCode_Con3 = "joystick button 2", RKeyCode_Con3 = "joystick button 1";


    /// <summary>
    /// Once one of the menu buttons is clicked it will set the other menus false and keep the menu clicked active.
    /// </summary>
    /// <param name="config"></param>
    public void setConfigMenu(string config)
    {
        switch (config)
        {
            case "config1":
                congfig1Menu.SetActive(true);
                congfig2Menu.SetActive(false);
                congfig3Menu.SetActive(false);
                currentP = "profile 1";

                break;
            case "config2":
                congfig1Menu.SetActive(false);
                congfig2Menu.SetActive(true);
                congfig3Menu.SetActive(false);
                currentP = "profile 2";

                break;
            case "config3":
                congfig1Menu.SetActive(false);
                congfig2Menu.SetActive(false);
                congfig3Menu.SetActive(true);
                currentP = "profile 3";

                break;
        }
    }
    /// <summary>
    /// Looks at the current state of the varable "currentP" and assigns the appropriate controls to the keymaps
    /// </summary>
    void settingActConfig()
    {
        if (currentP == "profile 1")
        {
            UKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), UKeyCode_Con1);
            DKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), DKeyCode_Con1);
            LKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), LKeyCode_Con1);
            RKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), RKeyCode_Con1);
        }
        if (currentP == "profile 2")
        {
            UKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), UKeyCode_Con2);
            DKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), DKeyCode_Con2);
            LKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), LKeyCode_Con2);
            RKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), RKeyCode_Con2);
        }
        if (currentP == "proflie 3")
        {
            UKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), UKeyCode_Con3);
            DKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), DKeyCode_Con3);
            LKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), LKeyCode_Con3);
            RKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), RKeyCode_Con3);
        }

        changeKeys = false;
    }

    /// <summary>
    /// Responsible for actually moveing the character depending on the current config
    /// </summary>
    void CurrentConfig()
    {
        if (!changeKeys) {


            if (currentP != "profile 3")
            {

                if (Input.GetKey(UKeyCode))
                {
                    this.transform.Translate(Vector3.forward * Time.deltaTime * speed);
                }
                if (Input.GetKey(DKeyCode))
                {
                    this.transform.Translate(Vector3.back * Time.deltaTime * speed);
                }
                if (Input.GetKey(LKeyCode))
                {
                    this.transform.Translate(Vector3.left * Time.deltaTime * speed);
                }
                if (Input.GetKey(RKeyCode))
                {
                    this.transform.Translate(Vector3.right * Time.deltaTime * speed);
                }


                if (currentP == "profile 1")
                {
                    //The Handles All mouse movement
                    if (Input.GetAxis("Mouse X") < 0)
                    {
                        this.transform.Rotate(Vector3.down * Time.deltaTime * RotSpeed);
                    }
                    else if (Input.GetAxis("Mouse X") > 0)
                    {
                        this.transform.Rotate(Vector3.up * Time.deltaTime * RotSpeed);
                    }
                }
                if (currentP == "profile 2")
                {
                    if (Input.GetKey("left"))
                    {
                        this.transform.Rotate(Vector3.down * Time.deltaTime * RotSpeed);
                    }
                    if (Input.GetKey("right"))
                    {
                        this.transform.Rotate(Vector3.up * Time.deltaTime * RotSpeed);
                    }
                }

            }
            //else if (currentP == "profile 3")
            //{
            //    if (Input.GetAxisRaw("Xboxlook") < 0)
            //    {
            //        this.transform.Rotate(Vector3.down * Time.deltaTime * RotSpeed);
            //    }
            //    if (Input.GetAxisRaw("Xboxlook") > 0)
            //    {
            //        this.transform.Rotate(Vector3.up * Time.deltaTime * RotSpeed);
            //    }
            //    if (Input.GetButton("joystick button 3"))
            //    {
            //        this.transform.Translate(Vector3.forward * Time.deltaTime * speed);
            //    }
            //    if (Input.GetButton("joystick button 0"))
            //    {
            //        this.transform.Translate(Vector3.back * Time.deltaTime * speed);
            //    }
            //    if (Input.GetButton("joystick button 2"))
            //    {
            //        this.transform.Translate(Vector3.left * Time.deltaTime * speed);
            //    }
            //    if (Input.GetButton("joystick button 1"))
            //    {
            //        this.transform.Translate(Vector3.right * Time.deltaTime * speed);
            //    }
            //}
        }

    }
    void firingScript()
    {
        if (currentP == "profile 1")
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                inventory.UseItemFromAlphas(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                inventory.UseItemFromAlphas(2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                inventory.UseItemFromAlphas(3);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                inventory.UseItemFromAlphas(4);
            }
        }
        if (currentP == "profile 2")
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                inventory.UseItemFromAlphas(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                inventory.UseItemFromAlphas(2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                inventory.UseItemFromAlphas(3);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                inventory.UseItemFromAlphas(4);
            }
        }
        //if (currentP == "profile 3")
        //{
        //    if (Input.GetButtonDown("Trigger"))
        //    {
               
        //    }
        //}
    }
    //void DetectController()
    //{
    //    if (Input.GetJoystickNames()[0] != null)
    //    {
    //        isControllerConnected = true;
    //        IdentifyController();
    //    }
    //}
    //void IdentifyController()
    //{
    //    Controller = Input.GetJoystickNames()[0];
    //}
    public void CloseMenus()
    {
        BigMenu.SetActive(false);

    }

    public void OpenMenus()
    {
        BigMenu.SetActive(true);
        setConfigMenu("config1");

    }



    void Start()
    {
        //Con1up.text = UKeyCode_Con1;
        //Con1down.text = DKeyCode_Con1;
        //Con1left.text = LKeyCode_Con1;
        //Con1right.text = RKeyCode_Con1;
        //BigMenu.SetActive(false);
    }

    private void Awake()
    {
        inventory = GetComponent<Inventory>();
    }
    void Update()
    {
        //keyPressed();
        firingScript();
        //DetectController();
        CurrentConfig();
        settingActConfig();

    }



    //public TextMeshProUGUI Con1up;
    //public TextMeshProUGUI Con1down;
    //public TextMeshProUGUI Con1left;
    //public TextMeshProUGUI Con1right;
    //public TextMeshProUGUI pressedKey;
    //string KEY = "";
    //bool getInputKey = false;
    //string inputKey = "";
    //string changingKey = "";
    ////public TextMeshProUGUI Con2up;
    ////public TextMeshProUGUI Con2down;
    ////public TextMeshProUGUI Con2left;
    ////public TextMeshProUGUI Con2right;

    ////public TextMeshProUGUI Con3up;
    ////public TextMeshProUGUI Con3down;
    ////public TextMeshProUGUI Con3left;
    ////public TextMeshProUGUI Con3right;
    //void BulletSpawning()
    //{
    //    int brange = Random.Range(0, 2);
    //    if (brange == 0)
    //    {
    //        Instantiate(bullet, spawn.position, spawn.rotation);
    //    }
    //    else if (brange == 1)
    //    {
    //        Instantiate(bulkBullet, spawn.position, spawn.rotation);
    //    }
    //}
    /// <summary>
    /// Responsible for switching a single input for a certain config, it is triggered after a specific button is pressed 
    /// </summary>
    /// <param name="key"></param>
    //public void switchSingleInput(string input)
    //{
    //    //foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
    //    //{
    //    //    if (Input.GetKey(vKey) && getInputKey)
    //    //    {
    //    //        inputKey = vKey.ToString();
    //    //        getInputKey = false;
    //    //    }
    //    //}
    //    //Debug.Log(inputKey);
    //        if (changingKey.Equals("UkeyCode_1") )
    //        {      
    //            UKeyCode_Con1 = input;
    //            Con1up.text = UKeyCode_Con1;
    //        }
    //        else if (changingKey.Equals("DkeyCode_1"))
    //        {
    //            DKeyCode_Con1 = input;
    //            Con1down.text = DKeyCode_Con1;
    //        }
    //        else if (changingKey.Equals("RkeyCode_1"))
    //        {
    //            RKeyCode_Con1 = input;
    //            Con1right.text = RKeyCode_Con1;
    //        }
    //        else if (changingKey.Equals("LkeyCode_1"))
    //        {
    //            LKeyCode_Con1 = input;
    //            Con1left.text = LKeyCode_Con1;
    //        }
    //  //changeKeys = false;   
    //}
    //public void pressedC1U()
    //{
    //    changeKeys = true;
    //    getInputKey = true;
    //    changingKey = "UkeyCode_1";
    //    inputKey = "";
    //}
    //public void pressedC1D()
    //{
    //    changeKeys = true;
    //    getInputKey = true;
    //    changingKey = "DkeyCode_1";
    //    inputKey = "";
    //}
    //public void pressedC1L()
    //{
    //    changeKeys = true;
    //    getInputKey = true;
    //    changingKey = "LkeyCode_1";
    //    inputKey = "";
    //}
    //public void pressedC1R()
    //{
    //    changeKeys = true;
    //    getInputKey = true;
    //    changingKey = "RkeyCode_1";
    //    inputKey = "";
    //}
    //public void keyPressed()
    //{
    //    foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
    //    {
    //        if (Input.GetKeyDown(vKey))
    //        {
    //            KEY = vKey.ToString();
    //        }
    //        else if (Input.GetKeyUp(vKey))
    //        {
    //            KEY = " ";
    //        }
    //    }
    //    pressedKey.text = "KeyPressed: " + KEY;
    //    switchSingleInput(KEY);
    //}

}
