using GoogleARCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCamera : MonoBehaviour
{

    RawImage rawImage;

    [SerializeField] private ARCoreBackgroundRenderer bgroundRenderer;
    // Start is called before the first frame update
    void Start()
    {
        //bgroundRenderer = FindObjectOfType<ARCoreBackgroundRenderer>();
        rawImage = GetComponent<RawImage>();

        rawImage.texture = bgroundRenderer.BackgroundMaterial.mainTexture;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            rawImage.texture = bgroundRenderer.BackgroundMaterial.mainTexture;
        }
    }
}
