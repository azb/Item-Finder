using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMenuOpener : MonoBehaviour
{
    ItemFinderUser user;

    [SerializeField] private SharedResourcesScriptableObject sharedResources;

    // Start is called before the first frame update
    void Start()
    {
        user = FindObjectOfType<ItemFinderUser>();
    }

    public void OpenMenu()
    {
        if (user.state == ItemFinderUser.State.None)
        {
        sharedResources.selectedItem = transform.GetComponent<ItemGameObject>();
        sharedResources.OpenItemOptionsTab();
        }
    }
}
