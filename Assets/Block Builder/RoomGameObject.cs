using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RoomGameObject : MonoBehaviour
{
    //List<ItemGameObject> itemGameObjects;

    [SerializeField] private Transform blockPrefab;

    string roomName = "test room";

    // Start is called before the first frame update
    void Start()
    {

    }

    //public void SetRoom(Room room)
    //{
    //    this.room = room;
    //}

    public void Clear()
    {
        Debug.Log("Clearing room");

        ItemGameObject[] itemGameObjects = FindObjectsOfType<ItemGameObject>();

        int count = itemGameObjects.Length;

        for (int i = 0; i < count; i++)
        {
            Debug.Log("Destroying gameObject " + itemGameObjects[i].gameObject.name);

            GameObject.Destroy(itemGameObjects[i].gameObject);
        }

    }

    public void Save(string filename)
    {
        Room room = new Room("test room");

        ItemGameObject[] itemGameObjects = FindObjectsOfType<ItemGameObject>();

        int count = itemGameObjects.Length;

        for (int i = 0; i < count; i++)
        {
            room.AddItem(itemGameObjects[i].GetItem());
        }

        room.Save(filename);
    }

    public void Load(string filename)
    {
        if (File.Exists(filename))
        {
            Clear();

            Room room = Room.LoadFromFile(filename);

            Item[] saveableItemsArray = room.GetItemsList();

            int count = saveableItemsArray.Length;

            for (int i = 0; i < count; i++)
            {
                Transform newItem = Instantiate(
                    blockPrefab,
                    saveableItemsArray[i].position,
                    Quaternion.identity
                    );
                newItem.localScale = saveableItemsArray[i].scale;
            }
        }
    }

}
