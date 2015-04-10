using UnityEngine;
using System.Collections;

public class GlobalValues : MonoBehaviour 
{
	private const int START_HOUR = 11;
	private const int START_MINUTE = 0;
	private const int END_HOUR = 6;
	private const int END_MINUTE = 0;
	private const int DAY_MINUTES = 360;
	private const int TOTAL_MINUTES = 420;
	public static float MINUTE_TIME = 0.25f;
	public static int DIFFICULTY_INCREASE = 3;

	public static float startDifficulty = 0.1f;
	public static float difficulty = 0.1f;
	public static int curDiffIncrease = 0;
	public static int size = 2;
	public static float money = 200;
	public static int day = 1;
	public static int hour = 11;
	public static int minute = 0;

	public static bool morning = false;
	public static bool musicOff = false;
	public static bool soundOff = false;
	public static bool inGame = false;
	public static bool paused = false;
	public static bool tutorials = false;
	public static bool wonGame = false;

	public static DifficultyValues minWaveTime;
	public static DifficultyValues maxWaveTime;
	public static DifficultyValues minWaveZombies;
	public static DifficultyValues maxWaveZombies;

	public static int maxZombies = 40;
	
	public static int zombiesReturned = 0;
	public static int zombiesEscaped = 0;
	public static int zombiesKilled = 0;
	public static float returnMoney = 3.50f;
	public static float escapeMoney = 5.00f;
	
	private static int minutesPassed = 0;
	
	public static float GetWaveTime()
	{
		return Random.Range(minWaveTime.getVal(size, difficulty), maxWaveTime.getVal(size, difficulty));
	}
	
	public static int GetNumZombies()
	{
		return (int)Random.Range(minWaveZombies.getVal(size, difficulty), maxWaveZombies.getVal(size, difficulty)+1);
	}
	
	public static void IncrementDifficulty()
	{
		curDiffIncrease++;
		if (curDiffIncrease >= DIFFICULTY_INCREASE)
		{
			curDiffIncrease = 0;
			float newDiff = difficulty*100;
			newDiff = Mathf.Floor(newDiff += 20.0f);
			
			difficulty = newDiff/100;
		}
	}
	
	public static void IncrementMenuDifficulty(float mult)
	{
		curDiffIncrease = 0;
		float newDiff = difficulty*100;
		newDiff = Mathf.Floor(newDiff += 20.0f * mult);
		
		difficulty = newDiff/100;
	}
	
	public static string GetDifficultyString()
	{
		if ((0.3f > difficulty) && (difficulty >= 0.1f))
		{
			return "Easy"; 
		}
		else if ((0.5f > difficulty) && (difficulty >= 0.3f))
		{
			return "Normal";
		}
		else if ((0.7f > difficulty) && (difficulty >= 0.5f))
		{
			return "Hard";
		}
		else if ((0.9f > difficulty) && (difficulty >= 0.7f))
		{
			return "Harder";
		}
		else 
		{
			return "#2SPOOKY";
		}
	}
	
	public static void IncrementSize(int num)
	{
		size += num;
		
		if (size > 3)
			size = 1;
		if (size < 1)
			size = 3;
	}
	
	public static string GetSizeString()
	{
		switch(size)
		{
		case 1:
			return "Small";
		case 2:
			return "Normal";
		case 3:
			return "Huge";
		default:
			return "";
		}
	}
	
	public static void TogglePause()
	{
		paused = !paused;
		updateGameSpeed ();
	}

	public static void setPause(bool tf)
	{
		paused = tf;
		updateGameSpeed();
	}

	public static void updateGameSpeed()
	{
		if (paused)
		{
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}
	}
	
	public static void ToggleTutorials()
	{
		tutorials = !tutorials;
	}
	
	public static void AddMoney(float newMoney)
	{
		money += newMoney;
	}

	public static void SpendMoney(float cost)
	{
		money -= cost;
	}
	
	public static bool CanSpendMoney(float cost)
	{
		return (money >= cost);
	}
	
	public static bool IsBankrupt()
	{
		return (money <= 0.0f);
	}
	
	public static string GetMoneyString()
	{
		return "$"+money.ToString("F2");
	}
	
	public static string GetMoneyString(float moneyAmount)
	{
		return "$"+moneyAmount.ToString("F2");
	}
	
	public static void ResetTime()
	{
		hour = START_HOUR;
		minute = START_MINUTE;
		minutesPassed = 0;
	}
	
	public static bool IsTimeUp()
	{
		return ((hour == END_HOUR) && (minute == END_MINUTE));
	}
	
	public static void EndTime()
	{
		hour = END_HOUR;
		minute = END_MINUTE;
	}
	
	public static void PassTime(int addHours,int addMinutes)
	{
		hour += addHours;
		minute += addMinutes;
		minutesPassed += addMinutes;
		
		if (minute >= 60)
		{
			minute = 0;
			hour++;
			
			if (hour == 12)
			{
				morning = !morning;
			}
		    else if (hour > 12)
			{
				hour = 1;
			}
		}
	}
	
	public static float GetTimePercentage()
	{
		if (minutesPassed < DAY_MINUTES)
		{	
			return 0;
		}
		else
		{
			float passed = minutesPassed-DAY_MINUTES;
			float total = TOTAL_MINUTES-DAY_MINUTES;
			return (passed/total);
		}
	}
	
	public static string GetTimeDayString()
	{
		return GetTimeString()+" - "+GetDayString();
	}
	
	public static string GetDayString()
	{
		return "Night "+day.ToString();
	}
	
	public static string GetTimeString()
	{
		string hourText;
		string minuteText;
		string morningText;
		
		if (hour < 10)
		{
			hourText = "0"+hour.ToString();
		}
		else
		{
			hourText = hour.ToString();
		}
		
		if (minute < 10)
		{
			minuteText = "0"+minute.ToString();
		}
		else
		{
			minuteText = minute.ToString();
		}
		
		if (morning)
		{
			morningText = "AM";
		}
		else
		{
			morningText = "PM";
		}
		
		return (hourText+":"+minuteText+" "+morningText);
	}
	
	public static void ResetZombieNums()
	{
		zombiesReturned = 0;
		zombiesEscaped = 0;
		zombiesKilled = 0;
	}

	public static int getStartHour() { return START_HOUR; }
}
