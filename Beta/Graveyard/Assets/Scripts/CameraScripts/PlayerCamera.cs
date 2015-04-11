using UnityEngine;
using System.Collections;

public class PlayerCamera : CameraScript
{
	[SerializeField] private float Fov = 60;
	[SerializeField] private PlayerScript player;
	[SerializeField] private float speed;

	private bool shaking = false;
	private float shakeIntensity = 0;
	private float shakeSpeed = 0;
	private float shakeDuration = 0;
	private float totalDuration = 0;
	//private float curShakeTime = 0;

	public override void Activate()
	{
		if (!this.enabled)
		{
			this.enabled = true;
			GetComponent<Camera>().orthographic = false;
			GetComponent<Camera>().fieldOfView = Fov;
			shaking = false;
			shakeIntensity = 0;
			shakeSpeed = 0;
			shakeDuration = 0;
			totalDuration = 0;
			//curShakeTime = 0;
		}
	}
	
	protected override void UpdateTransform()
	{
		if (player == null)
		{
			player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
		}                                
		
		if (shaking)
		{
			//transform.position = GetShakePosition();
			//transform.position = Vector3.Lerp(transform.position,GetShakePosition(),shakeSpeed);
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(GetShakeRotation()), shakeSpeed);
		}
		else
		{
			transform.position = Vector3.Lerp(transform.position, GetPlayerPosition(), speed);
			//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(cameraRotation), shakeSpeed);
			transform.eulerAngles = cameraRotation;
		}
		
		//transform.eulerAngles = cameraRotation;
		
		GetComponent<Camera>().fieldOfView = Fov;
	}

	protected override void UpdateZoom()
	{

	}
	
	private Vector3 GetPlayerPosition()
	{
		Vector3 playerPos = player.transform.position;
		return new Vector3(playerPos.x+positionOffset.x,
		                   playerPos.y+positionOffset.y,
		                   playerPos.z+positionOffset.z);
	}
	
	private Vector3 GetShakePosition()
	{
		float xOffset = Random.Range(-shakeIntensity,shakeIntensity);
		float yOffset = Random.Range(-shakeIntensity,shakeIntensity);
		Vector3 shakePos = GetPlayerPosition();
		
		shakePos = new Vector3(shakePos.x+xOffset,shakePos.y+yOffset,shakePos.z);
		
		shakeDuration -= Time.deltaTime;
		if (shakeDuration <= 0)
		{
			shaking = false;
		}
		
		return shakePos;
	}

	private Vector3 GetShakeRotation()
	{
		//float zOffset = Random.Range (-shakeIntensity, shakeIntensity);
		float zOffset = shakeIntensity;
		if (Random.value < .5f)
		{
			zOffset *= -1;
		}
		zOffset *= (shakeDuration / totalDuration);
		//Vector3 shakeRot = transform.rotation.eulerAngles;
		Vector3 shakeRot = cameraRotation;

		shakeRot = new Vector3(shakeRot.x, shakeRot.y, shakeRot.z+zOffset);
		
		shakeDuration -= Time.deltaTime;
		if (shakeDuration <= 0)
		{
			shaking = false;
		}
		
		return shakeRot;
	}
	
	public void ShakeCamera(float intensity, float speed, float duration)
	{
		shaking = true;
		shakeIntensity = intensity;
		shakeSpeed = speed;
		shakeDuration = duration;
		totalDuration = shakeDuration;
	}
}
