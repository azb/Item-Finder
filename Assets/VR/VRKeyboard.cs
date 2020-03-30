using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRKeyboard : MonoBehaviour
{
    public InputField inputField;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetActiveInputField(InputField inputField)
    {
        Debug.Log("SetActiveInputField " + inputField.gameObject.name);

        transform.parent.gameObject.SetActive(inputField != null);

        this.inputField = inputField;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddCharacter(GameObject character)
    {
        if (inputField == null)
        {
            inputField = FindObjectOfType<InputField>();
        }

        if (character.name == "BACKSPACE")
        {
            if (inputField.text.Length > 0)
            {
                inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
            }
        }
        else
        {
            inputField.text += character.name;
        }
    }
}
