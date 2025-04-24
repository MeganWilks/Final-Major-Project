
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [Header("Room Variables")]

    [SerializeField] public Room[] Rooms;
    [SerializeField] public int RoomIndex;
    [SerializeField] public GameObject CurrentRoom;
    [SerializeField] public Camera cam;
    [SerializeField] public GameObject playerPos;




    public void Awake()
    {
        instance = this;
    }

    public void LoadNextRoom()
    {
        RoomIndex++;
        //Instantiate(Rooms[RoomIndex]);
        CurrentRoom = Rooms[RoomIndex].roomPrefab;
        CurrentRoom.SetActive(true);
        cam.transform.position = Rooms[RoomIndex].cameraPos;
        cam.orthographicSize = Rooms[RoomIndex].cameraSize;
        playerPos.GetComponent<CharacterController>().enabled = false;
        playerPos.transform.position = Rooms[RoomIndex].playerPos;
        playerPos.GetComponent<CharacterController>().enabled = true;
        //PlayerDelay();


    }

    public void UnLoadCurrentRoom()
    {
        
        //Destroy(CurrentRoom);
        CurrentRoom.SetActive(false);

    }

    public void LoadNewRoom()
    {
        if (Rooms[RoomIndex].keyToRemove == null) return;
        if (Rooms[RoomIndex].enemiesInRoom != 0) return;
        InventoryManager.instance.Remove(Rooms[RoomIndex].keyToRemove);
        UnLoadCurrentRoom();
        LoadNextRoom();

    }

   // private IEnumerator PlayerDelay()
    //{
    //    yield return new WaitForSeconds(3);
    //    playerPos.transform.position = Rooms[RoomIndex].playerPos;

    //}
        

}

[Serializable]
public class Room
{
    public GameObject roomPrefab;
    public Vector3 cameraPos;
    public float cameraSize;
    public Vector3 playerPos;
    public int enemiesInRoom;
    public ItemClass keyToRemove;

}
