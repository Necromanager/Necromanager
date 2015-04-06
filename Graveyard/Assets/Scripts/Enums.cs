using UnityEngine;
using System.Collections;

public enum Direction{UP,DOWN,LEFT,RIGHT};
public enum EnemyType{NORMAL};
public enum ZombieStatus{NORMAL,STUNNED,GRABBED,DEAD};
public enum GameMode{GAME,BUILD,STORE};
public enum Objectives{NULL, MOVE, SMACK_ZOMBIE, PICKUP_ZOMBIE, ZOMBIE_IN_GRAVE, FINISH_NIGHT};

public class Enums : MonoBehaviour 
{
	public static Direction ReverseDirection(Direction dir)
	{
		if (dir == Direction.LEFT)
		{
			return Direction.RIGHT;
		}
		else if (dir == Direction.RIGHT)
		{
			return Direction.LEFT;
		}
		else if (dir == Direction.UP)
		{
			return Direction.DOWN;
		}
		else if (dir == Direction.DOWN)
		{
			return Direction.UP;
		}
		
		return Direction.LEFT;
	}
	
	public static Direction GetNextDirection(Direction dir, float speed)
	{
		if (speed > 0)
		{
			if (dir == Direction.LEFT)
			{
				return Direction.UP;
			}
			else if (dir == Direction.RIGHT)
			{
				return Direction.DOWN;
			}
			else if (dir == Direction.UP)
			{
				return Direction.RIGHT;
			}
			else if (dir == Direction.DOWN)
			{
				return Direction.LEFT;
			}
		}
		else
		{
			if (dir == Direction.LEFT)
			{
				return Direction.DOWN;
			}
			else if (dir == Direction.RIGHT)
			{
				return Direction.UP;
			}
			else if (dir == Direction.UP)
			{
				return Direction.LEFT;
			}
			else if (dir == Direction.DOWN)
			{
				return Direction.RIGHT;
			}
		}
		
		return Direction.UP;
	}
	
	public static bool IsVertical (Direction dir)
	{
		if ((dir == Direction.UP) || (dir == Direction.DOWN))
		{
			return true;
		}
		
		return false;
	}
	
	public static bool IsPositive (Direction dir)
	{
		if ((dir == Direction.UP) || (dir == Direction.RIGHT))
		{
			return true;
		}
		
		return false;
	}


}
