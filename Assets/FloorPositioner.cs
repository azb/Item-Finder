using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPositioner : MonoBehaviour
{
    public Transform arCamera, floor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (arCamera.position.y < floor.position.y + .05f)
        {
            floor.position = new Vector3(
                floor.position.x,
                arCamera.position.y - .05f,
                floor.position.z
                );
        }
    }
}
