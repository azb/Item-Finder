using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

//This script allows the player to teleport around the map

public class VRTeleporter : MonoBehaviour
{
    Vector3 targetPoint;

    VRController vrController;

    public LineRenderer line;

    public Transform playArea;

    VRPointer[] vrPointers;

    bool teleportArcHitsGround;

    public Material arcCanTeleportMaterial, arcCantTeleportMaterial;

    AudioSource audioSource;
    public AudioClip teleportSoundEffect, powerUpSoundEffect;

    public MonoBehaviour[] disableWhileTeleporting;
    bool[] disableWhileTeleportingStartEnabled;

    public enum TeleportType
    {
        TeleportTypeUseTerrain,
        TeleportTypeUseCollider,
        TeleportTypeUseZeroY
    }

    public TeleportType teleportType = TeleportType.TeleportTypeUseZeroY;

    LayerMask groundLayer;

    Mesh lineMesh;
    int teleportArcLineCount;
    Vector3[] teleportArcLinePositions, finalTeleportArcLinePositions;
    public Transform teleportCylinder;

    bool teleportButtonPressed, teleportButtonHeld, teleportButtonReleased;


    List<Vector3> storedLinePoints;

    void Start()
    {
        storedLinePoints = new List<Vector3>();

        disableWhileTeleportingStartEnabled = new bool[disableWhileTeleporting.Length];

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }


        string[] groundLayerNames = new string[] { "Ground", "Water" };

        groundLayer = LayerMask.GetMask(groundLayerNames);

        //vrPointer = GetComponent<VRPointer>();
        vrPointers = FindObjectsOfType<VRPointer>();

        if (teleportCylinder == null)
        {
            GameObject teleportCylinderGO = GameObject.Find("Teleport Target");
            if (teleportCylinderGO != null)
            {
                teleportCylinder = GameObject.Find("Teleport Target").transform;
            }
        }

        teleportCylinder.gameObject.SetActive(false);

        //playArea = GameObject.Find("VRPlatformCameraRig").transform;

        vrController = FindObjectOfType<VRController>();

        var t = playArea;
        targetPoint = transform.position;

        if (teleportType == TeleportType.TeleportTypeUseTerrain)
        {
            // Start the player at the level of the terrain
            if (t != null)
                t.position = new Vector3(t.position.x, Terrain.activeTerrain.SampleHeight(t.position), t.position.z);
        }
    }

    void OnDisable()
    {
        if (teleportCylinder != null)
            teleportCylinder.gameObject.SetActive(false);
    }

    void OnDisableInHierarchy()
    {

    }

    
    void Teleport(Vector3 point)
    {
        audioSource.PlayOneShot(teleportSoundEffect);
        playArea.position = point;
    }

    int increments = 100;

    void UpdateTeleportArc()
    {
        teleportArcLineCount = 100;

        teleportArcLinePositions = new Vector3[teleportArcLineCount];


        for (int i = 0; i < teleportArcLineCount; i++)
        {
            teleportArcLinePositions[i] = new Vector3(0, -i * i / 50f, i + .05f);
        }

        line.SetVertexCount(teleportArcLineCount);

        line.SetPositions(teleportArcLinePositions);

        lineMesh = new Mesh();
        line.BakeMesh(lineMesh);
    }



    void Update()
    {
        teleportButtonPressed =
            (vrController.touchpadRightHand.GetTouchpadButtonPressed()
            && Mathf.Abs(vrController.touchpadRightHand.padPos.x) < .4f)
            || (vrController.GetJoystickAxisPressed(1) && vrController.usesJoystick);

        teleportButtonHeld =
            (vrController.touchpadRightHand.GetTouchpadButtonHeld()
            && Mathf.Abs(vrController.touchpadRightHand.padPos.x) < .4f)
            || (vrController.GetJoystickAxisHeld(1) && vrController.usesJoystick);

        teleportButtonReleased =
            (vrController.touchpadRightHand.GetTouchpadButtonReleased()
            && Mathf.Abs(vrController.touchpadRightHand.padPos.x) < .4f)
            || (vrController.GetJoystickAxisReleased(1) && vrController.usesJoystick);

        if (teleportButtonPressed)
        {
            int count = disableWhileTeleporting.Length;

            for (int i = 0; i < count; i++)
            {
                disableWhileTeleportingStartEnabled[i] = disableWhileTeleporting[i].enabled;
                disableWhileTeleporting[i].enabled = false;
            }

            audioSource.PlayOneShot(powerUpSoundEffect);
            teleportCylinder.gameObject.SetActive(true);

            count = vrPointers.Length;

            for (int i = 0; i < count; i++)
            {
                vrPointers[i].enabled = false;
            }

            UpdateTeleportArc();

        }

        if (teleportButtonHeld)
        {
            teleportArcHitsGround = false;

            storedLinePoints.Clear();

            for (int i = 0; i < teleportArcLineCount - 1; i++) //?8teleportArcLineCount - 1
            {
                RaycastHit hitinfo;

                storedLinePoints.Add(teleportArcLinePositions[i]);

                if (Physics.Linecast(
                    transform.TransformPoint(teleportArcLinePositions[i]),
                    transform.TransformPoint(teleportArcLinePositions[i + 1]),
                    out hitinfo,
                    groundLayer
                    ))
                {
                    if (hitinfo.transform != null
                            && UnityExtensions.Contains(
                                groundLayer,
                                hitinfo.transform.gameObject.layer
                                ))
                    {
                        teleportArcHitsGround = true;
                        teleportCylinder.position = hitinfo.point;
                        increments = i;
                        break;
                    }
                }
            }

            line.positionCount = (increments);

            for (int i = 0; i < increments; i++)
            {
                line.SetPosition(i, storedLinePoints[i]);
            }

            teleportCylinder.gameObject.SetActive(teleportArcHitsGround);

            if (teleportArcHitsGround)
            {
                line.material = arcCanTeleportMaterial;
            }
            else
            {
                line.material = arcCantTeleportMaterial;
            }
        }

        if (teleportButtonReleased)
        {
            int count = disableWhileTeleporting.Length;

            for (int i = 0; i < count; i++)
            {
                disableWhileTeleporting[i].enabled = disableWhileTeleportingStartEnabled[i];
            }

            count = vrPointers.Length;

            for (int i = 0; i < count; i++)
            {
                vrPointers[i].enabled = true;
            }

            teleportCylinder.gameObject.SetActive(false);
            Vector3[] positions = new Vector3[2];

            positions[0] = new Vector3(0, 0, .05f);
            positions[1] = new Vector3(0, 0, 10);
            line.SetVertexCount(2);
            line.SetPositions(positions);

            if (teleportArcHitsGround)
            {
                Teleport(teleportCylinder.position);
            }
            line.material = arcCanTeleportMaterial;
        }
    }
}
