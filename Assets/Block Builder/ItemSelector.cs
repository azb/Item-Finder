using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelector : MonoBehaviour
{
    [SerializeField] private Pointer pointer;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    protected void SelectItem(
        bool selectPressed, 
        bool selectHeld, 
        bool selectReleased
        )
    {
        //if (selectPressed)
        //{
            if (pointer.hitInfoInitialized)
            {
                if (pointer.hitInfo.transform != null)
                {
                    Transform hitTransform = pointer.hitInfo.transform;

                    FloatingLabel floatingLabel = hitTransform.GetComponentInChildren<FloatingLabel>();

                    if (floatingLabel != null)
                    {
                        floatingLabel.Show();
                    }
                }
            }
        //}
    }
}
