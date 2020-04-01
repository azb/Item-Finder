using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingLabel : MonoBehaviour
{
    [SerializeField] private GameObject label;

    ItemGameObject item;

    Text text;

    bool startedTimer, stillHovering;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        HideLabel();
    }

    public void SetItem(ItemGameObject item)
    {
        this.item = item;
        VRButton button = item.transform.GetComponent<VRButton>();
        button.onHover.AddListener(ShowLabel);
        button.onEndHover.AddListener(HideLabel);
    }
    
    public void ShowLabel()
    {
        label.SetActive(true);
    }
    
    public void HideLabel()
    {
        label.SetActive(false);
    }
}
