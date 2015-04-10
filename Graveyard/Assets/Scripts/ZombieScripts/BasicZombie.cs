using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicZombie : ZombieScript
{
	private List<Tile> gateTiles;
	private Object[] zombieGrowls;

    private Animator zombieAnimator;
    private GameObject zombieModel;
	private SkinnedMeshRenderer zombieRenderer;

    private Vector3 lastPosition;

	public GameObject eye1;
	public GameObject eye2;

	private Color originalColor;

	void Start () 
	{
		Init();
		GetComponent<Renderer>().material.color = Color.green;
        zombieAnimator = GetComponentsInChildren<Animator>()[1];
        zombieModel = zombieAnimator.gameObject;
        lastPosition = transform.position;
		FindGateTiles();

		originalColor = eye1.GetComponent<Light> ().color;

		zombieRenderer = GetComponentInChildren<SkinnedMeshRenderer> ();
		Object[] zombieTexes = Resources.LoadAll("Zombies/Textures/");
		
		zombieRenderer.material.mainTexture = zombieTexes[Random.Range(0, zombieTexes.Length)] as Texture;
	}

    protected override void Update()
    {
        base.Update();

        Vector3 direction = (transform.position - lastPosition).normalized;
        lastPosition = transform.position;

        if (zomStat != ZombieStatus.GRABBED)
            zombieModel.transform.localRotation = Quaternion.AngleAxis(Mathf.Atan2(direction.z, -direction.x) * Mathf.Rad2Deg - 90.0f, Vector3.up);

        zombieAnimator.SetBool("PickedUp", zomStat == ZombieStatus.GRABBED);
        zombieAnimator.SetBool("Stunned", zomStat == ZombieStatus.STUNNED);

		if (zomStat == ZombieStatus.STUNNED)
		{
			eye1.GetComponent<ParticleSystem>().startColor = Color.red;
			eye2.GetComponent<ParticleSystem>().startColor = Color.red;

			eye1.GetComponent<Light>().color = Color.red;
			eye2.GetComponent<Light>().color = Color.red;
		}
		else
		{
			eye1.GetComponent<ParticleSystem>().startColor = originalColor;
			eye2.GetComponent<ParticleSystem>().startColor = originalColor;
			
			eye1.GetComponent<Light>().color = originalColor;
			eye2.GetComponent<Light>().color = originalColor;
		}

        //print(zombieModel.name);
    }

	private void FindGateTiles()
	{
		Tile tempTile;
		gateTiles = new List<Tile>();
		GameObject[] floors = GameObject.FindGameObjectsWithTag("Floor");
		
		foreach(GameObject floor in floors)
		{
			tempTile = floor.GetComponent<Tile>();
			if (tempTile != null)
			{
				if (tempTile.IsGateSpot())
				{
					gateTiles.Add(tempTile);
				}
			}
		}
	}
	
	public override Tile GetGoalTile()
	{
		/*GameObject[] spotLights = GameObject.FindGameObjectsWithTag("Spotlight");
		if (spotLights.Length > 0)
		{
			return homeGrave.GetTile();
		}*/
	
		GameObject[] brainSticks = GameObject.FindGameObjectsWithTag("BrainStick");
		
		foreach (GameObject brainStick in brainSticks)
		{
			Vector3 pos = brainStick.transform.position;
			Vector3 spherePos = new Vector3(pos.x,pos.y-1,pos.z);
			Collider[] below = Physics.OverlapSphere(spherePos,0.4f);
	
			foreach (Collider ob in below)
			{
				if (ob.tag == "Floor")
				{
					/*List<Tile> brainTiles = ob.gameObject.GetComponent<Tile>().GetNeighbors();
					return brainTiles[Random.Range(0,brainTiles.Count)];*/
					return ob.gameObject.GetComponent<Tile>();
				}
			}
		}

		GameObject[] screamBoxes = GameObject.FindGameObjectsWithTag("ScreamBox");
		
		foreach (GameObject screamBox in screamBoxes)
		{
			Vector3 pos = screamBox.transform.position;
			Vector3 spherePos = new Vector3(pos.x,pos.y-0.7f,pos.z);
			Collider[] below = Physics.OverlapSphere(spherePos,0.4f);
			
			foreach (Collider ob in below)
			{
				if (ob.tag == "Floor")
				{
					List<Tile> boxTiles = ob.gameObject.GetComponent<Tile>().GetNeighbors();
					return boxTiles[Random.Range(0,boxTiles.Count)];
				}
			}
		}
	
		return gateTiles[Random.Range(0,gateTiles.Count)];
	}
	
	public override void Stun ()
	{
		if (zomStat == ZombieStatus.NORMAL)
		{

			zomStat = ZombieStatus.STUNNED;
		}
	}
	
	public override void Shoot ()
	{
		LoseHealth(totalHealth);
	}
	
	public override bool Grab()
	{
		if (zomStat == ZombieStatus.STUNNED)
		{
			zomStat = ZombieStatus.GRABBED;
			GlobalFunctions.PlaySoundEffect(SoundEffectLibrary.zombiePickUp);
			return true;
		}
		
		return false;
	}
	
	protected override AudioClip GetSpawnSound()
	{
		return SoundEffectLibrary.zombieGroan[
		       Random.Range(0,SoundEffectLibrary.zombieGroan.Count)];
	}
	
	public override AudioClip GetHitSound()
	{
		return SoundEffectLibrary.zombiePain[
		       Random.Range(0,SoundEffectLibrary.zombiePain.Count)];
	}
	
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Gum")
		{
			Gum gum = col.gameObject.GetComponent<Gum>();
			if (gum == null)
			{
				return;
			}
			if (!gum.HasZombie())
			{
				gum.StickZombie(this);
			}
		}
		else if (col.gameObject.tag == "Gel")
		{
			Gel gel = col.gameObject.GetComponent<Gel>();
			gel.SlipZombie(this);
		}
		else if (col.gameObject.tag == "Bell")
		{
			Bell bell = col.gameObject.GetComponent<Bell>();
			bell.RingBell();
		}
	}
}
