using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiater : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void DoInstantiate()
    {
        GameObject newObj = Instantiate(
            prefab,
            transform.position,
            Quaternion.identity
            );

        newObj.GetComponent<FollowObject>()
            .target = transform;

        newObj.GetComponent<FloatingLabel>()
            .SetItem(GetComponent<ItemGameObject>());
    }
}
