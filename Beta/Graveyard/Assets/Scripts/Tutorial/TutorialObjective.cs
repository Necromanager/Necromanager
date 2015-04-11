using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum ObjectiveState
{
	START,
	MESSAGE,
	GAMEPLAY,
	COMPLETE
}

public class TutorialObjective
{
	protected bool big = true;
	protected string message;
	protected ObjectiveState state = ObjectiveState.START;
	
	
	public TutorialObjective(string m, bool shouldBeBig)
	{
		attatchMyEvents();
		message = m;
		big = shouldBeBig;
	}
	
	public virtual void attatchMyEvents()
	{
		
	}
	
	public virtual void detatchMyEvents()
	{
		
	}
	
	public virtual void onShowMessage()
	{
		
	}
	
	public virtual void checkGameplay()
	{
		state = ObjectiveState.COMPLETE;
	}
	
	public bool isComplete(){ return (state == ObjectiveState.COMPLETE); }
	public ObjectiveState getState(){ return state; }
	public void setState(ObjectiveState s){state = s;}
	
	public bool isBig(){return big;}
	public string getMessage(){return message;}
}