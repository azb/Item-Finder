using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Takes a list of GameObjects and you can tell it to enable only one of them using Enable(gameObject) method

public class TabGroup : MonoBehaviour {

    public GameObject[] gameObjects;

	// Use this for initialization
	void Start () {
		
	}
	
    //Set tab enables this tab and disables all other tabs in the list
    public void SetTab(GameObject tab)
    {
        CloseAllTabs();
        tab.SetActive(true);
    }

    public void CloseAllTabs()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i] == null)
            {
                continue;
            }

            if (gameObjects[i] != gameObject)
            {
                gameObjects[i].SetActive(false);
            }
        }
    }


}
