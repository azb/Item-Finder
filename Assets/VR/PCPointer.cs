using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCPointer : Pointer {

    public Camera camera;

    // Use this for initialization
    void Start () {
        hitDot.GetComponent<MeshRenderer>().enabled = false;


    }
	
	// Update is called once per frame
	void Update () {
        
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        UpdatePointer(ray,
            Input.GetMouseButtonDown(0),
            Input.GetMouseButton(0),
            Input.GetMouseButtonUp(0)
            );
    }
}
