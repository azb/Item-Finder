﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FileChooser : MonoBehaviour
{
    [SerializeField] private Transform content, fileChooserButtonPrefab;

    [HideInInspector]
    public UnityEvent onSaveFile, onLoadFile, onCancel;
    
    [HideInInspector]
    public string chosenFile;
        
    enum State { Saving, Loading };
    
    State state;

    string[] fileList;

    Transform[] fileButtonList;

    [SerializeField] private InputField fileNameInput;

    //save and load button are needed for 
    //enabling / disabling based on which mode is open
    [SerializeField] private GameObject saveButton, loadButton;

    FileInfo[] displayedFiles;

    [SerializeField] private TabGroup tabGroup;

    [SerializeField] private SharedResourcesScriptableObject sharedResource;
    
    // Start is called before the first frame update
    void Start()
    {

    }
    
    //ShowFiles
    //Search pattern examples: 
    // *.* for files of any type
    // *.txt for txt files
    
    public void ShowFiles(string directory, string searchPattern)
    {
        ClearFileList();

        DirectoryInfo dir = new DirectoryInfo(directory);
        displayedFiles = dir.GetFiles(searchPattern);

        int count = displayedFiles.Length;

        fileButtonList = new Transform[count];

        for (int i = 0; i < count; i++)
        {
            fileButtonList[i] = Instantiate(fileChooserButtonPrefab, content);
            Text buttonLabel = fileButtonList[i].GetComponentInChildren<Text>();
            buttonLabel.text = displayedFiles[i].Name;
            Button newButton = fileButtonList[i].GetComponent<Button>();

            int currentIndex = i;

            newButton.onClick.AddListener(
                delegate{SelectFile(currentIndex);}
                );

        }
    }

    void SelectFile(int i)
    {
        fileNameInput.text = displayedFiles[i].FullName;
        Debug.Log("SelectFile chosenFile = "+chosenFile);
    }
    
    void ClearFileList()
    {
        if (fileButtonList == null)
            return;

        int count = fileButtonList.Length;

        for(int i=0;i<count;i++)
        {
            Destroy(fileButtonList[i].gameObject);
        }
    }
    
    
    public void OpenSaveFileWindow(UnityEvent onSaveFileCallback, UnityEvent onCancelCallback)
    {
        fileNameInput.text = "";
        saveButton.SetActive(true);
        loadButton.SetActive(false);
        //gameObject.SetActive(true);
        tabGroup.SetTab(gameObject);

        onSaveFile = onSaveFileCallback;
        onCancel = onCancelCallback;
    }
    
    public void OpenLoadFileWindow(UnityEvent onLoadFileCallback, UnityEvent onCancelCallback)
    {
        fileNameInput.text = "";
        saveButton.SetActive(false);
        loadButton.SetActive(true);
        Debug.Log("GetsHere14 "+gameObject.name);
        
        //gameObject.SetActive(true);
        tabGroup.SetTab(gameObject);

        onLoadFile = onLoadFileCallback;
        onCancel = onCancelCallback;
    }
    
    public void SaveButtonClicked()
    {
        chosenFile = fileNameInput.text;
        Debug.Log("chosenFile = "+chosenFile);

        gameObject.SetActive(false);
        onSaveFile.Invoke();
        sharedResource.CloseVRKeyboard();
    }
    
    public void LoadButtonClicked()
    {
        chosenFile = fileNameInput.text;
        Debug.Log("chosenFile = "+chosenFile);
        
        gameObject.SetActive(false);
        onLoadFile.Invoke();
        sharedResource.CloseVRKeyboard();
    }

    public void CancelButtonClicked()
    {
        gameObject.SetActive(false);

        //It makes sense for onCancel to be a UnityEvent so that 
        //whatever UI called the file browser can set it's onCancel event
        onCancel.Invoke();
        sharedResource.CloseVRKeyboard();
    }
}
