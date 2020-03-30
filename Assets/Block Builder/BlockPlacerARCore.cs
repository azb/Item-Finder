using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacerARCore : BlockPlacer
{
    bool touchPressed, touchHeld, touchReleased;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Debug.Log("GetsHere2 Start "+gameObject.name);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touchPressed = Input.GetTouch(0).phase == TouchPhase.Began;
            touchReleased = Input.GetTouch(0).phase == TouchPhase.Ended;
            
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touchHeld = true;
                Debug.Log("touchPressed "+gameObject.name);
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                touchHeld = false;
                Debug.Log("touchReleased "+gameObject.name);
            }
        }

        PlaceBlock(
            touchPressed,
            touchHeld,
            touchReleased,
            handTransform.position.y
            //Input.GetTouch(0).phase == TouchPhase.Ended
            //Input.GetMouseButtonDown(0),
            //Input.GetMouseButton(0),
            //Input.GetMouseButtonUp(0)
            );
    }
}
