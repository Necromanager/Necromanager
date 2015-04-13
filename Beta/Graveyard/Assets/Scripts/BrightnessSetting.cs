using UnityEngine;
using System.Collections;

public class BrightnessSetting : MonoBehaviour 
{
	[SerializeField] Light myLight;
	[SerializeField] float min;
	[SerializeField] float max;
	[SerializeField] float speed;

	void Start () 
	{
	
	}
	
	void Update () 
	{
		if ((Input.GetKeyDown(KeyCode.Plus)) || (Input.GetKeyDown(KeyCode.Equals)))
		{
			myLight.intensity += speed;
			if (myLight.intensity > max)
			{
				myLight.intensity = max;
			}
		}
		else if (Input.GetKeyDown(KeyCode.Minus))
		{
			myLight.intensity -= speed;
			if (myLight.intensity < min)
			{
				myLight.intensity = min;
			}
		}
	}
}
