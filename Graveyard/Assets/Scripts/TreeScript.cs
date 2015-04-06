using UnityEngine;
using System.Collections;

public class TreeScript : MonoBehaviour 
{
	[SerializeField] private float visibleRange;
	[SerializeField] private float minVisibility;
	
	private GameObject playerCamera;

	void Start () 
	{
		Init ();
	}
	
	private void Init()
	{
		float rotation = Random.Range (0,4);
		transform.eulerAngles = new Vector3(0,rotation*90,0);
		playerCamera = Camera.main.gameObject;
	}
	
	void Update () 
	{
		//renderer.material.color = GetColor();
	}
	
	private Color GetColor()
	{
		float curAlpha = 0;
		Vector3 camPos = new Vector3(playerCamera.transform.position.x,0,playerCamera.transform.position.z);
		Vector3 myPos = new Vector3(transform.position.x,0,transform.position.z);
		float distance = Vector3.Distance(camPos, myPos);
		
		if (distance <= visibleRange)
		{
			curAlpha = distance/visibleRange;
			
			if (curAlpha < minVisibility)
			{
				curAlpha = minVisibility;
			}
			
			return new Color(1,1,1,curAlpha);
		}
		
		return new Color(1,1,1);
	}
}
