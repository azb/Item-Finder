using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPointer : Pointer {
    
    public VRController vrController;

    private void Start()
    {
        if (vrController == null)
        vrController = FindObjectOfType<VRController>(); //GetComponent<VRController>();
    }

    // Update is called once per frame
    void Update () {
        
        Ray ray = new Ray(this.transform.position, transform.forward);
        
        UpdatePointer(ray,
            vrController.triggerRightHand.Pressed(),
            vrController.triggerRightHand.Held(),
            vrController.triggerRightHand.Released()
            );

            //vrController.GetTriggerPressed() || vrController.GetButtonPressed(0),
            //vrController.GetTriggerHeld() || vrController.GetButtonHeld(0),
            //vrController.GetTriggerReleased() || vrController.GetButtonReleased(0)
            //);

        //OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger),
        //OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger),
        //OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger)
        //);
    }
}
