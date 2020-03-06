using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacer : MonoBehaviour
{
    VRPointer vrPointer;

    VRController vrController;

    [SerializeField] private VRController.Hand hand = VRController.Hand.Right;

    [SerializeField] private Transform blockPrefab;

    // Start is called before the first frame update
    void Start()
    {
        vrPointer = GetComponent<VRPointer>();
        vrController = FindObjectOfType<VRController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (vrController.GetTriggerPressed(hand))
        {
            Instantiate(blockPrefab, vrPointer.point, Quaternion.identity);
        }
    }


}
