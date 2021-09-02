using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;
    private Vector3 offset;

    public float smoothSpeed = 0.15f;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        if (target.position.y > -20)
        {
            Vector3 desiredPosition = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }
        else
        {
            //transform.position = new Vector3(target.position.x, 0f, target.position.z)+ offset;
            transform.position = Vector3.Lerp(transform.position, new Vector3(PlayerController.playerPositionX, 4.5f, target.position.z)+offset, smoothSpeed);
        }
    }
}
