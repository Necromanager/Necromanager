using UnityEngine;
using System.Collections;

public class MoveObjective : TutorialObjective
{
	TutorialCollider col;

	public MoveObjective(string m, bool b, string fp) : base(m, b, fp)
	{
		col = GameObject.FindGameObjectWithTag("TutorialCollider").GetComponent<TutorialCollider>();
	}


	public override void attatchMyEvents()
	{
		base.attatchMyEvents ();
		//ObjectiveEvents.movementEvent += onMove;
	}
	
	public override void detatchMyEvents()
	{
		base.detatchMyEvents ();
		//ObjectiveEvents.movementEvent -= onMove;

	}


	public override void checkGameplay()
	{
		if(col.triggered)
		{
			state = ObjectiveState.COMPLETE;
		}
	}
}
