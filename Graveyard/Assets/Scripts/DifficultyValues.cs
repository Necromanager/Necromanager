using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class Values
{
	public float easyValue;
	public float normalValue;
	public float hardValue;
	public float harderValue;
	public float spookyValue;
}

[Serializable]
public class DifficultyValues
{
	public Values smallValues;
	public Values normalValues;
	public Values hugeValues;
	/*public float easyValue;
	public float normalValue;
	public float hardValue;
	public float harderValue;
	public float spookyValue;*/

	private Values getValues(int size)
	{
		if (size == 1) 
		{
			return smallValues;
		} 
		else if (size == 2)
		{
			return normalValues;
		}
		else
		{
			return hugeValues;
		}
	}

	public float getVal(int size, float difficulty)
	{
		if ((0.3f > difficulty) && (difficulty >= 0.1f))
		{
			return getValues(size).easyValue; 
		}
		else if ((0.5f > difficulty) && (difficulty >= 0.3f))
		{
			return getValues(size).normalValue;
		}
		else if ((0.7f > difficulty) && (difficulty >= 0.5f))
		{
			return getValues(size).hardValue;
		}
		else if ((0.9f > difficulty) && (difficulty >= 0.7f))
		{
			return getValues(size).harderValue;
		}
		else 
		{
			return getValues(size).spookyValue;
		}
	}
}
