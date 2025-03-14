
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        PlayerDelay();
        

    }

    public void UnLoadCurrentRoom()
    {
        //Destroy(CurrentRoom);
        CurrentRoom.SetActive(false);

    }

    private IEnumerator PlayerDelay()
    {
        yield return new WaitForSeconds(3);
        playerPos.transform.position = Rooms[RoomIndex].playerPos;

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
