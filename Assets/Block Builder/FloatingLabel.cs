using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingLabel : MonoBehaviour
{
    [SerializeField] private GameObject label;

    ItemGameObject item;

    [SerializeField] private Text text;

    bool startedTimer, stillHovering;

    [SerializeField] private SharedResourcesScriptableObject sharedResource;

    // Start is called before the first frame update
    void Start()
    {
        HideLabel();
    }

    public void SetItem(ItemGameObject item)
    {
        this.item = item;
        item.floatingLabel = this;
        VRButton button = item.transform.GetComponent<VRButton>();
        button.onHover.AddListener(ShowLabel);
        button.onEndHover.AddListener(HideLabel);
    }

    public void ShowLabel()
    {
        if (sharedResource.user.state == ItemFinderUser.State.None)
        {
            label.transform.localPosition = new Vector3(0, item.transform.localScale.y + .2f, 0);
            text.text = item.GetItem().itemName;
            label.SetActive(true);
        }
    }

    public void HideLabel()
    {
        if (sharedResource.selectedItem != item)
        {
            label.SetActive(false);
        }
    }
}
