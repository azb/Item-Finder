using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FileChooser : MonoBehaviour
{
    [SerializeField] private Transform content, fileChooserButtonPrefab;

    public UnityEvent onSaveFile, onLoadFile, onCancel;

    public string chosenFile;
        
    enum State { Saving, Loading };
    
    State state;

    string[] fileList;

    Transform[] fileButtonList;

    [SerializeField] private InputField fileNameInput;

    VRKeyboard vrKeyboard;

    //save and load button are needed for 
    //enabling / disabling based on which mode is open
    [SerializeField] private GameObject saveButton, loadButton;

    FileInfo[] displayedFiles;

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
    
    // Start is called before the first frame update
    void Start()
    {
        CloseVRKeyboard();
    }
    
    public void OpenSaveFileWindow(UnityEvent onSaveFileCallback, UnityEvent onCancelCallback)
    {
        fileNameInput.text = "";
        saveButton.SetActive(true);
        loadButton.SetActive(false);
        gameObject.SetActive(true);
        onSaveFile = onSaveFileCallback;
        onCancel = onCancelCallback;
    }
    
    public void OpenLoadFileWindow(UnityEvent onLoadFileCallback, UnityEvent onCancelCallback)
    {
        fileNameInput.text = "";
        saveButton.SetActive(false);
        loadButton.SetActive(true);
        Debug.Log("GetsHere14 "+gameObject.name);
        
        gameObject.SetActive(true);
        onLoadFile = onLoadFileCallback;
        onCancel = onCancelCallback;
    }
    
    public void SaveButtonClicked()
    {
        chosenFile = fileNameInput.text;
        Debug.Log("chosenFile = "+chosenFile);

        gameObject.SetActive(false);
        onSaveFile.Invoke();
        CloseVRKeyboard();
    }
    
    public void LoadButtonClicked()
    {
        chosenFile = fileNameInput.text;
        Debug.Log("chosenFile = "+chosenFile);
        
        gameObject.SetActive(false);
        onLoadFile.Invoke();
        CloseVRKeyboard();
    }

    public void CancelButtonClicked()
    {
        gameObject.SetActive(false);
        onCancel.Invoke();
        CloseVRKeyboard();
    }

    void CloseVRKeyboard()
    {
        vrKeyboard = FindObjectOfType<VRKeyboard>();
        if (vrKeyboard != null)
        vrKeyboard.Close();
    }
}
