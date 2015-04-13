using UnityEngine;
using System.Collections;

public class SpawnZombieObjective : TutorialObjective {

	float timesTriggered = 0;
	float timesToComplete = 1;
	
	
	public SpawnZombieObjective(string m, bool b, string fp) : base(m, b, fp)
	{
		
	}
	
	
	public override void attatchMyEvents()
	{
		base.attatchMyEvents ();
	}
	
	public override void detatchMyEvents()
	{
		base.detatchMyEvents ();		
	}
	
	void spawnZombie()
	{
		GameObject[] graves = GameObject.FindGameObjectsWithTag ("Grave");
		int r = Random.Range (0, graves.Length);
		graves [r].GetComponent<Grave>().SpawnZombie (0);
		timesTriggered++;
	}

	public override void onShowMessage()
	{
		base.onShowMessage ();
		spawnZombie ();
	}
	
	public override void checkGameplay()
	{
		if(timesTriggered >= timesToComplete)
		{
			state = ObjectiveState.COMPLETE;
		}
	}
}
