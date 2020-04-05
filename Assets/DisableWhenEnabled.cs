using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableWhenEnabled : MonoBehaviour
{
    [Tooltip("Gameobjects to disable when the menu is open and re-enable when it is closed again")]
    [SerializeField] private GameObject[] gameObjectToDisable;
    bool[] gameObjectsPreviousState;

    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        Debug.Log("OnEnable "+gameObject.name);


        DisableGameObjects();
    }

    void OnDisable()
    {
        RestoreGameObjectsPriorState();
    }
    
    void DisableGameObjects()
    {
        gameObjectsPreviousState = new bool[gameObjectToDisable.Length];

        int count = gameObjectToDisable.Length;

        for (int i = 0; i < count; i++)
        {
            gameObjectsPreviousState[i] = gameObjectToDisable[i].activeSelf;
            gameObjectToDisable[i].SetActive(false);
        }
    }

    void RestoreGameObjectsPriorState()
    {
        int count = gameObjectToDisable.Length;

        for (int i = 0; i < count; i++)
        {
            gameObjectToDisable[i].SetActive(true); //gameObjectsPreviousState[i]);
        }
    }
}
