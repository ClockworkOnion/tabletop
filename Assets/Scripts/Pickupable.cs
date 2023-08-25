using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    Vector3 initialScale;
    Vector3 newScale;

    public void Awake()
    {
        initialScale = transform.localScale;
        //newScale = Vector3.Scale(newScale, new Vector3(1.5f, 1.5f, 1.5f));

    }
    public bool positionLocked = false; // TODO: Allow host to lock position

    public void EnlargeWobble() {
        transform.localScale = new Vector3(1, 1, 1);
    }
}
