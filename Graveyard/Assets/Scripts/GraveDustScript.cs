using UnityEngine;
using System.Collections;

public class GraveDustScript : MonoBehaviour 
{
	private ParticleSystem ps;

	void Start () 
	{
		ps = GetComponent<ParticleSystem>();
	}
	
	void Update () 
	{
		if (ps)
		{
			if (!ps.IsAlive())
			{
				Destroy (gameObject);
			}
		}
	}
}

//Thanks to:
//http://answers.unity3d.com/questions/219609/auto-destroying-particle-system.html
