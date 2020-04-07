﻿using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [HideInInspector]
    public float viewScale = 5, viewScaleTarget = 5;

    [SerializeField]
    private float
        minZoom = 5,
        maxZoom = 100,
        zoomSensitivity = 5,
        rotateSensitivity = 200;

    public Camera cam;

    [HideInInspector]
    public Vector3 mouse_start_pos;
    [HideInInspector]
    public Vector3 view_start_pos;

    private Vector2 orgBoxPos = Vector2.zero;
    private Vector2 endBoxPos = Vector2.zero;

    Rigidbody rb;

    [HideInInspector]
    public Vector3 camRotation;

    public float zoom, smoothedZoom;

    [HideInInspector]
    public bool movingView,
                movingViewJustFinished,
                movingViewJustStarted,
                movingViewPrev;

    float speed = 500;

    [HideInInspector]
    public float timeSinceMovedView;

    float minHeight;

    public virtual void Start()
    {
        minHeight = transform.position.y;

        cam = GetComponentInChildren<Camera>();

        rb = transform.GetComponent<Rigidbody>();
    }

    void UpdateChildCamPosition()
    {
        cam.transform.localPosition = new Vector3(0, 0, smoothedZoom);  //startCameraPosition + 
    }

    public void SetMovingViewStates(
        bool movingViewButtonDown,
        bool movingViewButton,
        bool movingViewButtonUp
        )
    {
        this.movingViewJustStarted = movingViewButtonDown;
        this.movingView = movingViewButton;
        this.movingViewJustFinished = movingViewButtonUp;

        if (movingView)
            timeSinceMovedView = 0f;
    }

    public void Zoom(float zoomAmount)
    {
        zoom -= (zoom / 2f) * zoomAmount * zoomSensitivity; // * Time.deltaTime;

        if (zoom < minZoom)
        {
            zoom = minZoom;
        }
        if (zoom > maxZoom)
        {
            zoom = maxZoom;
        }
        smoothedZoom += (zoom - smoothedZoom) / 10f;

        if (viewScaleTarget < 1)
            viewScaleTarget = 1;
        if (viewScaleTarget > 40)
            viewScaleTarget = 40;

        UpdateChildCamPosition();
    }

    public void RotateCamera(float rotateAmount)
    {
        camRotation.y -= rotateAmount * rotateSensitivity * Time.deltaTime;
        transform.eulerAngles = camRotation;
    }

    public void MoveCamera(float horizontal, float vertical)
    {
        Vector3 moveVector =
            Quaternion.AngleAxis(camRotation.y, Vector3.up)
            * new Vector3(
                horizontal * speed * (viewScale + 10) * Time.deltaTime,
                0,
                vertical * speed * (viewScale + 10) * Time.deltaTime
                );

        rb.AddForce(moveVector);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (timeSinceMovedView < 10)
            timeSinceMovedView += Time.deltaTime;

        camRotation = transform.eulerAngles;

        //WHEN MIDDLE MOUSE BUTTON IS PRESSED MOVE VIEW
        if (movingViewJustStarted)
        {
            mouse_start_pos = Input.mousePosition;
            view_start_pos = transform.position;
        }

        if (movingView)
        {
            float moveSpeed = 1f / 4000f;

            float x = -(Input.mousePosition.x - mouse_start_pos.x) * moveSpeed;
            float y = minHeight;
            float z = -(Input.mousePosition.y - mouse_start_pos.y) * moveSpeed;

            Vector3 vec = 
                Quaternion.AngleAxis(camRotation.y, Vector3.up) *
                (
                new Vector3(x,y,z) * Mathf.Pow(viewScale, 3)
                )
                + view_start_pos;
            ;
            transform.position = vec;
        }

        EnforceMinimumCameraHeight();
    }

    void EnforceMinimumCameraHeight()
    {
        //SET MINIMUM HEIGHT FOR VIEW
        if (transform.position.y < minHeight)
        {
            transform.position =
                Quaternion.AngleAxis(camRotation.y, Vector3.up)
                * new Vector3(transform.position.x, minHeight, transform.position.z);

            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }
    }
}
