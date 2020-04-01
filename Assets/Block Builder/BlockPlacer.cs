using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacer : MonoBehaviour
{
    protected Pointer pointer;

    [SerializeField] private Transform blockPrefab;

    [SerializeField] protected Transform handTransform;

    Transform newBlock;

    Vector3 startPoint; //, handStartPoint;

    float raiseAmountStart;

    RoomGameObject room;

    protected virtual void Start()
    {
        room = FindObjectOfType<RoomGameObject>();
        pointer = GetComponent<Pointer>();
    }

    void OnDisable()
    {
        if (newBlock != null && placingState != PlacingState.NONE)
        {
            Destroy(newBlock.gameObject);
            placingState = PlacingState.NONE;
        }
    }
    
    enum PlacingState { DRAW_XZ, EXTRUDE_Y, NONE };

    PlacingState placingState = PlacingState.NONE;

    protected void PlaceBlock(
        bool placeButtonPressed,
        bool placeButtonHeld,
        bool placeButtonReleased,
        float raiseAmount
        )
    {
        if (placingState == PlacingState.NONE)
        {
            if (placeButtonPressed)
            {
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
                newBlock.position = (pointer.point + startPoint) / 2f + new Vector3(0,.05f,0);
                newBlock.localScale = new Vector3(
                    Mathf.Abs(pointer.point.x - startPoint.x),
                    .1f,
                    Mathf.Abs(pointer.point.z - startPoint.z)
                    );

                if (placeButtonReleased)
                {
                    placingState = PlacingState.EXTRUDE_Y;
                    //handStartPoint = handTransform.position;
                    raiseAmountStart = raiseAmount;
                }
            }
        }
        else
        if (placingState == PlacingState.EXTRUDE_Y)
        {
            float newHeight = Mathf.Abs(
                raiseAmountStart - raiseAmount
                //handTransform.position.y - handStartPoint.y
                ) * 10f+.1f;

            newBlock.position = new Vector3(
                newBlock.position.x,
                startPoint.y + newHeight / 2,
                newBlock.position.z
                );

            newBlock.localScale = new Vector3(
                    newBlock.localScale.x,
                    newHeight,
                    newBlock.localScale.z
                    );

            if (placeButtonPressed)
            {
                ItemGameObject item = newBlock.GetComponent<ItemGameObject>();
                item.StoreTransform();
                newBlock.GetComponent<Collider>().enabled = true;
                newBlock = null;
                placingState = PlacingState.NONE;
            }
        }
    }
}
