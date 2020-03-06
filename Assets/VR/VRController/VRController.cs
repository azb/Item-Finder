using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script was created by Arthur Baney on June 10th 2019
//  A class for abstracting various vr controllers 
//  functionality into a common class format
//  To add a new controller format, create a new class named VRControllerPlatformName
//  example VRControllerSteam
//  And have it extend this class (VRControllerPlatformName : VRController)
//  And then implement map for the buttons and axes

public class VRController : MonoBehaviour
{
    public enum Hand { Left , Right };

    private Hand hand { get; set; }

    public enum Input { Trigger , Joystick, Touchpad, Button };

    public VRControllerTrigger 
        triggerLeftHand, 
        triggerRightHand
        ;

    public VRControllerTouchpad 
        touchpadLeftHand,
        touchpadRightHand
        ;
    
    public bool GetTriggerHeld(Hand hand)
    {
        if (hand == Hand.Left)
        {
            return triggerLeftHand.Held();
        }
        else
        {
            return triggerRightHand.Held();
        }
    }
    
    public bool GetTriggerPressed(Hand hand)
    {
        if (hand == Hand.Left)
        {
            return triggerLeftHand.Pressed();
        }
        else
        {
            return triggerRightHand.Pressed();
        }
    }
    
    public bool GetTriggerReleased(Hand hand)
    {
        if (hand == Hand.Left)
        {
            return triggerLeftHand.Released();
        }
        else
        {
            return triggerRightHand.Released();
        }
    }
    
    void Awake()
    {
        triggerLeftHand = 
            gameObject.AddComponent<VRControllerTrigger>();

        triggerRightHand = 
            gameObject.AddComponent<VRControllerTrigger>();
        
        triggerLeftHand.SetHand(Hand.Left);
        triggerRightHand.SetHand(Hand.Right);
        
        touchpadLeftHand = 
            gameObject.AddComponent<VRControllerTouchpad>();

        touchpadRightHand = 
            gameObject.AddComponent<VRControllerTouchpad>();
        
        touchpadLeftHand.SetHand(Hand.Left);
        touchpadRightHand.SetHand(Hand.Right);
    }

    //Button
    public bool[] 
        buttonPressed, 
        buttonHeld, 
        buttonReleased;

    public bool joystickUpPressed,
                joystickUpHeld,
                joystickUpReleased;


    public bool usesJoystick;

    public float[] joystick_axis;
    public float[] joystick_axis_prev;
    public float[] joystick_axis_sensitivity;

    public bool[] joystick_axis_pressed;
    public bool[] joystick_axis_held;
    public bool[] joystick_axis_released;



    //public virtual void SetJoystickUpPressed()
    //{
    //    //Implement in child classes
    //}

    //public virtual void SetJoystickUpHeld()
    //{
    //    //Implement in child classes
    //}

    //public virtual void SetJoystickUpReleased()
    //{
    //    //Implement in child classes
    //}

    public virtual void SetTriggerStates()
    {
        //Implement in child classes
        Debug.LogError("SetTriggerStates not implemented");
    }

    public virtual void SetJoystickStates()
    {
        //Implement in child classes
        Debug.LogError("SetJoystickStates not implemented");
    }
    
    public virtual void SetTouchpadStates()
    {
        //Implement in child classes
        Debug.LogError("SetTouchpadStates not implemented");
    }

    public virtual void SetButtonStates()
    {
        string test = "hello";
        Debug.Log($"GetsHere5{test}");


        //Implement in child classes
        Debug.LogError("SetButtonStates not implemented");
    }

    // Use this for initialization
    public virtual void Start()
    {
        buttonPressed = new bool[6];
        buttonHeld = new bool[6];
        buttonReleased = new bool[6];

        joystick_axis = new float[20];
        joystick_axis_prev = new float[20];
        joystick_axis_sensitivity = new float[20];

        joystick_axis_pressed = new bool[20];
        joystick_axis_held = new bool[20];
        joystick_axis_released = new bool[20];

        for (int i = 0; i < 20; i++)
        {
            joystick_axis_sensitivity[0] = 1f;
        }
    }


    public virtual void Update()
    {
        
        SetTriggerStates();

        if (usesJoystick)
        {
            SetJoystickStates();
        }

        SetTouchpadStates();

        SetButtonStates();

    }



    protected void SetJoystickAxis(int id, float value)
    {

        joystick_axis_prev[id] = joystick_axis[id];
        joystick_axis[id] = value;

        joystick_axis_pressed[id] = false;
        joystick_axis_released[id] = false;

        if (Mathf.Abs(joystick_axis_prev[id]) < .5f
            && Mathf.Abs(joystick_axis[id]) >= .5f)
        {
            joystick_axis_pressed[id] = true;
            joystick_axis_held[id] = true;
        }

        if (Mathf.Abs(joystick_axis_prev[id]) >= .5f
            && Mathf.Abs(joystick_axis[id]) < .5f)
        {
            joystick_axis_held[id] = false;
            joystick_axis_released[id] = true;
        }
    }

    public float GetJoystickAxis(int id)
    {
        return joystick_axis[id];
    }

    public bool GetJoystickAxisPressed(int id)
    {
        return joystick_axis_pressed[id];
    }

    public bool GetJoystickAxisHeld(int id)
    {
        return joystick_axis_held[id];
    }

    public bool GetJoystickAxisReleased(int id)
    {
        return joystick_axis_released[id];
    }
    
    public bool GetButtonPressed(int buttonID)
    {
        return buttonPressed[buttonID];
    }

    public bool GetButtonHeld(int buttonID)
    {
        return buttonHeld[buttonID];
    }

    public bool GetButtonReleased(int buttonID)
    {
        return buttonReleased[buttonID];
    }

    public virtual void Vibrate(ushort durationMicroSec)
    {
        //Implement this method for each platform separately

    }
}
