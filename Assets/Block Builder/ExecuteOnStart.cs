using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExecuteOnStart : MonoBehaviour {

    public UnityEvent actions;

	// Use this for initialization
	void Start () {
		actions.Invoke();
	}
}
