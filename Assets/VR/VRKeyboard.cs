using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VRKeyboard : MonoBehaviour
{
    public InputField inputField;

    [SerializeField] private GameObject vrKeyboardCanvas, vrui;

    private UnityEvent onHitEnterButton;

    [SerializeField] private GameObject[] letterKeys;

    bool uppercase;

    // Start is called before the first frame update
    void Awake()
    {
        onHitEnterButton = new UnityEvent();
        SwitchCase(uppercase);
    }

    void SwitchCase(bool newCase)
    {
        int count = letterKeys.Length;

        for (int i = 0; i < count; i++)
        {
            if (uppercase)
            {
                letterKeys[i].name = letterKeys[i].name.ToUpper();
            }
            else
            {
                letterKeys[i].name = letterKeys[i].name.ToLower();
            }
        }
    }
    
    public void AddCharacter(GameObject character)
    {
        if (inputField == null)
        {
            Debug.LogError("No input field set for vr keyboard");
        }
        else
        {
            if (character.name == "BACKSPACE")
            {
                if (inputField.text.Length > 0)
                {
                    inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
                }
            }
            else
            if (character.name == "CLEAR")
            {
                inputField.text = "";
            }
            else
            if (character.name == "SHIFT")
            {
                uppercase = !uppercase;
                SwitchCase(uppercase);
            }
            else
            if (character.name == "ENTER")
            {
                if (onHitEnterButton != null)
                {
                    onHitEnterButton.Invoke();
                }
            }
            else
            {
                inputField.text += character.name;
            }
        }
    }

    public void SetEnterAction(UnityAction onHitEnterButton)
    {
        this.onHitEnterButton.AddListener(onHitEnterButton);
    }

    public void Close()
    {
        vrKeyboardCanvas.SetActive(false);
    }

    public void Open(InputField inputField)
    {
        vrKeyboardCanvas.SetActive(true);
        vrui.SetActive(true);
        this.inputField = inputField;
    }
}
