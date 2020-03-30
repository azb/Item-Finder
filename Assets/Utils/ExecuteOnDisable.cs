using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExecuteOnDisable : MonoBehaviour
{
    public UnityEvent unityEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnDisable()
    {
        unityEvent.Invoke();
    }
}
