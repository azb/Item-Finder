using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FileButtons : MonoBehaviour
{
    [SerializeField] private FileChooser fileChooser;
    [SerializeField] private RoomGameObject room;
    [SerializeField] private TabGroup vrui;
    [SerializeField] private GameObject homeUI;

    UnityEvent
        onSaveFileCallback,
        onCancelSaveFileCallback,
        onCancelLoadFileCallback,
        onLoadFileCallback;

    string currentFile = "";

    // Start is called before the first frame update
    void Start()
    {
        onSaveFileCallback = new UnityEvent();
        onCancelSaveFileCallback = new UnityEvent();
        onCancelLoadFileCallback = new UnityEvent();
        onLoadFileCallback = new UnityEvent();

        onSaveFileCallback.AddListener(OnSaveFileCallback);
        onCancelSaveFileCallback.AddListener(OnCancelSaveFileCallback);
        onCancelLoadFileCallback.AddListener(OnCancelLoadFileCallback);
        onLoadFileCallback.AddListener(OnLoadFileCallback);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnSaveFileCallback()
    {
        Debug.Log("OnSaveFileCallback");

        string filename = fileChooser.chosenFile;

        //filename = filename.Replace("\","");
        
        if (filename != "")
        {
            string filenameWithForwardSlashes = filename.Replace("\\","/");
            filename = filenameWithForwardSlashes;

            Debug.Log("checking if filename "+filename+" contains "+Application.persistentDataPath);


            if (!filename.Contains(Application.persistentDataPath))
            {
                Debug.Log("Doesn't contain");
                
                filename = Application.persistentDataPath + "/" + filename;
            }
            else
            {
                Debug.Log("Does contain");
            }

            room.Save(filename);
        }

        
        vrui.SetTab(homeUI);
    }
    
    void OnCancelSaveFileCallback()
    {
        Debug.Log("Saving file cancelled");
        vrui.SetTab(homeUI);

    }

    void OnCancelLoadFileCallback()
    {
        Debug.Log("Loading file cancelled");
        vrui.SetTab(homeUI);
        
    }

    void OnLoadFileCallback()
    {
        Debug.Log("OnLoadFileCallback");
        
        if (fileChooser.chosenFile != "")
        {
            currentFile = fileChooser.chosenFile;
            room.Load(fileChooser.chosenFile);
        }
        
        vrui.SetTab(homeUI);
    }

    public void NewButtonPressed()
    {
        currentFile = "";
        Debug.Log("NewButtonPressed "+gameObject.name);

        room.Clear();
    }

    public void SaveButtonPressed()
    {
        Debug.Log("SaveButtonPressed "+gameObject.name);


        if (currentFile == "")
        {
            fileChooser.OpenSaveFileWindow(
                onSaveFileCallback,
                onCancelSaveFileCallback
                );
        }
        else
        {
            room.Save(currentFile);
        }
    }

    public void SaveAsButtonPressed()
    {
        Debug.Log("SaveAsButtonPressed "+gameObject.name);


        fileChooser.OpenSaveFileWindow(
            onSaveFileCallback,
            onCancelSaveFileCallback
            );
    }

    public void LoadButtonPressed()
    {
        Debug.Log("LoadButtonPressed "+gameObject.name);
        
        fileChooser.OpenLoadFileWindow(
            onLoadFileCallback,
            onCancelLoadFileCallback
            );

        fileChooser.ShowFiles(
            Application.persistentDataPath,
            "*.json"
            );
    }
}
