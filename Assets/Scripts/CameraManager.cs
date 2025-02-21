using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Player Object Transform")]
    [SerializeField] public Transform playerPrefab;

    [Header("Vector Attributes")]
    [SerializeField] public Vector3 offset;

    void Update()
    {
        Vector3 newPos = playerPrefab.position + offset;
        transform.position = newPos;
    }
}
