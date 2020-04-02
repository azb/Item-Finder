using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMenuOpener : MonoBehaviour
{
    ItemFinderUser user;

    // Start is called before the first frame update
    void Start()
    {
        user = FindObjectOfType<ItemFinderUser>();
    }

    public void OpenMenu()
    {
        GameObject menu = GameObject.FindWithTag("ItemOptions");
        menu.GetComponent<ItemOptions>().Open(transform);
    }
}
