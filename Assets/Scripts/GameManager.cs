
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [Header("Room Variables")]

    [SerializeField] public GameObject[] Rooms;
    [SerializeField] public int RoomIndex;
    [SerializeField] public GameObject CurrentRoom;

    public void LoadNextRoom()
    {
        RoomIndex++;
        //Instantiate(Rooms[RoomIndex]);
        CurrentRoom = Rooms[RoomIndex];
        CurrentRoom.SetActive(true);

    }

    public void UnLoadCurrentRoom()
    {
        //Destroy(CurrentRoom);
        CurrentRoom.SetActive(false);

    }
}
