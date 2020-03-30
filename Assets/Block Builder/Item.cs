using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public class Item
{
    [SerializeField] public string itemName;
    [SerializeField] public Vector3 position, scale;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StoreTransform(Vector3 position, Vector3 scale)
    {
        //Debug.Log("storing transform "+gameObject.name);
        
        this.position = position; //gameObject.transform.position;
        this.scale = scale; //gameObject.transform.localScale;
    }
    
    public Item(string name)
    {
        itemName = name;
    }
}
