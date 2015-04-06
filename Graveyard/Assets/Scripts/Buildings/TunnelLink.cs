using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TunnelLink : MonoBehaviour 
{
	private TunnelLink otherEnd;
	//private List<Tile> exitTiles;
	private bool hasOtherEnd;
	private bool canTeleport;
	//private Tile attachedTile;
	
	void Start () 
	{
		hasOtherEnd = false;
		canTeleport = true;
		//FillExitPositions();
		FindOtherEnd();
	}
	
	void Update () 
	{
		if (hasOtherEnd)
		{
			hasOtherEnd = (otherEnd != null);
		}
	}
	
	public Vector3 GetSpawnPos()
	{
		/*Tile spawnTile = exitTiles[Random.Range(0, exitTiles.Count)];
		Vector3 tilePos = spawnTile.transform.position;
		
		return new Vector3(tilePos.x, tilePos.y+1.5f, tilePos.z);*/
		Vector3 myPos = transform.position;

		return new Vector3 (myPos.x, myPos.y + 0.5f, myPos.z);
	}
	
	public void SetOtherEnd(TunnelLink otherTunnel)
	{
		if (otherTunnel == null)
		{
			otherEnd = null;
			hasOtherEnd = false;
			return;
		}
		
		otherEnd = otherTunnel;
		hasOtherEnd = true;
	}
	
	private void FindOtherEnd()
	{
		TunnelLink tunnelScript;
		GameObject[] tunnels = GameObject.FindGameObjectsWithTag("Tunnel");
		if (tunnels.Length != 2)
		{
			return;
		}
		
		foreach(GameObject tunnel in tunnels)
		{
			tunnelScript = tunnel.GetComponent<TunnelLink>();
			if (tunnelScript == this)
			{
				continue;
			}
			else
			{
				SetOtherEnd(tunnelScript);
				tunnelScript.SetOtherEnd(this);
				return;
			}
		}
	}

	public bool CanTeleport()
	{
		return canTeleport;
	}

	public void SetCanTeleport(bool canNowTeleport)
	{
		canTeleport = canNowTeleport;
	}
	
	/*private void FillExitPositions()
	{
		Debug.Log("Started finding exits");
		exitTiles = new List<Tile>();
		Collider[] otherTiles;
		List<Vector3> possibleTiles = new List<Vector3>();
		Vector3 pos = transform.position;
		possibleTiles.Add(new Vector3(pos.x,pos.y-1,pos.z+1));
		possibleTiles.Add(new Vector3(pos.x,pos.y-1,pos.z-1));
		possibleTiles.Add(new Vector3(pos.x+1,pos.y-1,pos.z));
		possibleTiles.Add(new Vector3(pos.x-1,pos.y-1,pos.z));
		
		foreach (Vector3 tilePos in possibleTiles)
		{
			otherTiles = Physics.OverlapSphere(tilePos,0.4f);
			
			foreach (Collider ob in otherTiles)
			{
				if (ob.tag == "Floor")
				{
					Tile newTile = ob.GetComponent<Tile>();
					Debug.Log("Found Floor   Open: "+newTile.IsOpen());
					if (newTile.IsOpen())
					{
						Debug.Log("Added floor");
						exitTiles.Add(newTile);
					}
				}
			}
		}
		
		if (exitTiles.Count <= 0)
		{
			Debug.Log("Tunnel: No free spaces");
			Vector3 myPos = transform.position;
			otherTiles = Physics.OverlapSphere(new Vector3(myPos.x, myPos.y-1, myPos.z), 0.4f);
			
			foreach (Collider ob in otherTiles)
			{
				if (ob.tag == "Floor")
				{
					exitTiles.Add(ob.GetComponent<Tile>());
					return;
				}
			}
		}
	} */
	
	void OnTriggerEnter(Collider col)
	{
		if (!hasOtherEnd)
		{
			Debug.Log("Has no other end");
			return;
		}

		if (col.gameObject.tag == "Player")
		{
			if (canTeleport)
			{
				otherEnd.SetCanTeleport(false);
				col.gameObject.transform.position = otherEnd.GetSpawnPos();
			}
			Debug.Log("Can't teleport");
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (!hasOtherEnd)
		{
			return;
		}
		
		if (col.gameObject.tag == "Player")
		{
			SetCanTeleport(true);
		}
	}
}
