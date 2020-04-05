using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SharedResources", menuName = "ScriptableObjects/SharedResourcesScriptableObject", order = 1)]
public class SharedResourcesScriptableObject : ScriptableObject
{
    public TabGroup vruiTabGroup;
    public GameObject itemOptionsStartPanel;
    public ItemGameObject selectedItem;

    VRKeyboard vrKeyboard;

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
        CloseVRKeyboard();
    }

    public void CloseVRKeyboard()
    {
        if (vrKeyboard != null)
        {
            vrKeyboard.Close();
        }
        else
        {
            Debug.LogError("VR keyboard not set");
        }
    }
    
    public void SetVRKeyboard(VRKeyboard vrKeyboard)
    {
        this.vrKeyboard = vrKeyboard;
    }

    public VRKeyboard GetVRKeyboard()
    {
        return vrKeyboard;
    }
}
