using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour {

	List<TutorialObjective> tutorials;
	TutorialPopup popup;
	[SerializeField] TutorialPopup popupSmall;
	[SerializeField] TutorialPopup popupTitle;
	public int numTutorials;
	bool previousBig = true;
	protected AudioSource myAudio;

	// Use this for initialization
	void Start ()
	{
		myAudio = GetComponent<AudioSource>();
		tutorials = new List<TutorialObjective> ();

		//Introduction
		tutorials.Add (new TutorialObjective("Hello and welcome to Necromanager!", true, "Intro/Intro_01"));
		tutorials.Add (new TutorialObjective("In this tutorial we will explain how you stop the zombie apocalypse.", true, "Intro/Intro_02"));
		tutorials.Add (new TutorialObjective("As a gravekeeper, you are the first line of defense. " +
			"The townspeople depend on you not to let a single zombie escape.", true, "Intro/Intro_03"));
		tutorials.Add (new TutorialObjective("If one of them does, it will take a bite of your own wallet. " +
			"And if enough of them do you will be fired on the spot.", true, "Intro/Intro_04"));

		//Part1: movement
		tutorials.Add (new TutorialObjective("Zombies will escape through the main gate, so protect it well.", false, "Messages/Instructions_01"));
		tutorials.Add (new MoveObjective("You can move around using [WASD] or [Left Stick].", false, "Messages/Instructions_02"));

		//Part2: Zambies
		tutorials.Add (new SpawnZombieObjective("Oh No! A zombie came out of it's grave!", false, "Messages/Instructions_03"));
		tutorials.Add (new HitZombieObjective("Quickly, run up to it an hit [Left Mouse] or [RT] to smack it with your shovel!", false, "Messages/Instructions_03_2"));

		tutorials.Add (new TutorialObjective("While a zombie is stunned, run up to it and press [Right Mouse] or [A] to pick it up.", false, "Messages/Instructions_06"));
		tutorials.Add (new TutorialObjective("Now that you are carrying that zombie there run back to an empty grave.", false, "Messages/Instructions_07"));
		tutorials.Add (new TutorialObjective("When a zombie gets out of its grave, the candle on top of it will turn blue. " +
			"While a zombie is in the grave, the candle will burn red.", false, "Messages/Instructions_05"));

		tutorials.Add (new PickUpZombieObjective("While standing over an empty grave, press [Right Mouse] or [A] to " +
			"shove that zombie back where it came from.", false, "Messages/Instructions_08"));

		//money
		tutorials.Add (new TutorialObjective("The more zombies you put back in the grave over the course of the night, the more money you'll earn.", true, "Messages/Instructions_11"));
		tutorials.Add (new TutorialObjective("Money can be spent on various items and buildings to help keep the zombies in the graveyard.", true, "Messages/Instructions_13"));

		/*
		//a zombie spawned! smack it!
		tutorials.Add (new SpawnZombieObjective("Oh Noez! The zombies are escaping!", false));
		tutorials.Add (new HitZombieObjective("Press [Left Mouse] or [RT] to stun the zombie with your shovel.", false));

		//it is stunned. put in hole.
		tutorials.Add (new TutorialObjective("Now that the zombie is stunned, you can pick it up with [Right Mouse] or [A].", false));
		tutorials.Add (new PickUpZombieObjective("Press [Right Mouse] or [A] again while standing on a grave to shove the zombie in.", false));

		//GG
		tutorials.Add (new TutorialObjective("Thanks for playing!", true));
		*/
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (InputMethod.getButtonDown ("Quit"))
		{
			GlobalValues.setPause(false);
			Application.LoadLevel(1);
		}

		numTutorials = tutorials.Count;

		if(previousBig != tutorials[0].isBig())
		{
			previousBig = tutorials[0].isBig();
			popupSmall.GetComponent<CanvasGroup>().alpha = 0;
			popupTitle.GetComponent<CanvasGroup>().alpha = 0;
		}
		if(tutorials[0].isBig())
		{
			popup = popupTitle;
		}
		else
		{
			popup = popupSmall;
		}


		switch(tutorials[0].getState())
		{

		case ObjectiveState.START:
			GlobalValues.setPause(true);
			popup.GetComponent<CanvasGroup>().alpha = 1;
			popup.myText.text = tutorials[0].getMessage();
			tutorials[0].onShowMessage();
			myAudio.PlayOneShot(tutorials[0].getClip());
			tutorials[0].setState(ObjectiveState.MESSAGE);
			break;

		case ObjectiveState.MESSAGE:
			if(InputMethod.getButtonDown("Submit") ||
			   InputMethod.getButtonDown("SwingShovel"))
			{
				tutorials[0].attatchMyEvents();
				tutorials[0].setState(ObjectiveState.GAMEPLAY);
			}
			break;

		case ObjectiveState.GAMEPLAY:
			tutorials[0].checkGameplay();
			if(tutorials[0].getState() == ObjectiveState.GAMEPLAY)
			{
				GlobalValues.setPause(false);
				popup.GetComponent<CanvasGroup>().alpha = 0;
			}
			break;

		case ObjectiveState.COMPLETE:
			myAudio.Stop();
			tutorials[0].detatchMyEvents();
			tutorials.RemoveAt(0);
			break;
		}

		if(tutorials.Count == 0)
		{
			GlobalValues.setPause(false);
			Application.LoadLevel(1);
		}
	}
}
