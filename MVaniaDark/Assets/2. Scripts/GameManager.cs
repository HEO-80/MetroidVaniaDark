using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ControllerType
{
    PC,
    MOBILE
}


public class GameManager : MonoBehaviour
{

    public ControllerType controller;
    public PlayerController player;
    public GameObject mobileController;

    // Start is called before the first frame update
    void Start()
    {
        ControllerSetup();
    }

    // Update is called once per frame
    void Update()
    {
        ControllerSetup();
    }

    public void ControllerSetup()
    {
        if (controller == ControllerType.PC)
        {
            mobileController.SetActive(false);
        }
        if (controller == ControllerType.MOBILE)
        {
            mobileController.SetActive(true);
        }
        player.controller = controller;
    }
}
