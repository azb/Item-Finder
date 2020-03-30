using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSwitcher : MonoBehaviour
{
    public GameObject augmentedRealityRig, virtualRealityRig;
    
    // Start is called before the first frame update
    void Start()
    {
        virtualRealityRig.SetActive(false);
        augmentedRealityRig.SetActive(false);

        if (VR.GetPlatform() == VR.Platform.None)
        {
            augmentedRealityRig.SetActive(true);
        }
        else
        {
            virtualRealityRig.SetActive(true);
        }

        //if (platform == Platform.AugmentedReality)
        //{
        //    augmentedRealityRig.SetActive(true);
        //}

        //if (platform == Platform.VirtualReality)
        //{
        //    virtualRealityRig.SetActive(true);
        //}
    }
}
