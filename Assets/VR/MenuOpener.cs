using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOpener : MonoBehaviour
{
    public OVRInput.Button openMenuButton;
    public GameObject menu;
    
    AudioSource audioSource;
    public AudioClip openMenuSoundEffect, closeMenuSoundEffect;

    VRController vrController;

    int openMenuButtonIndex = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        if (VR.GetPlatform() == VR.Platform.OculusGo)
            openMenuButtonIndex = 4;

        vrController = FindObjectOfType<VRController>();

        audioSource = GetComponent<AudioSource>();
        
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.minDistance = 500f;
        audioSource.maxDistance = 1000f;
    }

    // Update is called once per frame
    void Update()
    {
        if ( vrController.GetButtonPressed(openMenuButtonIndex) )
        {
            menu.SetActive(!menu.activeSelf);
            if (menu.activeSelf)
            {
                if (openMenuSoundEffect != null)
                {
                audioSource.PlayOneShot(openMenuSoundEffect);
                }
            }
            else
            {
                if (closeMenuSoundEffect != null)
                {
                audioSource.PlayOneShot(closeMenuSoundEffect);
                }
            }
        }
    }
}
