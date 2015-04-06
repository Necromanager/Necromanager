using UnityEngine;
using System.Collections;

public class Grass : MonoBehaviour 
{
	[SerializeField] bool randomRotate;

	void Start () 
	{
		if (randomRotate)
		{
			ChangeRotation();
		}
	}
	
	void Update () 
	{
		
	}
	
	private void ChangeRotation()
	{
		transform.Rotate(new Vector3(0,Random.Range(-180, 180),0));
	}
}
