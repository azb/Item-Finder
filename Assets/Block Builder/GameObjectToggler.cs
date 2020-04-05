using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectToggler : MonoBehaviour
{
    public GameObject[] gameObjectsToToggle;

    // Use this for initialization
    void Start()
    {
        Debug.Log("GameObjectToggler GetsHere12 " + gameObject.name);


    }

    public void Toggle()
    {
        Debug.Log("Toggle GetsHere13 " + gameObject.name);


        for (int i = 0; i < gameObjectsToToggle.Length; i++)
        {
            Debug.Log("gameObjectsToToggle[" + i + "].activeSelf = "
                + gameObjectsToToggle[i].activeSelf);

            gameObjectsToToggle[i].SetActive(
                !gameObjectsToToggle[i].activeSelf
            );
        }
    }
}
