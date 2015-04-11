using UnityEngine;
using System.Collections;

public class Necromancer : ZombieScript
{
	private Grave goalGrave;

	void Start () 
	{
		Init ();
		isBoss = true;
		myName = "Necromancer";
		speed = 3f;
		goalGrave = GetGoalGrave();
		
		GetComponent<Renderer>().material.color = Color.red;
	}
	
	public override Tile GetGoalTile()
	{
		if (ReachedGoalGrave())
		{
			SummonZombies();
			goalGrave = GetGoalGrave();
		}
		
		Vector3 gravePos = goalGrave.transform.position;
		Vector3 spherePos = new Vector3(gravePos.x,gravePos.y-0.5f,gravePos.z);
		Collider[] below = Physics.OverlapSphere(spherePos,0.4f);
		foreach (Collider ob in below)
		{
			if ((ob.tag == "Floor"))
			{
				Tile tile = ob.gameObject.GetComponent<Tile>();
				return tile.GetNeighbors()[Random.Range(0,tile.GetNeighbors().Count)];
			}
		}
		
		return GetRandomTile();
	}
	
	private Grave GetGoalGrave()
	{
		GameObject[] graves = GameObject.FindGameObjectsWithTag("Grave");
		Grave graveScript;
		
		//Shuffle list
		for (int i=0; i<graves.Length; i++)
		{
			GameObject temp = graves[i];
			int randomIndex = Random.Range(i, graves.Length);
			graves[i] = graves[randomIndex];
			graves[randomIndex] = temp;
		}
		
		foreach (GameObject grave in graves)
		{
			graveScript = grave.GetComponent<Grave>();
			if (graveScript.GetNumZombies() > 0)
			{
				return graveScript;
			}
		}
		
		return graves[Random.Range(0,graves.Length)].GetComponent<Grave>();
	}
	
	private bool ReachedGoalGrave()
	{	
		Vector3 spherePos = new Vector3(transform.position.x,1,transform.position.z);
		Collider[] around = Physics.OverlapSphere(spherePos,1);
		foreach (Collider ob in around)
		{
			if ((ob.tag == "Grave"))
			{
				Grave graveScript = ob.gameObject.GetComponent<Grave>();
				if (graveScript == goalGrave)
				{
					return true;
				}
			}
		}
		
		return false;
	}
	
	private void SummonZombies()
	{
		/*while(goalGrave.GetNumZombies() > 0)
		{
			goalGrave.SpawnZombie();
		}*/
		goalGrave.SpawnZombie();
	}
	
	public override void Stun ()
	{
		LoseHealth(totalHealth/10);
	}
	
	public override void Shoot ()
	{
		LoseHealth(totalHealth/5);
	}
	
	public override bool Grab()
	{
		return false;
	}
	
	protected override AudioClip GetSpawnSound()
	{
		return null;
	}
	
	public override AudioClip GetHitSound()
	{
		return GetSpawnSound();
	}
}
