using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OVRScroll : MonoBehaviour
{
    [SerializeField] private Scrollbar scrollbar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scrollbar.value += OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y / 10f;
        scrollbar.value += OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y / 10f;
    }
}
