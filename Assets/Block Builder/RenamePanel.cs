﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenamePanel : MonoBehaviour
{
    [SerializeField] private SharedResourcesScriptableObject sharedResource;
    
    VRKeyboard keyboard;
    
    void OnEnable()
    {
        keyboard = sharedResource.GetVRKeyboard();
        keyboard.SetEnterAction(DoRename);
    }
    
    public void DoRename()
    {
        sharedResource.selectedItem.Rename(keyboard.inputField.text);
        sharedResource.CloseVRKeyboard();
        sharedResource.CloseUI();
        //CloseRenamePanel();
    }

    //public void CloseRenamePanel()
    //{
    //    sharedResource.CloseUI();
    //    //sharedResource.vruiTabGroup.SetTab(startPanel);
    //    //gameObject.SetActive(false);
    //    //startPanel.SetActive(true);
    //}
}
