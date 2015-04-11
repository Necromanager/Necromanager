using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour 
{
	[SerializeField] private string roomName;
	[SerializeField] private RoomEnd[] roomEnds;
	[SerializeField] private Vector2 size;
	[SerializeField] private bool entryRoom = false;
	[SerializeField] private bool oneUse = false;
	[SerializeField] private bool useRoom = true;
	[SerializeField] private int roomID = 0;
	
	private PlayerScript player;
	
	void Start () 
	{
		ShuffleEnds();
	}
	
	public void Setup(int createNum)
	{
		roomID = createNum;
	
		//player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
	}
	
	void Update () 
	{
	
	}
	
	public string GetName()
	{
		return roomName;
	}
	
	public int GetRoomID()
	{
		return roomID;
	}
	
	public bool IsPlayerInRoom()
	{
		//return (player.GetCurRoomID() == roomID);
		return false;
	}
	
	public bool IsEntryRoom()
	{
		return entryRoom;
	}
	
	public bool IsOneUse()
	{
		return oneUse;
	}
	
	public bool CanUse()
	{
		return useRoom;
	}
	
	public Vector2 getSize()
	{
		return size;
	}
	
	public int GetNumEnds()
	{
		return roomEnds.Length;
	}
	
	public RoomEnd GetEnd(Direction dir)
	{
		for (int i=0; i<roomEnds.Length; i++)
		{
			if (roomEnds[i].GetDirection() == dir)
			{
				return roomEnds[i];
			}
		}
		
		return null;
	}
	
	public RoomEnd[] GetEnds()
	{
		return roomEnds;
	}
	
	public bool HasEnd(Direction dir)
	{
		return (GetEnd(dir) != null);
	}
	
	public Vector3 GetEndPosition(Direction dir)
	{
		return GetEnd(dir).transform.position;
	}
	
	public void FindEnds()
	{
		roomEnds = GetComponentsInChildren<RoomEnd>();
		ShuffleEnds();
	}
	
	public void ShuffleEnds()
	{
		for (int i = 0; i < roomEnds.Length; i++) 
		{
			RoomEnd temp = roomEnds[i];
			int randomIndex = Random.Range(i, roomEnds.Length);
			roomEnds[i] = roomEnds[randomIndex];
			roomEnds[randomIndex] = temp;
		}
	}
	
	public void CreateCollider()
	{
		BoxCollider roomArea = gameObject.AddComponent<BoxCollider>();
		roomArea.size = new Vector3(size.x-0.01f,1,size.y-0.01f);
		roomArea.isTrigger = true;
	}
	
	public void ClosePath(RoomEnd roomEnd)
	{
		roomEnd.SetUsed();
		//roomEnd.CreateWall(size);
	}
	
	public void CreateRooms()
	{
		LevelGeneration levelGen = GameObject.FindGameObjectWithTag("Main").GetComponent<LevelGeneration>();
		List<Room> createdRooms = new List<Room>();
		
		for (int i=0; i<roomEnds.Length; i++)
		{
			if (levelGen.CanMakeRoom() && !roomEnds[i].WasUsed())
			{
				roomEnds[i].SetUsed();
				Room newRoom = levelGen.CreateRoom(this, roomEnds[i].GetDirection());
				if (newRoom != null)
				{
					createdRooms.Add(newRoom);
				}
			}
		}
		
		foreach (Room newRoom in createdRooms)
		{
			newRoom.CreateRooms();
		}
		
		for (int i=0; i<roomEnds.Length; i++)
		{
			if (!roomEnds[i].WasUsed())
			{
				ClosePath(roomEnds[i]);
			}
		}
	}
}
