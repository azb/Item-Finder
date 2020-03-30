using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRUIRotation : MonoBehaviour
{
    public Transform centerEyeAnchor;
    
    public float angularThreshold = 15f;

    public float minY;
    float minYRelative;

    public bool dontGoLowerThanStartYPosition;

    [SerializeField] private bool movement = true, rotation = true;

    // Start is called before the first frame update
    void Start()
    {
        if (dontGoLowerThanStartYPosition)
        {
            minY = transform.position.y;
        }

        if (centerEyeAnchor == null)
        {
            centerEyeAnchor = GameObject.Find("CenterEyeAnchor").transform;
        }

        //minYRelative = transform.position.y - minY;
    }

    void OnEnable()
    {
        if (centerEyeAnchor != null)
        {
            transform.position = MenuCameraPosition();
            
            transform.rotation = Quaternion.Euler(0, GetRotationTarget(), 0);
        }
    }

    Vector3 MenuCameraPosition()
    {
        return new Vector3(
            centerEyeAnchor.position.x,
            Mathf.Clamp(centerEyeAnchor.position.y, minY, float.MaxValue),
            centerEyeAnchor.position.z);
    }

    float GetRotationTarget()
    {
        float rotationTarget = 0;

        float current_ry, eye_ry;
        current_ry = transform.rotation.eulerAngles.y;
        eye_ry = centerEyeAnchor.rotation.eulerAngles.y;

        float x1, y1, x2, y2;

        x1 = Mathf.Cos(current_ry * Mathf.Deg2Rad);
        y1 = Mathf.Sin(current_ry * Mathf.Deg2Rad);
        x2 = Mathf.Cos(eye_ry * Mathf.Deg2Rad);
        y2 = Mathf.Sin(eye_ry * Mathf.Deg2Rad);

        //if (Vector2.Distance(new Vector2(x1, y1), new Vector2(x2, y2)) > Mathf.Sin(Mathf.Deg2Rad * angularThreshold)) //.3f)
        //{
            rotationTarget = Mathf.Round(eye_ry / angularThreshold) * angularThreshold;
        //}

        return rotationTarget;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (movement)
        {
            if (Vector3.Distance(transform.position, centerEyeAnchor.position) > .2f)
            {
                transform.position = Vector3.Lerp(transform.position, MenuCameraPosition(), .1f);
            }
        }

        if (rotation)
        {
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.Euler(0, GetRotationTarget(), 0),
                .1f
                );
        }
        //Quaternion.Euler(0,Mathf.Lerp(current_ry, rotation_target, .1f),0);
    }
}
