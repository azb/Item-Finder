using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: take zoom related functionality out of BuildingPlacerVRSwiper class and move it here

public class VRZoom : MonoBehaviour
{

    VRController vrController;

    float scale, startY;

    float maxScale = 100, minScale = 1;

    public Transform playArea, vrHead;

    public bool smoothMoveEnabled;

    // Use this for initialization
    void Start()
    {
        vrController = GetComponent<VRController>();
        startY = playArea.position.y;
    }

    // Update is called once per frame
    void Update()
    {

        if(smoothMoveEnabled)
        {
        float jxLeft = vrController.GetJoystickAxis(2);
        float jyLeft = vrController.GetJoystickAxis(3);

        float jxRight = vrController.GetJoystickAxis(0);
        float jyRight = vrController.GetJoystickAxis(1);

        float handTriggerPrimary = vrController.GetJoystickAxis(0);

        //scale += jy * Time.deltaTime;

        if (scale < minScale)
        {
            scale = minScale;
        }

        if (scale > maxScale)
        {
            scale = maxScale;
        }

        Vector3 prevPosition = vrHead.position;

        //playArea.position = playArea.position + new Vector3(0,jyLeft * Time.deltaTime * 10f,0); //localScale = Vector3.one * scale;

        if (!vrController.touchpadRightHand.startedSwipingHorizontally)
            playArea.position += transform.forward * jyRight * Time.deltaTime * 50f;

        if (vrController.touchpadRightHand.GetTouchpadButtonPressed())
        {
            if (vrController.joystick_axis[0] > .75f)
            playArea.Rotate(new Vector3(0,45,0));
            if (vrController.joystick_axis[0] < -.75f)
            playArea.Rotate(new Vector3(0,-45,0));
        }
        //playArea.position += transform.right * jxRight;

        //playArea.position = new Vector3(playArea.position.x, playArea.position.y, scale); // vrHead.position - prevPosition;

        //vrHead.position = new Vector3(prevPosition.x, vrHead.position.y, prevPosition.z);

        }

    }
}


