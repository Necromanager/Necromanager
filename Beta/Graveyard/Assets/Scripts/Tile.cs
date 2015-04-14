using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour 
{
	private const int DEVALUE_NIGHTS = 1;
	private const float DEVALUE_PERCENT = 0.75f;

	private List<Tile> neighbors;
	private List<Tile> adjacent;
	
	[SerializeField] private float grassChance;
	[SerializeField] private bool open;
	[SerializeField] private bool gateSpot;
	[SerializeField] private bool graveSpot;
	
	private float gScore;
	private float fScore;
	private bool hasBuilding;
	private GameObject building;
	private float buildingCost;
	private int buildingNights;

	void Start () 
	{
		gScore = 0;
		fScore = 0;
		graveSpot = false;
		hasBuilding = false;
		building = null;
		buildingCost = 0;
		buildingNights = 0;
		CreateGrass();
		//open = CheckOpen();
	}

	private void CreateGrass()
	{
		if (Random.Range(0f,1f) > grassChance)
		{
			return;
		}
		
		Object[] grassObs = Resources.LoadAll("FinalAssets/Grass");
		GameObject grass = GameObject.Instantiate(grassObs[Random.Range(0, grassObs.Length)]) as GameObject;
		Vector3 pos = transform.position;
		grass.transform.position = new Vector3(pos.x, pos.y+1, pos.z);
	}
	
	public bool IsGateSpot()
	{
		return gateSpot;
	}
	
	public bool IsGraveSpot()
	{
		if (!graveSpot)
		{
			CheckIsGraveSpot();
		}
		
		return graveSpot;
	}
	
	public Vector3 GetPos()
	{
		return transform.position;
	}
	
	public float GetGScore()
	{
		return gScore;
	}
	
	public void SetGScore(float newScore)
	{
		gScore = newScore;
	}
	
	public float GetFScore()
	{
		return fScore;
	}
	
	public void SetFScore(float newScore)
	{
		fScore = newScore;
	}
	
	public void CheckOpen()
	{
		Vector3 spherePos = new Vector3(transform.position.x,transform.position.y+1,transform.position.z);
		Collider[] above = Physics.OverlapSphere(spherePos,0.4f);

		foreach (Collider ob in above)
		{
			if ((ob.tag == "Wall") || (ob.tag == "Gate") || 
			    (ob.tag == "Obstacle") || (ob.tag == "Grave") || (ob.tag == "Mausoleum"))
			{
				open = false;
				return;
			}
		}
		
		open = true;
	}

	public bool CheckGraveOpen()
	{
		Vector3 spherePos = new Vector3(transform.position.x,transform.position.y+1,transform.position.z);
		Collider[] above = Physics.OverlapSphere(spherePos,0.4f);

		foreach (Collider ob in above)
		{
			if ((ob.tag == "Wall") || (ob.tag == "Gate") || 
			    (ob.tag == "Obstacle") || (ob.tag == "Mausoleum"))
			{
				return false;
			}
		}
		
		return true;
	}
	
	private void CheckIsGraveSpot()
	{
		Vector3 spherePos = new Vector3(transform.position.x,transform.position.y+1,transform.position.z);
		Collider[] above = Physics.OverlapSphere(spherePos,0.4f);
		
		foreach (Collider ob in above)
		{
			if (ob.tag == "Grave")
			{
				graveSpot = true;
				return;
			}
		}
		
		graveSpot = false;
	}
	
	public Tile GetOtherGraveTile()
	{
		List<Tile> tiles = GetAdjacent();
		foreach (Tile tile in tiles)
		{
			if (tile.IsGraveSpot())
			{
				return tile;
			}
		}
		
		return null;
	}
	
	public bool IsOpen()
	{
		//open = CheckOpen();
		return open;
	}
	
	public void SetOpen(bool isOpen)
	{
		open = isOpen;
		foreach (Tile tile in GetAdjacent())
		{
			tile.FillNeighborList();
		}
	}
	
	public bool HasBuilding()
	{
		return hasBuilding;
	}
	
	public GameObject GetBuilding()
	{
		return building;
	}
	
	public void SetBuilding(bool isBuilding)
	{
		hasBuilding = isBuilding;
	}
	
	public void DevalueBuilding()
	{
		if (hasBuilding)
		{
			buildingNights++;
		}
	}
	
	public void AddBuilding(GameObject newBuilding, float newCost)
	{
		building = newBuilding;
		buildingCost = newCost;
		buildingNights = 0;
		//newBuilding.GetComponent<Building>().SetAttachedTile(this);
		hasBuilding = true;
		
		BrainStickHealth bh = building.GetComponent<BrainStickHealth>();
		if (bh != null)
		{
			bh.SetParentTile(this);
		}
	}
	
	public float GetBuildingCost()
	{
		if (buildingNights >= DEVALUE_NIGHTS)
		{
			float newCost = Mathf.Round((buildingCost * DEVALUE_PERCENT) * 1000f)/1000f;
			Debug.Log(newCost);
			return newCost;
		}
		
		return buildingCost;
	}
	
	public void RemoveBuilding()
	{
		Spotlight spotLight = building.GetComponent<Spotlight>();
		if (spotLight != null)
		{
			return;
		}
	
		try
		{
			Destroy(building);
			building = null;
			buildingCost = 0;
			hasBuilding = false;
		}
		catch
		{
		}
	}
	
	public void CreateWalls()
	{
		if (!IsOpen())
		{
			return;
		}
		
		int numOpenSides = 0;
		int numCorners = 0;
		Collider[] otherTiles;
		Vector3 myPos = transform.position;
		
		List<Direction> possibleDirections = new List<Direction>();
		List<Direction> useDirections = new List<Direction>();
		possibleDirections.Add(Direction.UP);
		possibleDirections.Add(Direction.DOWN);
		possibleDirections.Add(Direction.RIGHT);
		possibleDirections.Add(Direction.LEFT);
		List<Vector3> possibleTiles = new List<Vector3>();
		List<Vector3> possibleCorners = new List<Vector3>();
		possibleTiles.Add(new Vector3(myPos.x,myPos.y,myPos.z+1));
		possibleTiles.Add(new Vector3(myPos.x,myPos.y,myPos.z-1));
		possibleTiles.Add(new Vector3(myPos.x+1,myPos.y,myPos.z));
		possibleTiles.Add(new Vector3(myPos.x-1,myPos.y,myPos.z));
		possibleCorners.Add(new Vector3(myPos.x-1,myPos.y,myPos.z+1));
		possibleCorners.Add(new Vector3(myPos.x-1,myPos.y,myPos.z-1));
		possibleCorners.Add(new Vector3(myPos.x+1,myPos.y,myPos.z+1));
		possibleCorners.Add(new Vector3(myPos.x+1,myPos.y,myPos.z-1));
		
		for (int i=0; i<possibleTiles.Count; i++)
		{
			otherTiles = Physics.OverlapSphere(possibleTiles[i],0.4f);
			//otherTiles = Physics.OverlapSphere(side,0.4f);
			
			if (otherTiles.Length < 1)
			{
				numOpenSides++;
				useDirections.Add(possibleDirections[i]);
				//CreateWallSection(possibleDirections[i]);
				//return;
			}
		}
		
		foreach (Vector3 corner in possibleCorners)
		{
			otherTiles = Physics.OverlapSphere(corner,0.4f);
			
			if (otherTiles.Length < 1)
			{
				numCorners++;
			}
		}
		
		bool inCorner = ((numOpenSides == 0) && (numCorners == 1));
		bool outCorner = (numOpenSides == 2);
		
		if (outCorner || inCorner)
		{
			CreatePillar();
		}
		else
		{
			foreach(Direction dir in useDirections)
			{
				CreateWallSection(dir);
			}
		}
	}
	
	public void CreateWallSection(Direction dir)
	{
		GameObject wall = GameObject.Instantiate(Resources.Load(GetFencePath())) as GameObject;
		Vector3 pos = transform.position;
		wall.transform.position = new Vector3(pos.x,pos.y,pos.z);
		
		switch(dir)
		{
		case Direction.RIGHT:
			//wall.transform.rotation = new Quaternion(0,90,0,0);
			wall.transform.rotation *= Quaternion.Euler(0,180f,0);
			break;
		case Direction.DOWN:
			//wall.transform.rotation = new Quaternion(0,180,0,0);
			wall.transform.rotation *= Quaternion.Euler(0,90f,0);
			break;
		case Direction.UP:
			//wall.transform.rotation = new Quaternion(0,270,0,0);
			wall.transform.rotation *= Quaternion.Euler(0,270f,0);
			break;
		}
		
		open = false;
	}
	
	private string GetFencePath()
	{
		//Object[] fences = Resources.LoadAll("FinalAssets/Fences");
		//Debug.Log("Fences: "+fences.Length);
		string path = "FinalAssets/Fences/Fence";
		
		if (Random.Range(0.0f,1.0f) >= 0.3f)
		{
			return path+"1";
		}
		else
		{
			int fenceNum = Random.Range(2,4);
			if (fenceNum == 3) //Placeholder until fence 3 is complete
			{
				fenceNum = 4;
			}
			return path+fenceNum.ToString();
		}
	}
	
	public void CreatePillar()
	{
		GameObject wall = GameObject.Instantiate(Resources.Load("FinalAssets/Pillars/Pillar")) as GameObject;
		Vector3 pos = transform.position;
		wall.transform.position = new Vector3(pos.x,pos.y,pos.z);
		float rotation = Random.Range (0,4);
		wall.transform.eulerAngles = new Vector3(0,rotation*90,0);
		open = false;
	}
	
	public List<Tile> GetNeighbors()
	{
		if (neighbors == null)
		{
			FillNeighborList();
		}
		
		return neighbors;
	}
	
	public List<Tile> GetAdjacent()
	{
		if (adjacent == null)
		{
			FillAdjacentList();
		}
		
		return adjacent;
	}
	
	public bool HasNeighbor(float changeX,float changeZ)
	{	
		Vector3 pos = transform.position;
		Vector3 targetPos = new Vector3(pos.x+changeX,pos.y,pos.z+changeZ);
		
		Collider[] otherTiles = Physics.OverlapSphere(targetPos,0.4f);
		foreach (Collider col in otherTiles)
		{
			if (col.tag == "Floor")
			{
				return true;
			}
		}
		
		return false;
	}
	
	public bool HasNeighbor(Direction dir)
	{
		if (neighbors == null)
		{
			FillNeighborList();
		}
	
		Vector3 pos = transform.position;
		Vector3 nPos;
		Vector3 targetPos = new Vector3(0,0,0);
		
		switch (dir)
		{
		case Direction.UP:
			targetPos = new Vector3(pos.x,pos.y,pos.z+1);
			break;
		case Direction.DOWN:
			targetPos = new Vector3(pos.x,pos.y,pos.z-1);
			break;
		case Direction.LEFT:
			targetPos = new Vector3(pos.x-1,pos.y,pos.z);
			break;
		case Direction.RIGHT:
			targetPos = new Vector3(pos.x+1,pos.y,pos.z);
			break;
		}
		
		foreach (Tile neighbor in neighbors)
		{
			nPos = neighbor.transform.position;
			if (nPos == targetPos)
			{
				return true;
			}
		}
		
		return false;
	}
	
	public void FillNeighborList()
	{
		neighbors = new List<Tile>();
		Collider[] otherTiles;
		List<Vector3> possibleTiles = new List<Vector3>();
		Vector3 pos = transform.position;
		possibleTiles.Add(new Vector3(pos.x,pos.y,pos.z+1));
		possibleTiles.Add(new Vector3(pos.x,pos.y,pos.z-1));
		possibleTiles.Add(new Vector3(pos.x+1,pos.y,pos.z));
		possibleTiles.Add(new Vector3(pos.x-1,pos.y,pos.z));
		
		foreach (Vector3 tilePos in possibleTiles)
		{
			otherTiles = Physics.OverlapSphere(tilePos,0.4f);
			
			foreach (Collider ob in otherTiles)
			{
				if (ob.tag == "Floor")
				{
					Tile newTile = ob.GetComponent<Tile>();
					if (newTile.IsOpen())
					{
						neighbors.Add(newTile);
					}
				}
			}
		}
	}
	
	public void FillAdjacentList()
	{
		adjacent = new List<Tile>();
		Collider[] otherTiles;
		List<Vector3> possibleTiles = new List<Vector3>();
		Vector3 pos = transform.position;
		possibleTiles.Add(new Vector3(pos.x,pos.y,pos.z+1));
		possibleTiles.Add(new Vector3(pos.x,pos.y,pos.z-1));
		possibleTiles.Add(new Vector3(pos.x+1,pos.y,pos.z));
		possibleTiles.Add(new Vector3(pos.x-1,pos.y,pos.z));
		
		foreach (Vector3 tilePos in possibleTiles)
		{
			otherTiles = Physics.OverlapSphere(tilePos,0.4f);
			
			foreach (Collider ob in otherTiles)
			{
				if (ob.tag == "Floor")
				{
					Tile newTile = ob.GetComponent<Tile>();
					adjacent.Add(newTile);
				}
			}
		}
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
}
