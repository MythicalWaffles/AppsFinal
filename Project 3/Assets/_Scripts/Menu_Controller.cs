using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Controller : MonoBehaviour
{
    public GameObject menu;
    public GameObject setting;
    public GameObject save;


    public void OpenMenu()
    {
        menu.SetActive(true);
    }
    public void ClosingMenu()
    {
        menu.SetActive(false);
    }
    public void OpenSettings()
    {
        setting.SetActive(true);
    }
    public void CloseSettings()
    {
        setting.SetActive(false);
    }
    public void OpenSave()
    {
        save.SetActive(true);
    }
    public void CloseSave()
    {
        save.SetActive(false);
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
