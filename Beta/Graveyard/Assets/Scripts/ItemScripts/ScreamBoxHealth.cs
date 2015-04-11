using UnityEngine;
using System.Collections;

public class ScreamBoxHealth : MonoBehaviour 
{
	[SerializeField] private float lightSpeed = 1;
	[SerializeField] private float brightness = 1.3f;
	[SerializeField] private float health = 100;
	[SerializeField] private float healthLoss = 1;
	
	private Light blinkLight;

	void Start () 
	{
		blinkLight = GetComponentInChildren<Light> ();
	}
	
	void Update () 
	{
		UpdateLight ();

		if (BeingEaten())
		{
			LoseHealth();
		}
	}

	private void UpdateLight()
	{
		float intensity =  Mathf.Abs(Mathf.Sin(Time.timeSinceLevelLoad * lightSpeed));

		blinkLight.intensity = intensity * brightness;
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
		
		if (health <= 0)
		{
			GetComponent<AudioSource>().Stop();
			DestroyImmediate(gameObject);
		}
	}
}
