using UnityEngine;
using System.Collections;

public class BrightnessSetting : MonoBehaviour 
{
	[SerializeField] Light light;
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
			light.intensity += speed;
			if (light.intensity > max)
			{
				light.intensity = max;
			}
		}
		else if (Input.GetKeyDown(KeyCode.Minus))
		{
			light.intensity -= speed;
			if (light.intensity < min)
			{
				light.intensity = min;
			}
		}
	}
}
