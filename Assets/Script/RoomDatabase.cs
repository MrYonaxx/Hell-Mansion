using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomDatabase", menuName = "RoomDatabase", order = 1)]
public class RoomDatabase : ScriptableObject
{
    [SerializeField]
    private List<Room> roomDatabase;
    public List<Room> Room
    {
        get { return roomDatabase; }
    }
}
