using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Room
{
    private List<Item> items;

    private Item[] itemsArray;

    [SerializeField] public string name;
    [SerializeField] string[] itemJson;

    public void LoadItems()
    {
        int count = itemJson.Length;

        itemsArray = new Item[count];

        for (int i = 0; i < count; i++)
        {
            itemsArray[i] = JsonUtility.FromJson<Item>(itemJson[i]);
        }
    }

    public Item[] GetItemsList()
    {
        return itemsArray;
    }

    public Room(string roomName)
    {
        this.name = roomName;
        items = new List<Item>();
    }

    public static Room LoadFromFile(string file)
    {
        if (File.Exists(file))
        {
            Debug.Log("Loading room from file " + file);

            string json = File.ReadAllText(file);

            Debug.Log("json =  " + json);

            Room newRoom = JsonUtility.FromJson<Room>(json);

            int count = newRoom.itemJson.Length;
            Debug.Log("items detected: " + count);

            for (int i = 0; i < count; i++)
            {
                Debug.Log("Item Json " + i + " = " + newRoom.itemJson[i]);
            }

            newRoom.LoadItems();

            return newRoom;
        }
        else
        {
            return null;
        }
    }

    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public void Print()
    {
        string printStr = "";

        int count = itemJson.Length;

        for (int i = 0; i < count; i++)
        {
            printStr += itemJson + "\n";
        }

        Debug.Log("Saved room " + name + "\n" + printStr);
    }

    public void Save(string filename)
    {
        if (!filename.Contains(".json"))
        {
            filename += ".json";
        }

        Debug.Log("Saving room " + name + " to " + filename);

        itemsArray = new Item[items.Count];

        itemJson = new string[items.Count];

        int count = itemsArray.Length;

        for (int i = 0; i < count; i++)
        {
            itemJson[i] = JsonUtility.ToJson(items[i], true);
            Debug.Log("itemJson[" + i + "] =  " + itemJson[i]);
        }

        string jsonData = JsonUtility.ToJson(this, true);
        File.WriteAllText(filename, jsonData);
    }
}
