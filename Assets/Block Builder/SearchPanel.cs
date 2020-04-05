using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchPanel : MonoBehaviour
{
    [SerializeField] private SharedResourcesScriptableObject sharedResource;
    
    VRKeyboard keyboard;
    
    void OnEnable()
    {
        keyboard = sharedResource.GetVRKeyboard();
        keyboard.SetEnterAction(DoSearch);
    }
    
    public void DoSearch()
    {
        sharedResource.CloseVRKeyboard();
        sharedResource.CloseUI();
        //CloseSearchPanel();
    }

    //public void CloseSearchPanel()
    //{
    //    sharedResource.CloseUI();
    //    //gameObject.SetActive(false);
    //    //startPanel.SetActive(true);
    //}
}
