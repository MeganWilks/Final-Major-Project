using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    void Update()
    {
        Vector3 newPosition = player.position + offset;
        transform.position = newPosition;
    }
}
