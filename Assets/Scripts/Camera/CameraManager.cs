using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraManager : MonoBehaviour
{
    [Header("Player Object Transform")]
    [SerializeField] public Transform playerPrefab;

    [Header("Vector Attributes")]
    [SerializeField] public Vector3 offset;

    void Update()
    {

        transform.position = playerPrefab.position + offset;
        

        

        //Vector3 newPos = playerPrefab.position + offset;
        //transform.position = newPos;
    }
}





   

