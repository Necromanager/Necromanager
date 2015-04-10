using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundEffects : MonoBehaviour {

	public AudioClip error;

	[Header("Game Phase")]
	public AudioClip clockChime;
	public List<AudioClip> bury;
	public AudioClip zombieEscaped;

	[Header("Store Phase")]
	public AudioClip purchaseItem;
	public AudioClip soldOut;
	public AudioClip enterStore;
	public AudioClip leaveStore;
	
	[Header("Build Phase")]
	public AudioClip placeFlower;
	public AudioClip placeBrain;
	public AudioClip placeTunnel;
	public AudioClip placeSpotlight;
	public AudioClip placeBell;
	public AudioClip removeBuilding;
	public AudioClip buildSwitchItem;
	public AudioClip finishBuildPhase;

	[Header("UI")]
	public AudioClip switchItem;
	public AudioClip deathScreen;
	public AudioClip moveCurser;
	public AudioClip menuSelect;
	public AudioClip menuCancel;

	[Header("Items")]
	public AudioClip usePlaceholder;
	public AudioClip useShovel;
	public List<AudioClip> shovelHit;
	public List<AudioClip> usePickaxe;
	public AudioClip useCoffee;
	public AudioClip useGum;
	public AudioClip useGel;

	[Header("Buildings")]
	public AudioClip bellRing;

	[Header("Zombies")]
	public List<AudioClip> zombiePain;
	public List<AudioClip> zombieGroan;
	public AudioClip zombiePickUp;
	public List<AudioClip> omnomnom;
	public AudioClip finishBrain;



	static bool hasRun = false;

	void Awake()
	{
		if(!hasRun)
		{

			SoundEffectLibrary.error = error;

			//Game
			SoundEffectLibrary.clockChime = clockChime;
			SoundEffectLibrary.bury = new List<AudioClip>(bury);
			SoundEffectLibrary.zombieEscaped = zombieEscaped;

			//Store
			SoundEffectLibrary.purchaseItem = purchaseItem;
			SoundEffectLibrary.soldOut = soldOut;
			SoundEffectLibrary.enterStore = enterStore;
			SoundEffectLibrary.leaveStore = leaveStore;

			//Build
			SoundEffectLibrary.placeFlower = placeFlower;
			SoundEffectLibrary.placeBrain = placeBrain;
			SoundEffectLibrary.placeTunnel = placeTunnel;
			SoundEffectLibrary.placeSpotlight = placeSpotlight;
			SoundEffectLibrary.placeBell = placeBell;
			SoundEffectLibrary.removeBuilding = removeBuilding;
			SoundEffectLibrary.finishBuildPhase = finishBuildPhase;
			SoundEffectLibrary.buildSwitchItem = buildSwitchItem;

			//UI
			SoundEffectLibrary.switchItem = switchItem;
			SoundEffectLibrary.deathScreen = deathScreen;
			SoundEffectLibrary.moveCurser = moveCurser;
			SoundEffectLibrary.menuSelect = menuSelect;
			SoundEffectLibrary.menuCancel = menuCancel;

			//Items
			SoundEffectLibrary.usePlaceholder = usePlaceholder;
			SoundEffectLibrary.useShovel = useShovel;
			SoundEffectLibrary.shovelHit = shovelHit;
			SoundEffectLibrary.usePickaxe = usePickaxe;
			SoundEffectLibrary.useCoffee = useCoffee;
			SoundEffectLibrary.useGum = useGum;
			SoundEffectLibrary.useGel = useGel;

			//Buildings
			SoundEffectLibrary.bellRing = bellRing;

			//zombie
			SoundEffectLibrary.zombiePain = zombiePain;
			SoundEffectLibrary.zombieGroan = zombieGroan;
			SoundEffectLibrary.zombiePickUp = zombiePickUp;
			SoundEffectLibrary.omnomnom =  new List<AudioClip>(omnomnom);
			SoundEffectLibrary.finishBrain = finishBrain;


			hasRun = true;
		}
		//all done!
		GameObject.Destroy (gameObject);
	}
}
