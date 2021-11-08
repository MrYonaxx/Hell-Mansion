using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    [SerializeField]
    RoomDatabase roomDatabase = null;
    [SerializeField]
    GameObject player = null;

    List<Room> levelLayout = new List<Room>();
    List<Room> roomPool = new List<Room>();

    float posX = 0;

    private void Start()
    {
        Initialize();
        CreateRoom();
    }

    // Initialise le pool de room
    private void Initialize()
    {
        roomPool = new List<Room>(roomDatabase.Room.Count);
        for (int i = 0; i < roomDatabase.Room.Count; i++)
        {
            roomPool.Add(roomDatabase.Room[i]);
        }
    }

    // Récupère une room puis la retire de la pool de room pour ne pas retomber 2x sur la même
    public Room GetRoom()
    {
        int r = Random.Range(0, roomPool.Count);
        Room room = roomPool[r];
        roomPool.RemoveAt(r);

        return room;
    }



    public void CreateRoom()
    {
        levelLayout[levelLayout.Count - 1].eventEndRoom.RemoveListener(CreateRoom);

        // On instancie la room
        Room room = Instantiate(GetRoom(), new Vector3(posX, 0, 0), Quaternion.identity);
        room.eventEndRoom.AddListener(CreateRoom);
        posX += 100;
        levelLayout.Add(room);

        StartCoroutine(CreateRoomCoroutine(room));
    }

    private IEnumerator CreateRoomCoroutine(Room room)
    {
        // Fade in
        yield return new WaitForSeconds(1);

        // On initialise tout ni vu ni connu
        player.transform.position = room.StartPosition.position;
        room.StartRoom();

        // Fade out
        yield return new WaitForSeconds(1);
    }
}
