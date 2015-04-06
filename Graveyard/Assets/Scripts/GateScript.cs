using UnityEngine;
using System.Collections;

public class GateScript : MonoBehaviour 
{
	[SerializeField] private float gateHealth = 100;
	[SerializeField] private float healthLoss = 1;
	
	void Start () 
	{
	
	}
	

	/*void Update () 
	{
		if (BeingEaten())
		{
			LoseHealth();
		}
	}*/
	
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
		gateHealth -= healthLoss*Time.deltaTime;
		
		if (gateHealth <= 0)
		{
			//DestroyImmediate(gameObject);
		}
	}
}
