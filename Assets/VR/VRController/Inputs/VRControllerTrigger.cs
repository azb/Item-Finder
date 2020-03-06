using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRControllerTrigger : MonoBehaviour
{
    public enum State { Inactive, Pressed, Held, Released }

    private State state;

    float position, prevPosition;

    float deadzone = .3f;

    float lastSwipeTime;
    
    VRController.Hand hand;

    public void SetHand(VRController.Hand hand)
    {
        this.hand = hand;
    }
    
    //public VRControllerTrigger(VRController.Hand hand)
    //{
    //    this.hand = hand;
    //}

    public void SetTriggerState(State state)
    {
        this.state = state;
    }

    public void SetTriggerStateFromBools(bool pressed, bool held, bool released)
    {
        if (pressed)
        {
            SetTriggerState(VRControllerTrigger.State.Pressed);
        }
        else if (held)
        {
            SetTriggerState(VRControllerTrigger.State.Held);
        }
        else if (released)
        {
            SetTriggerState(VRControllerTrigger.State.Released);
        }
        else
        {
            SetTriggerState(VRControllerTrigger.State.Inactive);
        }
    }

    public State GetTriggerState()
    {
        return state;
    }

    public bool Pressed()
    {
        return state == State.Pressed;
    }

    public bool Held()
    {
        return state == State.Held;
    }

    public bool Released()
    {
        return state == State.Released;
    }

    public void Set(float position, bool setStateFromPosition = false)
    {
        this.prevPosition = this.position;
        this.position = position;

        if (setStateFromPosition)
        {
            if (prevPosition < deadzone && position >= deadzone)
            {
                state = State.Pressed;
            }
            else if (position >= deadzone)
            {
                state = State.Held;
            }
            else if (prevPosition >= deadzone)
            {
                state = State.Released;
            }
        }
    }

    public float Get()
    {
        return position;
    }
}
