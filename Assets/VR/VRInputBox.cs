using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRInputBox : MonoBehaviour
{
    VRKeyboard keyboard;

    VRButton vrButton;

    InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        keyboard = Resources.FindObjectsOfTypeAll<VRKeyboard>()[0];
        vrButton = GetComponent<VRButton>();
        vrButton.onClick.AddListener(OnClick);
        inputField = GetComponent<InputField>();
    }
    
    public void OnClick()
    {
        keyboard.Open(inputField);
    }
}
