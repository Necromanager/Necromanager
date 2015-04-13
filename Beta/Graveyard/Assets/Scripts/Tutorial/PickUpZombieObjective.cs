using UnityEngine;
using System.Collections;

public class PickUpZombieObjective : TutorialObjective {

	float timesTriggered = 0;
	float timesToComplete = 1;
	
	
	public PickUpZombieObjective(string m, bool b, string fp) : base(m, b, fp)
	{
		
	}
	
	
	public override void attatchMyEvents()
	{
		base.attatchMyEvents ();
		ObjectiveEvents.pickupZombieEvent += onHit;
	}
	
	public override void detatchMyEvents()
	{
		base.detatchMyEvents ();
		ObjectiveEvents.pickupZombieEvent -= onHit;
		
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
