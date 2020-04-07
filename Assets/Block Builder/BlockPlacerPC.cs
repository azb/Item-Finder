using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class BlockPlacerPC : BlockPlacer
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            PlaceBlock(
                Input.GetMouseButtonDown(0),
                Input.GetMouseButton(0),
                Input.GetMouseButtonUp(0),
                Input.mousePosition.y / 1000f
                );
        }
    }
}
