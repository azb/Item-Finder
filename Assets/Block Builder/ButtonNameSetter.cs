using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ButtonNameSetter : MonoBehaviour
{
    [SerializeField] private Text buttonLabel;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonLabel != null)
        {
            buttonLabel.gameObject.name = gameObject.name+" Label";
            buttonLabel.text = gameObject.name.Replace(" Button", "");
        }
    }
}
