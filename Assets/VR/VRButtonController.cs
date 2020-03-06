using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRButtonController : MonoBehaviour {

    public UnityAction onClick;

    public string lastButtonClicked = "";

    public void OnClick(string buttonName)
    {
        onClick.Invoke();
        //lastButtonClicked = buttonName;
    }
}
