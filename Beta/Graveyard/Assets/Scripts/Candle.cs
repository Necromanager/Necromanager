using UnityEngine;
using System.Collections;

public class Candle : MonoBehaviour
{
	public Color activeColor = new Color(234, 131, 0);
	public Color inactiveColor = new Color(0, 255, 234);

	private Light candleLight;
	public ParticleSystem ps;
	public ParticleSystem smokeParticles;
	private float originalIntensity;
	private float scrollingIntensity;
	private float originalParticleSize;
	private float originalParticleVelocity;

	private float flareBoost = 1.0f;

	bool activated = true;

	public void Start()
	{
		candleLight = GetComponentInChildren<Light> ();
		candleLight.color = activeColor;
		originalIntensity = candleLight.intensity;
		scrollingIntensity = originalIntensity;

		if (!candleLight || !ps)
		{
			candleLight = GetComponentInChildren<Light> ();
			ps = GetComponentsInChildren<ParticleSystem> ()[0];
		}
		smokeParticles = GetComponentsInChildren<ParticleSystem> ()[1];
		
		originalParticleSize = ps.startSize;
		originalParticleVelocity = ps.startSpeed;
	}

	public void Activate(bool active)
	{ 
		if (!candleLight || !ps)
		{
			candleLight = GetComponentInChildren<Light> ();
			ps = GetComponentInChildren<ParticleSystem> ();
		}
		smokeParticles = GetComponentsInChildren<ParticleSystem> ()[1];

		if (active)
		{
			candleLight.color = activeColor;
			ps.startColor = Color.white;

			if (!activated)
				FlareCandle();
		}
		else
		{
			candleLight.color = inactiveColor;
			ps.startColor = new Color(0, 226, 195);

			if (smokeParticles != null && activated)
			{
				//Debug.Log ( "smoke: " + (smokeParticles == null) );
				smokeParticles.Stop();
				smokeParticles.Play();
			}
		}

		activated = active;
	}

	protected void Update()
	{
		flareBoost = Mathf.Max(flareBoost - Time.deltaTime, 1.0f);

		scrollingIntensity = Mathf.Clamp(scrollingIntensity + Time.deltaTime * Random.Range (-1.7f, 1.7f), 0.75f * originalIntensity, 1.1f * originalIntensity);
		candleLight.intensity = scrollingIntensity * flareBoost;
		ps.startSize = originalParticleSize * scrollingIntensity * 1.5f * flareBoost;
		ps.startSpeed = originalParticleVelocity * scrollingIntensity * 1.5f * flareBoost;
		//if (activated)
			//ps.startColor = activeColor * flareBoost;
		//else
			//ps.startColor = inactiveColor;
 	}

	public void FlareCandle()
	{
		flareBoost = 5.0f;
	}
}
