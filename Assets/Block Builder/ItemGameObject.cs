using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGameObject : MonoBehaviour
{
    Item item;
    
    // Start is called before the first frame update
    void Start()
    {
        item = new Item("new item");
    }
    
    public Item GetItem()
    {
        return item;
    }
    
    public void StoreTransform()
    {
        //Debug.Log("storing transform "+gameObject.name);
        item.StoreTransform(transform.position, transform.localScale);
    }
    
    public void Rename(string newName)
    {
        item.itemName = newName;
    }


}
