using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacerOVR : BlockPlacer
{
    VRController vrController;

    [SerializeField] private VRController.Hand hand = VRController.Hand.Right;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        vrController = FindObjectOfType<VRController>();
    }

    // Update is called once per frame
    void Update()
    {
        PlaceBlock(
            vrController.GetTriggerPressed(hand),
            vrController.GetTriggerHeld(hand),
            vrController.GetTriggerReleased(hand)
            );
        
    }



}
