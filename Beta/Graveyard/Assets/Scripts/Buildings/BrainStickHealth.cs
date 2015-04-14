using UnityEngine;
using System.Collections;

public class BrainStickHealth : MonoBehaviour 
{
	[SerializeField] private float health = 100;
	[SerializeField] private float healthLoss = 1;
	
	private Tile parentTile;
	private float nomTime;
	private float nomMaxTime = 2;

	void Start () 
	{
	}
	
	void Update () 
	{
		nomTime -= Time.deltaTime;
		if (nomTime < 0)
			nomTime = 0;

		if (BeingEaten())
		{
			LoseHealth();
		}
	}
	
	public void SetParentTile(Tile tile)
	{
		parentTile = tile;
	}
	
	private bool BeingEaten()
	{
		Vector3 spherePos = new Vector3(transform.position.x,transform.position.y,transform.position.z);
		Collider[] around = Physics.OverlapSphere(spherePos,1.0f);
		
		foreach (Collider ob in around)
		{
			if (ob.tag == "Zombie")
			{
				return true;
			}
		}
		
		return false;
	}
	
	private void LoseHealth()
	{
		health -= healthLoss*Time.deltaTime;
		if (nomTime == 0)
		{
			nomTime = nomMaxTime;
			int soundIndex = Random.Range(0,SoundEffectLibrary.omnomnom.Count);
			GlobalFunctions.PlaySoundEffect(SoundEffectLibrary.omnomnom[soundIndex]);
		}

		if (health <= 0)
		{
			GlobalFunctions.PlaySoundEffect(SoundEffectLibrary.finishBrain);
			//DestroyImmediate(gameObject);
			if (parentTile != null)
			{
				parentTile.RemoveBuilding();
			}
			else
			{
				Destroy(gameObject);
			}
		}
	}
}
