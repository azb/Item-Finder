using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VRButton : MonoBehaviour
{
    //public MonoBehaviour onClickAction;

    public UnityEvent onClick, onRelease, onHover, onEndHover;

    public Material restTex, hoverTex, clickTex;

    Vector3 startPos, targetPos;

    float timeSinceHovered = 0;

    AudioSource audioSource;
    public AudioClip hoverSoundEffect, clickSoundEffect;

    MeshRenderer meshRenderer;

    bool alreadyHovering;

    Button button;

    // Use this for initialization
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        button = GetComponent<Button>();


        //GameObject uiSoundEffectsObject = GameObject.FindWithTag("UI Sound Effects");

        //if (uiSoundEffectsObject != null)
        //{
        //    audioSource = uiSoundEffectsObject.GetComponent<AudioSource>();
        //}
        //else
        //{
        //    audioSource = GetComponent<AudioSource>();
        //}

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.minDistance = 500f;
        audioSource.maxDistance = 1000f;

        targetPos = transform.localPosition;
        startPos = transform.localPosition;

        if (meshRenderer != null && restTex!=null)
        {
            meshRenderer.material = restTex;
        }
    }

    // Update is called once per frame

    void Update()
    {
        if (timeSinceHovered < 1f)
        {
            timeSinceHovered += Time.deltaTime;
        }
        else
        {
            //transform.localPosition = (transform.localPosition + targetPos) / 2;
            //targetPos = startPos;
        }
    }

    public void OnHover()
    {
        Debug.Log("OnHover " + gameObject.name);
        if (button != null)
        {
            button.Select();
        }

        onHover.Invoke();
        timeSinceHovered = 0;
        //targetPos = startPos + new Vector3(0, .05f, 0);
        if (meshRenderer != null && hoverTex != null)
        {
            meshRenderer.material = hoverTex;
        }

        if (!alreadyHovering)
        {
            if (hoverSoundEffect != null)
            {
                audioSource.PlayOneShot(hoverSoundEffect);
            }

            alreadyHovering = true;
        }
    }

    public void OnRelease()
    {
        onRelease.Invoke();

        if (clickSoundEffect != null)
        {
            audioSource.PlayOneShot(clickSoundEffect);
        }

        if (meshRenderer != null && restTex != null)
        {
            meshRenderer.material = restTex;
        }
    }

    public void OnEndHover()
    {
        onEndHover.Invoke();
        alreadyHovering = false;

        if (meshRenderer != null && restTex != null)
        {
            meshRenderer.material = restTex;
        }
    }

    public void OnClick()
    {
        Debug.Log("OnClick " + gameObject.name);

        //targetPos = startPos + new Vector3(0, 0, -.05f);

        if (meshRenderer != null && clickTex != null)
        {
            meshRenderer.material = clickTex;
        }

        if (button != null)
        {
            button.onClick.Invoke();
        }

        onClick.Invoke();
    }
}