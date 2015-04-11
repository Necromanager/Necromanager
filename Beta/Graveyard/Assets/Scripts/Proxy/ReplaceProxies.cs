using UnityEngine;
using System.Collections;

public class ReplaceProxies : MonoBehaviour 
{
	void Start ()
	{
		GameObject[] proxies = GameObject.FindGameObjectsWithTag("Proxy");
		ProxyPart proxScript;
		
		foreach (GameObject proxy in proxies)
		{
			proxScript = proxy.GetComponent<ProxyPart>();
			proxScript.CreateReplacement();
		}
	}
}
