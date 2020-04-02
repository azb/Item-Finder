using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenamePanel : MonoBehaviour
{
    [SerializeField] private ItemOptions itemOptions;
    
    VRKeyboard keyboard;
    
    void OnEnable()
    {
        keyboard = FindObjectOfType<VRKeyboard>();   
        keyboard.SetEnterAction(DoRename);
    }
    
    public void DoRename()
    {
        itemOptions.RenameItem(keyboard.inputField.text);
        keyboard.Close();
        itemOptions.Close();
    }
}
