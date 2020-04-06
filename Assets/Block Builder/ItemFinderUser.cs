using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFinderUser : MonoBehaviour
{
    public enum State { Architect, Search, HomeUI, None, Loading };

    public State state = State.None;

    [SerializeField] private GameObject vrui, homeUI;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetState(State state)
    {
        Debug.Log("SetState to  " + state + " in " + gameObject.name);


        this.state = state;
    }

    public void SetStateToNone()
    {
        state = State.None;
    }

    public void SetStateToHomeUI()
    {
        if (homeUI.activeInHierarchy)
        {
            state = State.HomeUI;
        }
        else
        {
            state = State.None;
        }
    }

    public void SetStateFromBlockPlacer()
    {
        BlockPlacer blockPlacer = FindObjectOfType<BlockPlacer>();

        if (blockPlacer != null && blockPlacer.enabled)
        {
            state = State.Architect;
        }
        else
        {
            state = State.None;
        }
    }

    public void StartSearchState()
    {
        state = State.Search;
    }

    public void StartLoadingState()
    {
        state = State.Loading;
        StartLoadingTimer(1f);
    }

    void StartLoadingTimer(float waitTime)
    {
        IEnumerator timer = LoadingTimer(waitTime);
        StartCoroutine(timer);
        Debug.Log("StartLoadingTimer " + gameObject.name);
    }

    IEnumerator LoadingTimer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //Do something
        state = State.None;
        Debug.Log("FinishedLoadingTimer " + gameObject.name);
    }

}
