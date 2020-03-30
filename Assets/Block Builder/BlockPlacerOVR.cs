using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacerOVR : BlockPlacer
{
    VRController vrController;

    [SerializeField] private VRController.Hand hand = VRController.Hand.Right;

    [SerializeField] private GameObject crossHair;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        vrController = FindObjectOfType<VRController>();
    }
    
    void OnDisable()
    {
        crossHair.SetActive(false);
    }
    void OnEnable()
    {
        crossHair.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        PlaceBlock(
            vrController.GetTriggerPressed(hand),
            vrController.GetTriggerHeld(hand),
            vrController.GetTriggerReleased(hand),
            handTransform.position.y
            );
        
    }



}
