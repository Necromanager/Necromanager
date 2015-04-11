using UnityEngine;
using System.Collections;

public class ProxyGrave : ProxyPart
{
	protected override void SetValues()
	{
		objectPath = "FinalAssets/Graves/Grave";
		posOffset = new Vector3(0,-0.5f,-0.25f);
	}
}
