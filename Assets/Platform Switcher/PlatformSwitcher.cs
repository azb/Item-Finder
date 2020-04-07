using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSwitcher : MonoBehaviour
{
    public GameObject augmentedRealityRig, virtualRealityRig, pcRig;

    public BuildSettingsScriptableObject buildSettings;

    // Start is called before the first frame update
    void Start()
    {
        virtualRealityRig.SetActive(false);
        augmentedRealityRig.SetActive(false);

        switch (buildSettings.buildMode)
        {
            case BuildSettingsScriptableObject.BuildMode.PCFlatMonitor:

                pcRig.SetActive(true);

                break;

            case BuildSettingsScriptableObject.BuildMode.PhoneAugmentedReality:

                augmentedRealityRig.SetActive(true);

                break;

            case BuildSettingsScriptableObject.BuildMode.VirtualReality:

                virtualRealityRig.SetActive(true);

                break;
        }
    }
}
