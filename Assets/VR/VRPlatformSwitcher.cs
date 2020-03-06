using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-30)]
public class VRPlatformSwitcher : MonoBehaviour
{
    public GameObject vrRigSteam, vrRigOVR, arCoreRig;

    public enum Platform { OVR, SteamVR, None };

    public Platform platform;

    public GameObject[] gameObjectsToEnableForOVR;

    // Start is called before the first frame update
    void Awake()
    {
        string model = UnityEngine.XR.XRDevice.model.ToLower();

        Debug.Log("VR Device Model = " + model);

        platform = Platform.None;


        if (model == "")
        {
            gameObject.SetActive(false);
        }
        else
        {
            if (model.Contains("vive"))
            {
                vrRigSteam.SetActive(true);
                vrRigOVR.SetActive(false);
                platform = Platform.SteamVR;
            }
            else
            {
                vrRigSteam.SetActive(false);
                vrRigOVR.SetActive(true);
                platform = Platform.OVR;
            }
        }

        int count = gameObjectsToEnableForOVR.Length;

        for (int i = 0; i < count; i++)
        {
            gameObjectsToEnableForOVR[i].SetActive(platform == Platform.OVR);
        }
    }

}
