using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIType
{
    Menu = 0,
    Profile,
    Elevator
};

public class UIController : MonoBehaviour
{
    public GameObject Menu;
    public GameObject Interaction;
    public GameObject ElevatorUI;
    
    PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        
    }

    public void UIModeOn(UIType type) //무슨 UI든 켜질 때
    {
        playerController.SetSpeedUI(true); //카메라, 캐릭터 못 움직이게
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true; //마우스 ON
        if (type == UIType.Menu)
        {
            Menu.SetActive(true);
        }
        else if(type == UIType.Elevator)
        {
            ElevatorUI.SetActive(true);
        }
    }

    public void UIModeOff() //모든 UI가 꺼졌을 때
    {
        playerController.SetSpeedUI(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; //마우스 제거
    }

    public void SetInteraction(bool a)
    {
        Interaction.SetActive(a);
    }
}