using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static OVRInput;

public class VRControllerOVR : VRController
{

    bool _triggerHeld, _triggerHeldPrev;

    enum ControllerType { OculusGo, OculusRift, GearVR, HTCVive };

    ControllerType controllerType;
    
    public override void Start()
    {
        base.Start();

        if (VR.GetPlatform() == VR.Platform.None)
        {
            this.enabled = false;
            return;
        }
        
        if (VR.GetPlatform() == VR.Platform.OculusGo)
        {
            controllerType = ControllerType.OculusGo;
            //Debug.Log("Found controller type OculusGo");
        }
        else
        {
            controllerType = ControllerType.OculusRift;
            //Debug.Log("Found controller type OculusRift");
        }


        Vibrate(10000);
        

        joystick_axis_sensitivity[0] = .1f;

        if (controllerType == ControllerType.OculusRift)
        {
            usesJoystick = true;
        }
        else
        {
            usesJoystick = false;
        }
    }

    public override void SetTriggerStates()
    {
        bool
            triggerJustPressed,
            triggerHeld,
            triggerJustReleased;
        
        if (controllerType == ControllerType.OculusGo)
        {
            triggerJustPressed = 
                OVRInput.GetDown(
                    OVRInput.Button.PrimaryIndexTrigger
                    );

            triggerHeld = 
                OVRInput.Get(
                    OVRInput.Button.PrimaryIndexTrigger
                    );

            triggerJustReleased = 
                OVRInput.GetUp(
                    OVRInput.Button.PrimaryIndexTrigger
                    );
        }
        else
        {
            triggerJustPressed = 
                OVRInput.GetDown(
                    OVRInput.Button.SecondaryIndexTrigger
                    );

            triggerHeld = 
                OVRInput.Get(
                    OVRInput.Button.SecondaryIndexTrigger
                    );

            triggerJustReleased = 
                OVRInput.GetUp(
                    OVRInput.Button.SecondaryIndexTrigger
                    );
        }
        
        triggerRightHand.SetTriggerStateFromBools(
            triggerJustPressed,
            triggerHeld,
            triggerJustReleased
            );
        
        float rightHandTriggerPos = OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger);
        float leftHandTriggerPos = OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger);

        triggerLeftHand.Set(leftHandTriggerPos);
        triggerRightHand.Set(rightHandTriggerPos);
    }

    public override void SetButtonStates()
    {
        buttonPressed[0] = OVRInput.GetDown(OVRInput.Button.One);
        buttonPressed[1] = OVRInput.GetDown(OVRInput.Button.Two);
        buttonPressed[2] = OVRInput.GetDown(OVRInput.Button.Three);
        buttonPressed[3] = OVRInput.GetDown(OVRInput.Button.Four);
        buttonPressed[4] = OVRInput.GetDown(OVRInput.Button.Back);
        buttonPressed[5] = OVRInput.GetDown(OVRInput.Button.Start);

        buttonHeld[0] = OVRInput.Get(OVRInput.Button.One);
        buttonHeld[1] = OVRInput.Get(OVRInput.Button.Two);
        buttonHeld[2] = OVRInput.Get(OVRInput.Button.Three);
        buttonHeld[3] = OVRInput.Get(OVRInput.Button.Four);
        buttonHeld[4] = OVRInput.Get(OVRInput.Button.Back);
        buttonHeld[5] = OVRInput.Get(OVRInput.Button.Start);


        buttonReleased[0] = OVRInput.GetUp(OVRInput.Button.One);
        buttonReleased[1] = OVRInput.GetUp(OVRInput.Button.Two);
        buttonReleased[2] = OVRInput.GetUp(OVRInput.Button.Three);
        buttonReleased[3] = OVRInput.GetUp(OVRInput.Button.Four);
        buttonReleased[4] = OVRInput.GetUp(OVRInput.Button.Back);
        buttonReleased[5] = OVRInput.GetUp(OVRInput.Button.Start);

    }
    
    public override void SetJoystickStates()
    {
        Vector2 joystickPosRight = OVRInput.Get(
            OVRInput.Axis2D.PrimaryThumbstick
            );
        
        //JOYSTICKS
        Vector2 leftThumbstick = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);
        Vector2 rightThumbstick = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);

        SetJoystickAxis(0, rightThumbstick.x);
        SetJoystickAxis(1, rightThumbstick.y);

        SetJoystickAxis(2, leftThumbstick.x);
        SetJoystickAxis(3, leftThumbstick.y);

    }

    public override void SetTouchpadStates()
    {
        
        Vector2 padPosOVR = OVRInput.Get(
            OVRInput.Axis2D.PrimaryTouchpad,
            OVRInput.Controller.RTrackedRemote
            );

        //SetJoystickAxis(0, padPosOVR.x);
        //SetJoystickAxis(1, padPosOVR.y);

        
        touchpadRightHand.SetTouchpadPosition(padPosOVR);
        
        touchpadRightHand.SetTouchpadPressed(OVRInput.GetDown(OVRInput.Touch.PrimaryTouchpad));
        touchpadRightHand.SetTouchpadHeld(OVRInput.Get(OVRInput.Touch.PrimaryTouchpad));
        touchpadRightHand.SetTouchpadReleased(OVRInput.GetUp(OVRInput.Touch.PrimaryTouchpad));

        touchpadRightHand.SetTouchpadButtonPressed(OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad));
        touchpadRightHand.SetTouchpadButtonHeld(OVRInput.Get(OVRInput.Button.PrimaryTouchpad));
        touchpadRightHand.SetTouchpadButtonReleased(OVRInput.GetUp(OVRInput.Button.PrimaryTouchpad));

        //TOUCHPADS
        //Vector2 leftTouchpad = OVRInput.Get(OVRInput.RawAxis2D.LTouchpad);
        //Vector2 rightTouchpad = OVRInput.Get(OVRInput.RawAxis2D.RTouchpad);

        //SetJoystickAxis(4, rightTouchpad.x);
        //SetJoystickAxis(5, rightTouchpad.y);

        //SetJoystickAxis(6, leftTouchpad.x);
        //SetJoystickAxis(7, leftTouchpad.y);

    }

    public override void Vibrate(ushort durationMicroSec)
    {
        OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
        StartCoroutine(StartVibrateTimer(durationMicroSec));
    }

    IEnumerator StartVibrateTimer(ushort durationMicroSec)
    {
        float sec = durationMicroSec / 1000000f;

        yield return new WaitForSeconds(sec);

        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
    }



}
