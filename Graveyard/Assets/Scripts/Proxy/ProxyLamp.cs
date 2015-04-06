using UnityEngine;
using System.Collections;

public class ProxyLamp : ProxyPart 
{
	protected override void SetValues()
	{
		objectPath = "FinalAssets/Lights/Lamp";
		posOffset = new Vector3(0,-0.5f,0);
	}
}
