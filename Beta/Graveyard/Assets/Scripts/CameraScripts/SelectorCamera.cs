using UnityEngine;
using System.Collections;

public class SelectorCamera : CameraScript
{
	private const float MAX_CAMERA_SIZE = 20;
	private const float MIN_CAMERA_SIZE = 5;
	private const float CAMERA_ZOOM_SPEED = 0.4f;
	private const float CAMERA_MOVE_SPEED = 0.3f;

	[SerializeField] private Selector selector;
	[SerializeField] private Camera childCamera;
	//[SerializeField] float cameraSpeed;

	public override void Activate()
	{
		if (!this.enabled)
		{
			this.enabled = true;
			GetComponent<Camera>().orthographic = true;
			GetComponent<Camera>().orthographicSize = MIN_CAMERA_SIZE;
			childCamera.orthographicSize = GetComponent<Camera>().orthographicSize;
			
			if (selector == null)
			{
				selector = GameObject.FindGameObjectWithTag("Selector").GetComponent<Selector>();
			}
			
			Vector3 selectorPos = selector.transform.position;
			transform.position = new Vector3(selectorPos.x+positionOffset.x,
			                                 selectorPos.y+positionOffset.y,
			                                 selectorPos.z+positionOffset.z);
			transform.eulerAngles = cameraRotation;
		}
	}
	
	protected override void UpdateTransform()
	{
		switch(InputMethod.getInputCode())
		{
		case InputModeCode.CONTROLLER:
			transform.position = new Vector3(selector.transform.position.x,
			                                 transform.position.y,
			                                 selector.transform.position.z);
			break;

		default:
		MoveCamera(InputMethod.getAxisRaw("Horizontal"),
		           InputMethod.getAxisRaw("Vertical"));
			break;
		}
	}

	protected override void UpdateZoom()
	{
		float zoomammount = InputMethod.getAxis ("Zoom");
		//Debug.Log ("Scroll: " + zoomammount);

		GetComponent<Camera>().orthographicSize -= zoomammount;

		GetComponent<Camera>().orthographicSize = Mathf.Clamp (GetComponent<Camera>().orthographicSize, MIN_CAMERA_SIZE, MAX_CAMERA_SIZE);
		childCamera.orthographicSize = GetComponent<Camera>().orthographicSize;
	}
	
	public void MoveCamera(float xMovement, float zMovement)
	{
		Vector3 pos = transform.position;

		float xDiff = xMovement*CAMERA_MOVE_SPEED;
		float zDiff = zMovement*CAMERA_MOVE_SPEED;
		
		if ((pos.x+xDiff > GlobalValues.upperBounds.x) ||
			(pos.x+xDiff < GlobalValues.lowerBounds.x))
		{
			xDiff = 0;
		}
		if ((pos.z+zDiff > GlobalValues.upperBounds.y) ||
		    (pos.z+zDiff < GlobalValues.lowerBounds.y))
		{
			zDiff = 0;
		}
		
		transform.position = new Vector3(pos.x+xDiff,pos.y,pos.z+zDiff);
	}
	
	public void ChangeZoom(bool zoomIn)
	{
		if (zoomIn)
		{
			if (GetComponent<Camera>().orthographicSize > MIN_CAMERA_SIZE)
			{
				GetComponent<Camera>().orthographicSize -= CAMERA_ZOOM_SPEED;
				childCamera.orthographicSize = GetComponent<Camera>().orthographicSize;
			}
		}
		else
		{
			if (GetComponent<Camera>().orthographicSize < MAX_CAMERA_SIZE)
			{
				GetComponent<Camera>().orthographicSize += CAMERA_ZOOM_SPEED;
				childCamera.orthographicSize = GetComponent<Camera>().orthographicSize;
			}
		}
	}
}
