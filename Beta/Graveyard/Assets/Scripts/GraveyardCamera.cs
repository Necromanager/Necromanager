using UnityEngine;
using System.Collections;

public class GraveyardCamera : MonoBehaviour {

	[SerializeField] private float leftXLimit;
	[SerializeField] private float rightXLimit;
	[SerializeField] private PlayerScript targetPlayer;

	void Start () 
	{
	
	}
	
	void Update () 
	{
		updatePosition();
	}
	
	private void updatePosition()
	{
		float newX = targetPlayer.transform.position.x;
		float newY = transform.position.y;
		float newZ = transform.position.z;
		
		if (newX < leftXLimit)
		{
			newX = leftXLimit;
		}
		else if (newX > rightXLimit)
		{
			newX = rightXLimit;
		}
		
		transform.position = new Vector3(newX,newY,newZ);
	}
}
