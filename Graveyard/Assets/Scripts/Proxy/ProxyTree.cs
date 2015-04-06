using UnityEngine;
using System.Collections;

public class ProxyTree : ProxyPart 
{
	protected override void SetValues()
	{
		Object[] trees = Resources.LoadAll("FinalAssets/Trees");
		objectPath = "FinalAssets/Trees/Tree";
		objectPath += Random.Range(1, trees.Length+1);
		posOffset = new Vector3(0,-1.5f,0);
	}
}
