using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchPanel : MonoBehaviour
{
    [SerializeField] private SharedResourcesScriptableObject sharedResource;

    [SerializeField] private InputField inputField;

    [SerializeField] private GameObject[] searchResultsButtons;

    VRKeyboard keyboard;

    List<ItemGameObject> results;

    void OnEnable()
    {
        keyboard = sharedResource.GetVRKeyboard();
        //keyboard.SetEnterAction(DoSearch);

        inputField.onValueChanged.AddListener(
            PopulateSearchResults
            );
    }

    void HideAllResultsButtons()
    {
        int count = searchResultsButtons.Length;

        for (int i = 0; i < count; i++)
        {
            searchResultsButtons[i].SetActive(false);
        }
    }
    
    public void PopulateSearchResults(string query)
    {
        if (query == "")
        {
            HideAllResultsButtons();
        }
        else
        {
            ItemGameObject[] itemGameObjects = FindObjectsOfType<ItemGameObject>();

            int count = itemGameObjects.Length;

            results = new List<ItemGameObject>();

            for (int i = 0; i < count; i++)
            {
                if (itemGameObjects[i].GetItem().itemName.Contains(query))
                {
                    results.Add(itemGameObjects[i]);
                    if (results.Count >= 5)
                    {
                        break;
                    }
                }
            }

            for (int i = 0; i < searchResultsButtons.Length; i++)
            {
                if (i < results.Count)
                {
                    searchResultsButtons[i].SetActive(true);
                    searchResultsButtons[i].name = results[i].GetItem().itemName;
                }
                else
                {
                    searchResultsButtons[i].SetActive(false);
                }
            }
        }
    }

    public void DoSearch(int i)
    {
        sharedResource.selectedItem = results[i];
        sharedResource.SetArrowTarget(results[i].transform);
        sharedResource.CloseVRKeyboard();
        sharedResource.CloseUI();
    }
}
