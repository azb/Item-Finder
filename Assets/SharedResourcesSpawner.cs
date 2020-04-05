using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SharedResourcesSpawner : MonoBehaviour
{
    [SerializeField] private SharedResourcesScriptableObject sharedResources;
    [SerializeField] private TabGroup vruiTabGroup;
    [SerializeField] private GameObject itemOptionsStartPanel;

    [SerializeField] private InputField renameItemField;

    // Start is called before the first frame update
    void Start()
    {
        sharedResources.vruiTabGroup = vruiTabGroup;
        sharedResources.itemOptionsStartPanel = itemOptionsStartPanel;
    }
    
    public void DeleteSelectedItem()
    {
        Destroy(sharedResources.selectedItem.gameObject);
    }

    public void RenameSelectedItem()
    {
        sharedResources.selectedItem.Rename(renameItemField.text);
        vruiTabGroup.CloseAllTabs();
        vruiTabGroup.gameObject.SetActive(false);
    }

    public void CloseUI()
    {
        vruiTabGroup.CloseAllTabs();
        vruiTabGroup.gameObject.SetActive(false);
    }



}
