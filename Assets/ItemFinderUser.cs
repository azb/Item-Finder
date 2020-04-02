using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFinderUser : MonoBehaviour
{
    public enum State { Architect, Search, None };

    public State state = State.None;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void SetState(State state)
    {
        this.state = state;
    }

    public void SetStateFromBlockPlacer()
    {
        BlockPlacer blockPlacer = FindObjectOfType<BlockPlacer>();

        Debug.Log("blockPlacer = "+blockPlacer);

        if (blockPlacer!=null)
        {
            Debug.Log("blockPlacer.enabled = "+blockPlacer.enabled);


        }

        if (blockPlacer!=null && blockPlacer.enabled)
        {
            Debug.Log("blockPlacer is active");
            
            state = State.Architect;
        }
        else
        {
            Debug.Log("blockPlacer is inactive");
            
            state = State.None;
        }
    }




}
