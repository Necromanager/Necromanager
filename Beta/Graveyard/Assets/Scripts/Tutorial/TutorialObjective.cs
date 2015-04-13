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
	protected AudioClip clip;
	
	
	public TutorialObjective(string m, bool shouldBeBig, string clipPath)
	{
		attatchMyEvents();
		message = m;
		big = shouldBeBig;
		clip = Resources.Load<AudioClip>("Sounds/Effects/Tutorial/" + clipPath);
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
	public AudioClip getClip(){return clip;}
}