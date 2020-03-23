using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARPointer : Pointer {
    
    private void Start()
    {
    }

    // Update is called once per frame
    void Update () {
        
        Ray ray = new Ray(this.transform.position, transform.forward);
        
        UpdatePointer(ray,
            Input.GetMouseButtonDown(0),
            Input.GetMouseButton(0),
            Input.GetMouseButtonUp(0)
            );
    }
}
