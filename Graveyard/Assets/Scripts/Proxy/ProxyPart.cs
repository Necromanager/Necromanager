using UnityEngine;
using System.Collections;

public abstract class ProxyPart : MonoBehaviour 
{
	protected string objectPath;
	protected Vector3 posOffset;

	void Start () 
	{
	
	}
	
	void Update () 
	{
	
	}
	
	public void CreateReplacement()
	{
		SetValues();
		
		Transform parentRoom = transform.parent;
		Vector3 pos = transform.position;
		
		GameObject createdObj = GameObject.Instantiate(Resources.Load(objectPath)) as GameObject;
		createdObj.transform.parent = parentRoom;
		createdObj.transform.position = new Vector3(pos.x+posOffset.x, pos.y+posOffset.y, pos.z+posOffset.z);
		//GameObject createdObj = GameObject.Instantiate(realObject) as GameObject;
		//createdObj.transform.position = new Vector3(pos.x+positionOffset.x, pos.y+positionOffset.y, pos.z+positionOffset.z);
	
		Destroy (gameObject);
	}
	
	protected abstract void SetValues();
}
