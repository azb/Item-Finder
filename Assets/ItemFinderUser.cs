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
        
        if (blockPlacer!=null && blockPlacer.enabled)
        {
            state = State.Architect;
        }
        else
        {
            state = State.None;
        }
    }
}
