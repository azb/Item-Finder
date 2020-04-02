using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OVRButtonAction : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] componentsToToggle;
    [SerializeField] private GameObject[] gameObjectsToToggle;

    [SerializeField] private OVRInput.Button button;

    [SerializeField] private UnityEvent action;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(button))
        {
            int count = componentsToToggle.Length;

            for(int i=0;i<count;i++)
            {
                componentsToToggle[i].enabled = !componentsToToggle[i].enabled;
            }
            
            count = gameObjectsToToggle.Length;

            for(int i=0;i<count;i++)
            {
                gameObjectsToToggle[i].SetActive(!gameObjectsToToggle[i].activeSelf);
            }
            
            action.Invoke();
        }
    }
}
