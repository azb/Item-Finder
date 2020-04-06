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

    [SerializeField] private VRKeyboard vrKeyboard;

    [SerializeField] private Transform arrowTarget;
    [SerializeField] private GameObject arrow;

    [SerializeField] private ItemFinderUser user;

    // Start is called before the first frame update
    void Start()
    {
        sharedResources.vruiTabGroup = vruiTabGroup;
        sharedResources.itemOptionsStartPanel = itemOptionsStartPanel;
        sharedResources.SetVRKeyboard(vrKeyboard);
        sharedResources.arrowTarget = arrowTarget;
        sharedResources.arrow = arrow;
        sharedResources.user = user;
    }

    public void ShowWhereObjectIs()
    {
        arrow.SetActive(true);
        arrowTarget.position = sharedResources.selectedItem.transform.position;
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

    public void SetInputFieldTextToSelectedItemName()
    {
        renameItemField.text = sharedResources.selectedItem.GetItem().itemName;
    }
    
    public void CloseVRKeyboard()
    {
        sharedResources.CloseVRKeyboard();
    }

    public void HideSelectedObjectLabel()
    {
        if (sharedResources.selectedItem != null)
        {
            FloatingLabel label = sharedResources.selectedItem.floatingLabel;

            sharedResources.selectedItem = null;

            label.HideLabel();
        }

        FloatingLabel[] floatingLabels = FindObjectsOfType<FloatingLabel>();

        int count = floatingLabels.Length;

        for(int i=0;i<count;i++)
        {
            floatingLabels[i].HideLabel();
        }
    }
}
