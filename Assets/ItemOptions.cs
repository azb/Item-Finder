using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOptions : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    ItemFinderUser user;

    ItemGameObject selectedItem;

    // Start is called before the first frame update
    void Start()
    {
        user = FindObjectOfType<ItemFinderUser>();
    }

    public void Open(Transform item)
    {
        selectedItem = item.GetComponent<ItemGameObject>();
        
        if (user.state == ItemFinderUser.State.None)
        {
            panel.gameObject.SetActive(true);

            GameObject mainCamera = GameObject.FindWithTag("MainCamera");

            transform.position = (item.position + mainCamera.transform.position) / 2f;
        }
    }

    public void Close()
    {
        panel.gameObject.SetActive(false);
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
