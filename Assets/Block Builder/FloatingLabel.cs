using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingLabel : MonoBehaviour
{
    [SerializeField] private GameObject label;

    bool startedTimer, stillHovering;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Show()
    {
        if (!startedTimer)
        {
            label.SetActive(true);
            StartHideLabelTimer(.1f);
            startedTimer = true;
            //transform.localScale = Vector3.one;
        }
        else
        {
            stillHovering = true;
        }
    }

    void StartHideLabelTimer(float waitTime)
    {
        IEnumerator timer = Timer(waitTime);
        StartCoroutine(timer);
    }

    IEnumerator Timer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //Do something
        if (stillHovering)
        {
            StartHideLabelTimer(.1f);
            stillHovering = false;
        }
        else
        {
            label.SetActive(false);
            startedTimer = false;
        }
    }
}
