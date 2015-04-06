using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGeneration : MonoBehaviour 
{
	/*private const int MIN_ROOMS = 4;
	private const int MAX_ROOMS = 6;
	private const int MIN_GRAVES = 7;
	private const int MAX_GRAVES = 12;*/

	[SerializeField] private int roomsSmall;
	[SerializeField] private int roomsNormal;
	[SerializeField] private int roomsLarge;

	[SerializeField] private DifficultyValues numGraves;
	[SerializeField] private DifficultyValues numZombies;
	
	[SerializeField] private int housesPerSide;
	//[SerializeField] private int gravesSmall;
	//[SerializeField] private int gravesNormal;
	//[SerializeField] private int gravesLarge;
	
	//[SerializeField] private int zombiesSmall;
	//[SerializeField] private int zombiesNormal;
	//[SerializeField] private int zombiesLarge;
	//[SerializeField] private List<int> maxGraves;
	
	private List<string> usedRooms;
	private List<Bounds> allHouseBounds;
	private Object[] rooms;
	private int numRooms;
	private int maxGraves;
	private int createdRooms;
	private Vector3 startRoomPos;
	private Vector2 upperBounds;
	private Vector2 lowerBounds;

	[SerializeField] protected bool isTutorial = false;

	void Start () 
	{

	}
	
	void Update () 
	{
	
	}
	
	private void GetSizeNums()
	{
		//numRooms = Random.Range(MIN_ROOMS*GlobalValues.size,MAX_ROOMS*GlobalValues.size);
		//maxGraves = Random.Range(MIN_GRAVES*GlobalValues.size,MAX_GRAVES*GlobalValues.size);
		switch (GlobalValues.size)
		{
		case 1:
			numRooms = roomsSmall;
			//maxGraves = gravesSmall;
			break;
		case 2:
			numRooms = roomsNormal;
			//maxGraves = gravesNormal;
			break;
		case 3:
			numRooms = roomsLarge;
			//maxGraves = gravesLarge;
			break;
		default:
			numRooms = 0;
			//maxGraves = 0;
			break;
		}

		maxGraves = (int)numGraves.getVal (GlobalValues.size, GlobalValues.difficulty);
	}
	
	public void GenerateLevel()
	{
		createdRooms = 0;
		GetSizeNums();
		usedRooms = new List<string>();
		rooms = Resources.LoadAll("Rooms/");
		Room startRoom = GameObject.FindGameObjectWithTag("Room").GetComponent<Room>();
		startRoom.CreateCollider();
		startRoom.Setup(0);
		startRoomPos = startRoom.transform.position;
		
		Bounds sb = startRoom.GetComponent<BoxCollider>().bounds;
		upperBounds = new Vector2(startRoomPos.x+(sb.extents.x+0.5f), startRoomPos.z+(sb.extents.z+0.5f));
		lowerBounds = new Vector2(startRoomPos.x-(sb.extents.x+0.5f), startRoomPos.z-(sb.extents.z+0.5f));
		Debug.Log("Start:");
		Debug.Log("Upper: "+upperBounds.x+" "+upperBounds.y);
		Debug.Log("Lower: "+lowerBounds.x+" "+lowerBounds.y);

		if(!isTutorial){ startRoom.CreateRooms(); };

		ReplaceProxies();
		DestroyObjects();
		FillGraves();
		CheckOpenings();
		CreateWalls();
		CreateGround();
		//FillGraves();
		CreateHouses();
		
		Debug.Log("End:");
		Debug.Log("Upper: "+upperBounds.x+" "+upperBounds.y);
		Debug.Log("Lower: "+lowerBounds.x+" "+lowerBounds.y);
	}
	
	public bool CanMakeRoom()
	{
		return (createdRooms < numRooms);
	}
	
	private void DestroyObjects()
	{
		GameObject[] obs = GameObject.FindGameObjectsWithTag("Grave");
		
		if (obs.Length > maxGraves)
		{
			List<GameObject> graves = new List<GameObject>();
			GameObject tempGrave;
			GameObject gate = GameObject.FindGameObjectWithTag("Gate");
			
			foreach (GameObject ob in obs)
			{
				graves.Add(ob);
			}
			
			graves.Sort(delegate(GameObject grave1, GameObject grave2)
			            {
			            	float grave1Dist = Vector3.Distance(grave1.transform.position,gate.transform.position);
							float grave2Dist = Vector3.Distance(grave2.transform.position,gate.transform.position);
				        	return grave1Dist.CompareTo(grave2Dist);
				        	//return grave1.transform.position.z.CompareTo(grave2.transform.position.z);
			            });
			
			while (graves.Count > maxGraves)
			{
				tempGrave = graves[0];
				graves.Remove(tempGrave);
				DestroyImmediate(tempGrave);
			}
		}
	}
	
	private void FillGraves()
	{
		int maxZombies = 0;
		int curZombies = 0;
		/*switch(GlobalValues.size)
		{
		case 1:
			maxZombies = zombiesSmall;
			break;
		case 2:
			maxZombies = zombiesNormal;
			break;
		case 3:
			maxZombies = zombiesLarge;
			break;
		}*/
		maxZombies = (int)numZombies.getVal (GlobalValues.size, GlobalValues.difficulty);
		
		GameObject[] graves = GameObject.FindGameObjectsWithTag("Grave");
		Grave graveScript;
		int index = 0;
		
		while (curZombies < maxZombies)
		{
			graveScript = graves[index].GetComponent<Grave>();
			graveScript.AddZombies(1);
			
			index++;
			if (index >= graves.Length)
			{
				index = 0;
			}
			
			curZombies++;
		}
	}
	
	private void CheckOpenings()
	{
		GameObject[] tiles = GameObject.FindGameObjectsWithTag("Floor");
		
		foreach(GameObject tile in tiles)
		{
			Tile tileScript = tile.GetComponent<Tile>();
			tileScript.CheckOpen();
		}
	}
	
	private void ReplaceProxies()
	{
		GameObject[] proxies = GameObject.FindGameObjectsWithTag("Proxy");
		ProxyPart proxScript;
		
		foreach (GameObject proxy in proxies)
		{
			proxScript = proxy.GetComponent<ProxyPart>();
			proxScript.CreateReplacement();
		}
	}
	
	private void CreateWalls()
	{
		GameObject[] tiles = GameObject.FindGameObjectsWithTag("Floor");
		
		foreach(GameObject tile in tiles)
		{
			Tile tileScript = tile.GetComponent<Tile>();
			tileScript.CreateWalls();
		}
	}
	
	private void CreateGround()
	{
		GameObject ground = GameObject.Instantiate(Resources.Load("Prefabs/Ground")) as GameObject;
		ground.transform.position = new Vector3(0,.49f,0);
	}
	
	private void CreateHouses()
	{
		//Placeholder.  Fix later
		//Object[] houses = new Object[1];
		//houses[0] = Resources.Load("Prefabs/Placeholder_house") as Object;
		//--------------------------
		Object[] houses = Resources.LoadAll("FinalAssets/Houses");
		int i;
		allHouseBounds = new List<Bounds>();
		
		for (i=0; i<housesPerSide; i++)
		{
			PlaceHouse(Direction.UP, houses);
		}
		/*for (i=0; i<housesPerSide; i++)
		{
			PlaceHouse(Direction.DOWN, houses);
		}*/
		for (i=0; i<housesPerSide; i++)
		{
			PlaceHouse(Direction.LEFT, houses);
		}
		for (i=0; i<housesPerSide; i++)
		{
			PlaceHouse(Direction.RIGHT, houses);
		}
	}
	
	private void PlaceHouse(Direction dir, Object[] houses)
	{
		Bounds hb;
		GameObject house;
		float houseDistance = 7.0f;	
		int maxAttempts = 99;
		int attempts = 0;
		bool keepTrying = true;
		float spawnX;
		float spawnY = 0.5f;
		float spawnZ;
		//attempts = 0;
		//keepTrying = true;
		
		house = GameObject.Instantiate(houses[Random.Range(0, houses.Length)]) as GameObject;
		
		while(keepTrying)
		{
			keepTrying = false;
			
			if (dir == Direction.UP)
			{
				spawnX = Random.Range(lowerBounds.x, upperBounds.x);
				spawnZ = upperBounds.y + (hb.extents.z + houseDistance);
			}
			else if (dir == Direction.DOWN)
			{
				spawnX = Random.Range(lowerBounds.x, upperBounds.x);
				spawnZ = lowerBounds.y - (hb.extents.z + houseDistance);
			}
			else if (dir == Direction.LEFT)
			{
				spawnX = lowerBounds.x - (hb.extents.x + houseDistance);
				spawnZ = Random.Range(lowerBounds.y, upperBounds.y);
			}
			else
			{
				spawnX = upperBounds.x + (hb.extents.x + houseDistance);
				spawnZ = Random.Range(lowerBounds.y, upperBounds.y);
			}
			/*if (Random.Range(0f,1f) > 0.4f)
			{
				if (Random.Range(0f,1f) > 0.4f)
				{
					spawnX = upperBounds.x + (hb.extents.x + houseDistance);
				}
				else
				{
					spawnX = lowerBounds.x - (hb.extents.x + houseDistance);
				}
				spawnZ = Random.Range(lowerBounds.y, upperBounds.y);
			}
			else
			{
				spawnX = Random.Range(lowerBounds.x, upperBounds.x);
				if (Random.Range(0f,1f) > 0.4f)
				{	
					spawnZ = upperBounds.y + (hb.extents.z + houseDistance);
				}
				else
				{
					spawnZ = lowerBounds.y - (hb.extents.z + houseDistance);
				}
			}*/
			house.transform.position = new Vector3(spawnX, spawnY, spawnZ);
			hb = house.GetComponent<BoxCollider>().bounds;
			foreach(Bounds ob in allHouseBounds)
			{
				if (hb.Intersects(ob))
				{
					keepTrying = true;
					Destroy (house);
					break;
				}
			}
			
			if (keepTrying)
			{
				attempts++;
				if (attempts >= maxAttempts)
				{
					keepTrying = false;
				}
			}
			else
			{
				Bounds newBound = house.GetComponent<BoxCollider>().bounds;
				allHouseBounds.Add(newBound);
			}
		}
	}
	
	public Room CreateRoom(Room creator, Direction dir)
	{
		Direction opDir = Enums.ReverseDirection(dir);
		Room roomScript;
		RoomEnd entryDoor;
		int creationAttemps = 0;
		int maxCreationAttemps = 5;
		
		while (creationAttemps < maxCreationAttemps)
		{
			//Debug.Log ("Created a room");
			GameObject newRoom = GameObject.Instantiate(Resources.Load(GetRoomPath(opDir))) as GameObject;
			roomScript = newRoom.GetComponent<Room>();
			
			roomScript.CreateCollider();
			roomScript.ShuffleEnds();
			entryDoor = roomScript.GetEnd(opDir);
			roomScript.transform.position = GetCreatePosition(creator,roomScript,entryDoor,creator.GetEnd(dir));
			
			if (roomScript.IsOneUse() && usedRooms.Contains(roomScript.GetName()))
			{
				creationAttemps++;
				DestroyImmediate(newRoom);
			}
			else if (roomScript.transform.position.z <= startRoomPos.z)
			{
				creationAttemps++;
				DestroyImmediate(newRoom);
			}
			else if (CheckIntersectingRoom(roomScript))
			{
				creationAttemps++;
				DestroyImmediate(newRoom);
			}
			else
			{
				if (roomScript.IsOneUse() && !usedRooms.Contains(roomScript.GetName()))
				{
					usedRooms.Add(roomScript.GetName());
				}
				
				roomScript.GetEnd(opDir).SetUsed();
				
				createdRooms++;
				
				roomScript.Setup(createdRooms);
				
				UpdateBounds(roomScript);
				
				return roomScript;
			}
		}
	
		return null;
	}
	
	private void UpdateBounds(Room newRoom)
	{
		Vector3 roomPos = newRoom.transform.position;
		Bounds rb = newRoom.GetComponent<BoxCollider>().bounds;
		float newUpperX = roomPos.x + (rb.extents.x + 0.5f);
		float newUpperY = roomPos.z + (rb.extents.z + 0.5f);
		float newLowerX = roomPos.x - (rb.extents.x + 0.5f);
		float newLowerY = roomPos.z - (rb.extents.z + 0.5f);
		
		if (newUpperX > upperBounds.x)
		{
			upperBounds = new Vector2(newUpperX, upperBounds.y);
		}
		if (newUpperY > upperBounds.y)
		{
			upperBounds = new Vector2(upperBounds.x, newUpperY);
		}
		if (newLowerX < lowerBounds.x)
		{
			lowerBounds = new Vector2(newLowerX, lowerBounds.y);
		}
		if (newLowerY < lowerBounds.y)
		{
			lowerBounds = new Vector2(lowerBounds.x, newLowerY);
		}
	}
	
	private string GetRoomPath(Direction entryDir)
	{
		GameObject room;
		Room roomScript;
		string roomName;
		int i = 0;
		
		//Shuffle list
		for (i=0; i<rooms.Length; i++)
		{
			Object temp = rooms[i];
			int randomIndex = Random.Range(i, rooms.Length);
			rooms[i] = rooms[randomIndex];
			rooms[randomIndex] = temp;
		}
		
		for (i=0; i<rooms.Length; i++)
		{
			room = rooms[i] as GameObject;
			roomScript = room.GetComponent<Room>();
			roomName = rooms[i].name;
			
			//Prevents the same room twice
			//if (roomScript.HasDoor(entryDir) && (roomName != previousRoom)) 
			if (roomScript.HasEnd(entryDir) && roomScript.CanUse())
			{
				//previousRoom = roomName;
				return "Rooms/"+roomName;
			}
		}
		
		Debug.Log("Room not found");
		
		return "";
	}
	
	private bool CheckIntersectingRoom(Room newRoom)
	{
		BoxCollider roomCollider = newRoom.GetComponent<BoxCollider>();
		GameObject[] otherRooms = GameObject.FindGameObjectsWithTag("Room");
		
		for (int i=0; i<otherRooms.Length; i++)
		{
			if (otherRooms[i].GetComponent<Room>() == newRoom)
			{
				continue;
			}
			
			BoxCollider otherRoomCollider = otherRooms[i].GetComponent<BoxCollider>();
			
			if (roomCollider.bounds.Intersects(otherRoomCollider.bounds))
			{
				return true;
			}
		}
		
		return false;
	}
	
	private Vector3 GetCreatePosition(Room creator,Room created,RoomEnd entryEnd,RoomEnd exitEnd)
	{
		Vector3 exitPos = exitEnd.transform.position;
		exitPos = new Vector3(exitPos.x,0,exitPos.z);
		
		float xOffset = 0;
		float zOffset = 0;
		
		if (Enums.IsVertical(entryEnd.GetDirection()))
		{
			zOffset = ((created.getSize().y-1)/2)+1;
			if (entryEnd.GetDirection() == Direction.UP)
			{
				zOffset *= -1;
			}
		}
		else
		{
			xOffset = ((created.getSize().x-1)/2)+1;
			if (entryEnd.GetDirection() == Direction.RIGHT)
			{
				xOffset *= -1;
			}
		}
		
		Vector3 createPos = new Vector3(exitPos.x+xOffset,0,exitPos.z+zOffset); 
		
		return createPos;
	}
}
