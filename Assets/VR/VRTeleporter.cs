using UnityEngine;
using System.Collections;
using System;

//This script allows the player to teleport around the map

public class VRTeleporter : MonoBehaviour
{
    Vector3 targetPoint;

    VRController vrController;

    public LineRenderer line;

    public Transform playerHead, playArea;

    VRPointer vrPointer;

    bool teleportArcHitsGround;

    public Material arcCanTeleport, arcCantTeleport;

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
    Vector3[] teleportArcLinePositions;
    public Transform teleportCylinder;

    bool teleportButtonPressed, teleportButtonHeld, teleportButtonReleased;

    void Start()
    {
        disableWhileTeleportingStartEnabled = new bool[disableWhileTeleporting.Length];

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }


        string[] groundLayerNames = new string[] { "Ground", "Water" };

        groundLayer = LayerMask.GetMask(groundLayerNames);

        vrPointer = GetComponent<VRPointer>();

        if (teleportCylinder == null)
        {
            GameObject teleportCylinderGO = GameObject.Find("Teleport Target");
            if (teleportCylinderGO != null)
            {
                teleportCylinder = GameObject.Find("Teleport Target").transform;
            }
        }

        teleportCylinder.gameObject.SetActive(false);

        playArea = GameObject.Find("VRPlatformCameraRig").transform;

        vrController = GetComponent<VRController>();

        var t = playArea;
        targetPoint = transform.position;

        if (teleportType == TeleportType.TeleportTypeUseTerrain)
        {
            // Start the player at the level of the terrain
            if (t != null)
                t.position = new Vector3(t.position.x, Terrain.activeTerrain.SampleHeight(t.position), t.position.z);
        }
    }

    void OnEnable()
    {
        if (teleportCylinder != null)
            teleportCylinder.gameObject.SetActive(true);
    }

    void OnDisable()
    {
        if (teleportCylinder != null)
            teleportCylinder.gameObject.SetActive(false);
    }

    void Teleport(Vector3 point)
    {
        audioSource.PlayOneShot(teleportSoundEffect);

        Vector3 offset = playArea.position - playerHead.position;

        playArea.position = point;
    }

    void Teleport()
    {
        Vector3 offset = playArea.position - playerHead.position;

        //Teleport if the pad is pressed
        //if (teleportOnClick)
        //{
        // First get the current Transform of the the reference space (i.e. the Play Area, e.g. CameraRig prefab)
        var t = playArea; //steamPlayAreaReference;
        if (t == null)
        {
            Debug.Log("Can't teleport - play area not found");
            return;
        }

        // Get the current Y position of the reference space
        float refY = t.position.y;

        // Create a plane at the Y position of the Play Area
        // Then create a Ray from the origin of the controller in the direction that the controller is pointing
        Plane plane = new Plane(Vector3.up, -refY);
        Ray ray = new Ray(this.transform.position, transform.forward);

        // Set defaults
        bool hasGroundTarget = false;
        float dist = 0f;
        if (teleportType == TeleportType.TeleportTypeUseTerrain) // If we picked to use the terrain
        {
            RaycastHit hitInfo;
            TerrainCollider tc = Terrain.activeTerrain.GetComponent<TerrainCollider>();
            hasGroundTarget = tc.Raycast(ray, out hitInfo, 1000f);
            dist = hitInfo.distance;
        }
        else if (teleportType == TeleportType.TeleportTypeUseCollider) // If we picked to use the collider
        {
            RaycastHit hitInfo;
            hasGroundTarget = Physics.Raycast(ray, out hitInfo);
            dist = hitInfo.distance;
        }
        else // If we're just staying flat on the current Y axis
        {
            // Intersect a ray with the plane that was created earlier
            // and output the distance along the ray that it intersects
            hasGroundTarget = plane.Raycast(ray, out dist);
        }
        teleportArcHitsGround = hasGroundTarget;
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

        //if (teleportButtonPressed)
        //    Debug.Log("teleportButtonPressed");
        //if (teleportButtonHeld)
        //    Debug.Log("teleportButtonHeld");
        //if (teleportButtonReleased)
        //    Debug.Log("teleportButtonReleased");


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

            vrPointer.enabled = false;

            teleportArcLinePositions = new Vector3[100];

            teleportArcLineCount = teleportArcLinePositions.Length;

            for (int i = 0; i < teleportArcLineCount; i++)
            {
                teleportArcLinePositions[i] = new Vector3(0, -i * i / 50f, i * 3 + .05f);
            }

            line.SetVertexCount(teleportArcLineCount);

            line.SetPositions(teleportArcLinePositions);

            lineMesh = new Mesh();
            line.BakeMesh(lineMesh);
        }

        if (teleportButtonHeld)
        {
            //teleportCylinder.position = transform.TransformPoint(teleportArcLinePositions[1]);

            teleportArcHitsGround = false;

            for (int i = 0; i < teleportArcLineCount - 1; i++)
            {
                RaycastHit hitinfo;

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
                        break;
                    }
                }
            }
            teleportCylinder.gameObject.SetActive(teleportArcHitsGround);

            if (teleportArcHitsGround)
            {
                line.material = arcCanTeleport;
            }
            else
            {
                line.material = arcCantTeleport;
            }
        }

        if (teleportButtonReleased)
        {

            int count = disableWhileTeleporting.Length;

            for (int i = 0; i < count; i++)
            {
                disableWhileTeleporting[i].enabled = disableWhileTeleportingStartEnabled[i];
            }

            vrPointer.enabled = true;
            teleportCylinder.gameObject.SetActive(false);
            Vector3[] positions = new Vector3[2];

            positions[0] = new Vector3(0, 0, +.05f);
            positions[1] = new Vector3(0, 0, 10);
            line.SetVertexCount(2);
            line.SetPositions(positions);

            if (teleportArcHitsGround)
            {
                Teleport(teleportCylinder.position + new Vector3(0, 5, 0));
            }
            line.material = arcCanTeleport;
        }
    }
}
