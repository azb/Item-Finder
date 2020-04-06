using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private Transform mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);

        if (Vector3.Distance(
            mainCamera.position + mainCamera.forward * 3f, 
            target.position) > 2f
            )
        {
            transform.position = Vector3.Lerp(
                transform.position,
                mainCamera.position + mainCamera.forward * 3f,
                .1f
                );
        }
        else
        {
            transform.position = Vector3.Lerp(
                transform.position,
                target.position + new Vector3(0,target.localScale.y/2f+.2f,0),
                .1f
                );
        }
    }
}
