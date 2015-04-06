using UnityEngine;
using System.Collections;

public class ProxyBench : ProxyPart
{
	protected override void SetValues()
	{
		objectPath = "FinalAssets/Benches/Bench1";
		posOffset = new Vector3(0,-0.5f,0);
	}
}
