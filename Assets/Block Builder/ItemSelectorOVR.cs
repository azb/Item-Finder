using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelectorOVR : ItemSelector
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SelectItem(
            OVRInput.GetDown(OVRInput.Button.One),
            OVRInput.Get(OVRInput.Button.One),
            OVRInput.GetUp(OVRInput.Button.One)
            );
    }
}
