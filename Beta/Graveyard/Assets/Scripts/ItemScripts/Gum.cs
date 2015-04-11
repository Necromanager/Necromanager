using UnityEngine;
using System.Collections;

public class Gum : MonoBehaviour 
{
	[SerializeField] float stickTime;
	
	private float curStickTime;
	private bool caughtZombie;
	private ZombieScript zombie;
	
	void Start () 
	{
		curStickTime = stickTime;
		caughtZombie = false;
	}
	
	void Update () 
	{
		bool zombieExists = (zombie != null);
		if (caughtZombie && zombieExists)
		{
			curStickTime -= Time.deltaTime;
			if (curStickTime <= 0)
			{
				UnstickZombie();
				Destroy (gameObject);
			}
		}
	}
	
	public bool HasZombie()
	{
		return caughtZombie;
	}
	
	public void StickZombie(ZombieScript stuckZombie)
	{
		zombie = stuckZombie;
		zombie.SetCanMove(false);
		caughtZombie = true;
	}
	
	public void UnstickZombie()
	{
		if (caughtZombie)
		{
			zombie.SetCanMove(true);
			zombie = null;
			caughtZombie = false;
		}
	}
}
