using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = target.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
        transform.position = target.position;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
