using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacer : MonoBehaviour
{
    protected Pointer pointer;

    [SerializeField] private Transform blockPrefab;

    [SerializeField] private Transform handTransform;

    Transform newBlock;

    Vector3 startPoint, handStartPoint;

    protected virtual void Start()
    {
        pointer = GetComponent<Pointer>();
    }

    enum PlacingState { DRAW_XZ, EXTRUDE_Y, NONE };

    PlacingState placingState = PlacingState.NONE;

    protected void PlaceBlock(
        bool placeButtonPressed,
        bool placeButtonHeld,
        bool placeButtonReleased
        )
    {
        if (placingState == PlacingState.NONE)
        {
            if (placeButtonPressed)
            {
                Debug.Log("blockPrefab = "+blockPrefab);
                
                Debug.Log("pointer = "+pointer);
                
                Debug.Log("pointer.point = "+pointer.point);
                
                newBlock = Instantiate(blockPrefab, pointer.point, Quaternion.identity);
                newBlock.GetComponent<Collider>().enabled = false;
                startPoint = pointer.point;
                placingState = PlacingState.DRAW_XZ;
            }
        }
        else
        if (placingState == PlacingState.DRAW_XZ)
        {
            if (newBlock != null)
            {
                newBlock.position = (pointer.point + startPoint) / 2f;
                newBlock.localScale = new Vector3(
                    Mathf.Abs(pointer.point.x - startPoint.x),
                    .1f,
                    Mathf.Abs(pointer.point.z - startPoint.z)
                    );

                if (placeButtonReleased)
                {
                    placingState = PlacingState.EXTRUDE_Y;
                    handStartPoint = handTransform.position;
                }
            }
        }
        else
        if (placingState == PlacingState.EXTRUDE_Y)
        {
            newBlock.localScale = new Vector3(
                    newBlock.localScale.x,
                    Mathf.Abs(handTransform.position.y - handStartPoint.y) * 10f+.1f,
                    newBlock.localScale.z
                    );

            if (placeButtonPressed)
            {
                newBlock.GetComponent<Collider>().enabled = true;
                newBlock = null;
                placingState = PlacingState.NONE;
            }
        }

    }
}
