using UnityEngine;
using System.Collections;

public class ProxyGround : ProxyPart
{
	protected override void SetValues()
	{
		objectPath = "Prefabs/Floor";
		posOffset = new Vector3(0,0,0);
	}
}
