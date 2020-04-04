using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRRotate : MonoBehaviour
{
    VRController vrController;

    bool rotateButtonPressed, rotateButtonHeld, rotateButtonReleased;

    public Transform playArea;

    AudioSource audioSource;
    public AudioClip rotateSoundEffect;

    public float rotateAngle = 30f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        vrController = FindObjectOfType<VRController>();
    }

    enum Direction { Left, Right };

    void Rotate(Direction direction)
    {
        if (direction == Direction.Left)
        {
            playArea.Rotate(new Vector3(0, rotateAngle, 0));
            audioSource.PlayOneShot(rotateSoundEffect);
        }
        else
        {
            playArea.Rotate(new Vector3(0, -rotateAngle, 0));
            audioSource.PlayOneShot(rotateSoundEffect);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!vrController.usesJoystick)
        {
        rotateButtonPressed = 
            (vrController.touchpadRightHand.GetTouchpadPressed() 
            && Mathf.Abs(vrController.touchpadRightHand.padPos.x) > .6f);
        
        rotateButtonHeld = 
            (vrController.touchpadRightHand.GetTouchpadHeld() 
            && Mathf.Abs(vrController.touchpadRightHand.padPos.x) > .6f);
            
        rotateButtonReleased = 
            (vrController.touchpadRightHand.GetTouchpadReleased() 
            && !vrController.touchpadRightHand.swiping);
            
            if (rotateButtonReleased)
            {
                if (vrController.touchpadRightHand.padPosPrev.x > .6f)
                {
                    Rotate(Direction.Left);
                }

                if (vrController.touchpadRightHand.padPosPrev.x < -.6f)
                {
                    Rotate(Direction.Right);
                }
            }
        }

        if (vrController.usesJoystick)
        {
            if (vrController.GetJoystickAxisPressed(2))
            {
                if (vrController.joystick_axis[2] < 0)
                {
                    Rotate(Direction.Right);
                }
                else
                {
                    Rotate(Direction.Left);
                }
            }
        }



    }
}
