using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenamePanel : MonoBehaviour
{
    [SerializeField] private ItemOptions itemOptions;

    [SerializeField] private GameObject startPanel;
    
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
        CloseRenamePanel();
    }

    public void CloseRenamePanel()
    {
        gameObject.SetActive(false);
        startPanel.SetActive(true);
    }


}
