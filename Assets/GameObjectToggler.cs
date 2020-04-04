using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectToggler : MonoBehaviour
{
    public GameObject[] gameObjectsToToggle;

    // Use this for initialization
    void Start()
    {
    }

    public void Toggle(GameObject gameObjectToEnable)
    {
        for (int i = 0; i < gameObjectsToToggle.Length; i++)
        {
            gameObjectsToToggle[i].SetActive( gameObjectsToToggle[i] == gameObjectToEnable );
        }
    }
}
