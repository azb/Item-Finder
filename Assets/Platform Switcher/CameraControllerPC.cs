using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;

public class CameraControllerPC : CameraController
{
    
    // Update is called once per frame
    public override void Update()
    {

        SetMovingViewStates(
            Input.GetMouseButtonDown(2),
            Input.GetMouseButton(2),
            Input.GetMouseButtonUp(2)
            );
        
        base.Update();
        
        if (!Input.GetKey(KeyCode.LeftControl))
            {
            Zoom(Input.GetAxis("Mouse ScrollWheel"));
            }

        //MOVE VIEW USING ARROW KEYS OR WASD
        
        MoveCamera(
            Input.GetAxis("Horizontal") , 
            Input.GetAxis("Vertical")
        );

        RotateCamera(
            InputHorizontalRotation() //Input.GetAxisRaw("Horizontal Rotation")
        );
        
    }

    float InputHorizontalRotation()
    {
        float rotation = 0f;
        if (Input.GetKey(KeyCode.Q))
        {
            rotation -= 1f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotation += 1f;
        }
        return rotation;
    }


}
