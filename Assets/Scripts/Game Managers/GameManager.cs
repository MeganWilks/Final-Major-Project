
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [Header("Room Variables")]

    [SerializeField] public Room[] Rooms;
    [SerializeField] public int RoomIndex;
    [SerializeField] public GameObject CurrentRoom;
    [SerializeField] public Camera cam;
    [SerializeField] public GameObject playerPos;

    public void LoadNextRoom()
    {
        RoomIndex++;
        //Instantiate(Rooms[RoomIndex]);
        CurrentRoom = Rooms[RoomIndex].roomPrefab;
        CurrentRoom.SetActive(true);
        cam.transform.position = Rooms[RoomIndex].cameraPos;
        cam.orthographicSize = Rooms[RoomIndex].cameraSize;
        playerPos.transform.position = Rooms[RoomIndex].playerPos;

    }

    public void UnLoadCurrentRoom()
    {
        //Destroy(CurrentRoom);
        CurrentRoom.SetActive(false);

    }
}

[Serializable]
public class Room
{
    public GameObject roomPrefab;
    public Vector3 cameraPos;
    public float cameraSize;
    public Vector3 playerPos;
}
