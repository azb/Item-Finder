using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public bool hitInfoInitialized;

    public Vector3 point = Vector3.zero;

    public Transform hitDot;

    public RaycastHit hitInfo;

    VRButton currentHoverButton;

    
    private void OnEnable()
    {
        hitDot.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        if (hitDot != null)
        {
        hitDot.gameObject.SetActive(false);
        }
    }

    
    // Update is called once per frame
    public void UpdatePointer(Ray ray, bool pointerPressed, bool pointerHeld, bool pointerReleased)
    {
        Plane plane = new Plane(Vector3.up, 0);

        string[] layerNames = new string[] { "UI", "Ground", "Water", "VROnly" };

        int layerMask = LayerMask.GetMask(layerNames);

        float maxDistance = 1000;

        if (Physics.Raycast(ray, out hitInfo, maxDistance, layerMask))
        {
            hitInfoInitialized = true;

            if (!hitDot.gameObject.activeSelf)
            {
            hitDot.gameObject.SetActive(true);
            }
            //Debug.Log("hitInfo.normal = "+hitInfo.normal);


            hitDot.rotation = Quaternion.Euler( hitInfo.normal );

            point = hitInfo.point;

            //if (Vector3.Distance(hitDot.position, hitInfo.point) < 10f)
            //{ 
            //    hitDot.position = Vector3.Lerp( 
            //        hitDot.position , 
            //        hitInfo.point , 
            //        15f * Time.deltaTime
            //        );
            //}
            //else
            //{   
                hitDot.position = hitInfo.point;
            //}

            hitDot.transform.localScale = 
                new Vector3(.01f, .01f, .01f) 
                + new Vector3(.01f, .01f, .01f) 
                * Vector3.Distance(hitDot.position, transform.position);

            Transform hitTransform = hitInfo.transform;
            VRButton button = hitTransform.GetComponent<VRButton>();

            if (button != currentHoverButton)
            {
                if (currentHoverButton != null)
                {
                    currentHoverButton.OnEndHover();
                }
                currentHoverButton = button;
            }

            if (button != null)
            {
                if (pointerPressed) //OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
                {
                    //InGameDebug d = GameObject.FindGameObjectWithTag("InGameDebugger").GetComponent<InGameDebug>();
                    //d.Log("OnClick in VRPointer.cs");
                    button.OnClick();
                }
                else
                if (pointerHeld) //OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
                {
                    //button.OnHold();
                }
                else
                if (pointerReleased) //OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
                {
                    button.OnRelease();
                }
                else
                {
                    button.OnHover();
                }
            }
        }
        else
        {
            if (hitDot.gameObject.activeSelf)
            {
            hitDot.gameObject.SetActive(false);
            }

            if (currentHoverButton != null)
            {
            currentHoverButton.OnEndHover();
            currentHoverButton = null;
            }
        }
    }
}
