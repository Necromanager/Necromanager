using UnityEngine;
using System.Collections;

public class HitZombieObjective : TutorialObjective {

	float timesTriggered = 0;
	float timesToComplete = 1;
	
	
	public HitZombieObjective(string m, bool b) : base(m, b)
	{
		
	}
	
	
	public override void attatchMyEvents()
	{
		base.attatchMyEvents ();
		ObjectiveEvents.hitZombieEvent += onHit;
	}
	
	public override void detatchMyEvents()
	{
		base.detatchMyEvents ();
		ObjectiveEvents.hitZombieEvent -= onHit;
		
	}
	
	void onHit()
	{
		timesTriggered++;
	}
	
	public override void checkGameplay()
	{
		if(timesTriggered >= timesToComplete)
		{
			state = ObjectiveState.COMPLETE;
		}
	}
}
