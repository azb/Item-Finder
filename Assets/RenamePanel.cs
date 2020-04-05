using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenamePanel : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;

    [SerializeField] private SharedResourcesScriptableObject sharedResource;
    
    VRKeyboard keyboard;
    
    void OnEnable()
    {
        keyboard = Resources.FindObjectsOfTypeAll<VRKeyboard>()[0];
        keyboard.SetEnterAction(DoRename);
    }
    
    public void DoRename()
    {
        sharedResource.selectedItem.Rename(keyboard.inputField.text);
        keyboard.Close();
        sharedResource.CloseUI();
        CloseRenamePanel();
    }

    public void CloseRenamePanel()
    {
        gameObject.SetActive(false);
        startPanel.SetActive(true);
    }


}
