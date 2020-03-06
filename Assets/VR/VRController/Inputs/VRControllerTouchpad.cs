using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRControllerTouchpad : MonoBehaviour
{

    //Touchpad Swipe
    public bool
        touchSwipedLeft,
        touchSwipedRight,
        swiping;

    public bool swipingHorizontally,
                swipingVertically,
                padTouched,
                padQuickTouched;

    //Touchpad (AXIS 2D)
    public bool
        touchpadPressed,
        touchpadHeld,
        touchpadReleased,
        touchpadButtonPressed,
        touchpadButtonHeld,
        touchpadButtonReleased;

    public float touchpadHeldTime, lastSwipeDistance;

    public Vector2 padPos, padPosPrev, padStartPos, padLastSwipedPosition;

    public bool startedSwipingVertically, startedSwipingHorizontally;

    VRController.Hand hand;

    public void SetHand(VRController.Hand hand)
    {
        this.hand = hand;
    }

    // Start is called before the first frame update
    void Start()
    {

    }


    public void SetTouchpadPosition(float x, float y)
    {
        padPos.x = x;
        padPos.y = y;
    }

    public void SetTouchpadPosition(Vector2 position)
    {
        padPos = position;
    }

    // Update is called once per frame
    void Update()
    {
        if (padPos != Vector2.zero)
        {
            padPosPrev = padPos;
        }

        //padPos = new Vector2(GetJoystickAxis(0), GetJoystickAxis(1));

        FirstSwipeDirectionDetector();

        if (touchpadHeld)
        {
            touchpadHeldTime += Time.deltaTime;
        }
        else
        {
            touchpadHeldTime = 0;
        }

        DetectSwipes();
    }


    void DetectSwipes()
    {
        touchSwipedLeft = false;
        touchSwipedRight = false;

        if (touchpadPressed)
        {
            swiping = false;
            padStartPos = padPos;
            padLastSwipedPosition = padPos;
        }
        else
        if (touchpadHeld)
        {
            if (padPos.x - padLastSwipedPosition.x < -.7f)
            {
                swiping = true;
                touchSwipedLeft = true;
                padLastSwipedPosition = padPos;
            }

            if (padPos.x - padLastSwipedPosition.x > .7f)
            {
                swiping = true;
                touchSwipedRight = true;
                padLastSwipedPosition = padPos;
            }
        }

        if (touchpadReleased)
        {
            if (touchpadHeldTime < .2f)
                padQuickTouched = true;
            else
                padQuickTouched = true;
            
        }
    }




    void FirstSwipeDirectionDetector()
    {
        //lastPx = px;
        //lastPy = py;
        //px = GetJoystickAxis(0);
        //py = GetJoystickAxis(1);

        if (!startedSwipingHorizontally && !startedSwipingVertically)
        {
            if (Mathf.Abs(padPos.y) > .5f)
            {
                startedSwipingVertically = true;
            }
            else
            if (Mathf.Abs(padPos.x) > .2f)
            {
                startedSwipingHorizontally = true;
            }
        }

        if ((startedSwipingHorizontally || startedSwipingVertically) &&
            (Mathf.Abs(padPos.x) < .1f && Mathf.Abs(padPos.y) < .1f))
        {
            startedSwipingHorizontally = false;
            startedSwipingVertically = false;
        }
    }

    public void SetTouchpadPressed(bool pressed)
    {
        touchpadPressed = pressed;
    }

    public void SetTouchpadReleased(bool released)
    {
        touchpadReleased = released;
    }

    public void SetTouchpadButtonHeld(bool held)
    {
        touchpadButtonHeld = held;
    }


    public void SetTouchpadButtonPressed(bool pressed)
    {
        touchpadButtonPressed = pressed;
    }

    public void SetTouchpadButtonReleased(bool released)
    {
        touchpadButtonReleased = released;
    }

    public void SetTouchpadHeld(bool held)
    {
        touchpadHeld = held;
    }


    public bool GetTouchpadPressed()
    {
        return touchpadPressed;
    }

    public bool GetTouchpadReleased()
    {
        return touchpadReleased;
    }

    public bool GetTouchpadHeld()
    {
        return touchpadHeld;
    }

    public bool GetTouchpadButtonPressed()
    {
        return touchpadButtonPressed;
    }

    public bool GetTouchpadButtonReleased()
    {
        return touchpadButtonReleased;
    }

    public bool GetTouchpadButtonHeld()
    {
        return touchpadButtonHeld;
    }
}
