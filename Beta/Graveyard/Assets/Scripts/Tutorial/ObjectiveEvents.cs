using UnityEngine;
using System.Collections;

public static class ObjectiveEvents
{
	public delegate void MovementEvent(float ammount);
	public static event MovementEvent movementEvent;
	
	public static void moveObjective(float ammount)
	{
		if(!Object.ReferenceEquals(null, movementEvent))
		{
			movementEvent(ammount);
		}
	}

	public delegate void HitZombieEvent();
	public static event HitZombieEvent hitZombieEvent;
	
	public static void hitZombieObjective()
	{
		if(!Object.ReferenceEquals(null, hitZombieEvent))
		{
			hitZombieEvent();
		}
	}

	public delegate void PickupZombieEvent();
	public static event PickupZombieEvent pickupZombieEvent;
	
	public static void pickupZombieObjective()
	{
		if(!Object.ReferenceEquals(null, pickupZombieEvent))
		{
			pickupZombieEvent();
		}
	}

}
