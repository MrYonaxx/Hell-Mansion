using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{

    [SerializeField]
    RoomDatabase roomDatabase = null;
    [SerializeField]
    RoomDatabase enigmeDatabase = null;

    [Space]
    [SerializeField]
    Room bossRoom = null;
    [SerializeField]
    int enigmeRoomInterval = 3;
    [SerializeField]
    int maxRoom = 12;


    [Space]
    [SerializeField]
    PlayerControl player = null;

    [Space]
    [SerializeField]
    Animator animator = null;

    List<Room> levelLayout = new List<Room>();
    List<Room> roomPool = new List<Room>();
    List<Room> roomEnigmePool = new List<Room>();

    [SerializeField]
    AudioClip music = null;

    [SerializeField]
    string ProchainNiveau;

    float posX = 0;
    int roomCount = 0;

    private void Start()
    {
        if(music != null)
            Audio.AudioManager.Instance?.PlayMusic(music, 10);
        Initialize();
        posX += 100;
    }


    // Initialise le pool de room
    private void Initialize()
    {
        roomPool = new List<Room>(roomDatabase.Room.Count);
        for (int i = 0; i < roomDatabase.Room.Count; i++)
        {
            roomPool.Add(roomDatabase.Room[i]);
        }

        roomEnigmePool = new List<Room>(enigmeDatabase.Room.Count);
        for (int i = 0; i < enigmeDatabase.Room.Count; i++)
        {
            roomEnigmePool.Add(enigmeDatabase.Room[i]);
        }
    }


    // Récupère une room puis la retire de la pool de room pour ne pas retomber 2x sur la même
    public Room GetRoom()
    {
        Room room;
        if(roomCount % enigmeRoomInterval == 0)
        {
            int r = Random.Range(0, roomPool.Count);
            room = roomPool[r];
            roomPool.RemoveAt(r);
        }
        else
        {
            int r = Random.Range(0, roomEnigmePool.Count);
            room = roomEnigmePool[r];
            roomEnigmePool.RemoveAt(r);
        }
        

        return room;
    }



    public void CreateRoom()
    {
        if(levelLayout.Count>0)
            levelLayout[levelLayout.Count - 1].eventEndRoom.RemoveListener(CreateRoom);

        roomCount += 1;
        if(roomCount == maxRoom)
        {
            CreateBossRoom();
            return;
        }

        if (roomCount > maxRoom)
        {
            ChangeScene(ProchainNiveau);
            return;
        }

        // On instancie la room
        Room room = Instantiate(GetRoom(), new Vector3(posX, 0, 0), Quaternion.identity);
        room.SetPlayer(player);
        room.eventEndRoom.AddListener(CreateRoom);
        posX += 100;
        levelLayout.Add(room);

        StartCoroutine(CreateRoomCoroutine(room));
    }

    private void CreateBossRoom()
    {
        // On instancie la room
        Room room = Instantiate(bossRoom, new Vector3(posX, 0, 0), Quaternion.identity);
        room.SetPlayer(player);
        room.eventEndRoom.AddListener(CreateRoom);
        posX += 100;
        levelLayout.Add(room);

        StartCoroutine(CreateRoomCoroutine(room));
    }

    public void ChangeScene(string scene)
    {
        ChangeSceneCoroutine(scene);
    }
    private IEnumerator ChangeSceneCoroutine(string scene)
    {
        // Fade in
        player.CanInputPlayer(false);
        animator.SetBool("Fade", true);
        yield return new WaitForSeconds(1);

        // On initialise tout ni vu ni connu
        SceneManager.LoadScene(scene);
    }

    private IEnumerator CreateRoomCoroutine(Room room)
    {
        // Fade in
        player.CanInputPlayer(false);
        animator.SetBool("Fade", true);
        yield return new WaitForSeconds(1);

        // On initialise tout ni vu ni connu
        player.transform.position = room.StartPosition.position + new Vector3(0, 0.2f, 0);
        room.StartRoom();

        // Fade out
        yield return new WaitForSeconds(1);
        animator.SetBool("Fade", false);
        player.CanInputPlayer(true);
    }
}
