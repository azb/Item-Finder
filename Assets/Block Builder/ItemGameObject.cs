using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGameObject : MonoBehaviour
{
    Item item;

    public FloatingLabel floatingLabel;

    bool initialized;

    void Start()
    {
        Initialize();
    }

    void SetFloatingLabel()
    {

    }

    // Start is called before the first frame update
    void Initialize()
    {
        if (!initialized)
        {
            item = new Item("new item");
            initialized = true;
        }
    }

    public Item GetItem()
    {
        if (!initialized)
        {
            Initialize();
        }

        StoreTransform();
        return item;
    }

    public void StoreTransform()
    {
        //Debug.Log("storing transform "+gameObject.name);
        if (item != null)
        {
            item.StoreTransform(transform.position, transform.localScale);
        }
    }

    public void Rename(string newName)
    {
        item.itemName = newName;
    }

}
