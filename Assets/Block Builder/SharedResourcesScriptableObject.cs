using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SharedResources", menuName = "ScriptableObjects/SharedResourcesScriptableObject", order = 1)]
public class SharedResourcesScriptableObject : ScriptableObject
{
    public TabGroup vruiTabGroup;
    public GameObject itemOptionsStartPanel;
    public ItemGameObject selectedItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void OpenItemOptionsTab()
    {
        vruiTabGroup.gameObject.SetActive(true);
        vruiTabGroup.SetTab(itemOptionsStartPanel);
    }

    public void CloseUI()
    {
        vruiTabGroup.gameObject.SetActive(false);
    }




}
