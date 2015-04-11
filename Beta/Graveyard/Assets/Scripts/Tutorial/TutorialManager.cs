using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour {

	List<TutorialObjective> tutorials;
	TutorialPopup popup;
	[SerializeField] TutorialPopup popupSmall;
	[SerializeField] TutorialPopup popupTitle;
	public int numTutorials;

	// Use this for initialization
	void Start ()
	{
		tutorials = new List<TutorialObjective> ();

		//move objective
		tutorials.Add (new TutorialObjective("Hello and welcome to Necromanager!", true));
		tutorials.Add (new MoveObjective("Press [WASD] or [Left Stick] to move the player.", true));

		//a zombie spawned! smack it!
		tutorials.Add (new SpawnZombieObjective("Oh Noez! The zombies are escaping!", false));
		tutorials.Add (new HitZombieObjective("Press [Left Mouse] or [RT] to stun the zombie with your shovel.", false));

		//it is stunned. put in hole.
		tutorials.Add (new TutorialObjective("Now that the zombie is stunned, you can pick it up with [Right Mouse] or [A].", false));
		tutorials.Add (new PickUpZombieObjective("Press [Right Mouse] or [A] again while standing on a grave to shove the zombie in.", false));

		//GG
		tutorials.Add (new TutorialObjective("Thanks for playing!", true));
	}
	
	// Update is called once per frame
	void Update ()
	{

		numTutorials = tutorials.Count;

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
