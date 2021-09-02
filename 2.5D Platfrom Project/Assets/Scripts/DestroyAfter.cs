using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    public float lifeDuration;
    void Start()
    {
        Destroy(gameObject, lifeDuration);
    }
}
