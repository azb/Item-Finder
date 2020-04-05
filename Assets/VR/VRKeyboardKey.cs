using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRKeyboardKey : MonoBehaviour
{
    VRKeyboard keyboard;
    
    VRButton vrButton;

    // Start is called before the first frame update
    void Start()
    {
        keyboard = FindObjectOfType<VRKeyboard>();
        vrButton = GetComponent<VRButton>();
        vrButton.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    public void OnClick()
    {
        if (keyboard == null)
        {
            keyboard = FindObjectOfType<VRKeyboard>();
        }

        keyboard.AddCharacter(gameObject);
    }
}
