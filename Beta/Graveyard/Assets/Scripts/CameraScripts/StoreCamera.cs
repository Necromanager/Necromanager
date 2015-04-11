using UnityEngine;
using System.Collections;

public class StoreCamera : CameraScript
{
	public override void Activate()
	{
		if (!this.enabled)
		{
			this.enabled = true;
			transform.position = positionOffset;
			transform.eulerAngles = cameraRotation;
		}
	}
	
	protected override void UpdateTransform()
	{
		
	}
	protected override void UpdateZoom()
	{
		
	}
}
