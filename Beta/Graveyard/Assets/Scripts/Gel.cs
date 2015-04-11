using UnityEngine;
using System.Collections;

public class Gel : MonoBehaviour 
{
	private HairGel creator;
	
	void Start () 
	{
	}
	
	void Update () 
	{
	}
	
	public void SetCreator(HairGel hairGel)
	{
		creator = hairGel;
	}
	
	public void SlipZombie(ZombieScript zombie)
	{
		zombie.SetStatus(ZombieStatus.STUNNED);
		
		creator.RemoveFromList(this);
		Destroy (gameObject);
	}
}
