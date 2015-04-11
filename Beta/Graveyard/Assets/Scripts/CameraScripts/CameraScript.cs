using UnityEngine;
using System.Collections;

public abstract class CameraScript: MonoBehaviour
{
	[SerializeField] protected Vector3 positionOffset;
	[SerializeField] protected Vector3 cameraRotation;

	//public Vector3 positionOffset;
	//public Quaternion cameraRotation;

	void Start () 
	{
	}
	
	void FixedUpdate () 
	{
		UpdateTransform();
		UpdateZoom ();
	}
	
	public void Deactivate()
	{
		this.enabled = false;
	}
	
	public abstract void Activate();
	protected abstract void UpdateTransform();
	protected abstract void UpdateZoom();
}
