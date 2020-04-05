using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemOptions : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    ItemFinderUser user;

    ItemGameObject selectedItem;

    [SerializeField] private GameObject vrui;

    [SerializeField] private UnityEvent onOpenItemMenu, onCloseItemMenu;

    [SerializeField] private SharedResourcesScriptableObject sharedResources;

    // Start is called before the first frame update
    void Start()
    {
        user = FindObjectOfType<ItemFinderUser>();
    }

    public void Open()
    {
        selectedItem = sharedResources.selectedItem; //item.GetComponent<ItemGameObject>();
        
        vrui.SetActive(true);

        Debug.Log("GetsHere16 "+gameObject.name);


        if (user.state == ItemFinderUser.State.None)
        {
            Debug.Log("GetsHere15 "+gameObject.name);


            //panel.gameObject.SetActive(true);
            
            onOpenItemMenu.Invoke();

            GameObject mainCamera = GameObject.FindWithTag("MainCamera");
            
            transform.position = (selectedItem.transform.position + mainCamera.transform.position) / 2f;
        }
    }

    public void Close()
    {
        //panel.gameObject.SetActive(false);
        onCloseItemMenu.Invoke();
    }

    public void RenameItem(string newName)
    {
        selectedItem.Rename(newName); 
    }

    public void DeleteItem()
    {
        Destroy(selectedItem.gameObject);
    }
}
