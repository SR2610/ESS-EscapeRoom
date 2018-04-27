using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{


	public float TimeInMinutes = 60F;
	private float TimeLeft;
	private bool TimerRunning = false;

	private float Minutes;
	private float Seconds;

	public bool HasKey = false;
	public bool HasPowerCell = false;
	public int GravityGeneratorParts = 0;


	public DoorScript AirlockDoor;
	public DoorScript GravityRoomDoor;
	public DoorScript EscapePodDoor;



	private bool SlidingBlockPuzzleCompleted = false;
	private bool WireCuttingPuzzleCompleted = false;


	public bool GravityOn = false;
	private bool GravityGeneratorRepaired = false;
	private bool LaserSolved = false;
	public bool PipeSolved = false;




	void Start()
	{
		StartTimer(); //Start Timer as soon as loaded in
	}

	void Update()
	{
		UpdateTimer();//Updates timer every frame
		if (!TimerRunning) //If its not running, time must be up
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScreen");
		}
	}

	public void StartTimer() //Function to Start Timer
	{
		TimeLeft = TimeInMinutes * 60; //Creates time from minutes provided in editor
		TimerRunning = true; //Starts Timer
		UpdateTimer(); //Does initial calculations before first visual update
		StartCoroutine(TimerRoutine()); //Starts the timer courotune to update the displays

	}

	private void UpdateTimer()
	{
		if (!TimerRunning) return;
		TimeLeft -= Time.deltaTime; //Removes time since last frame

		Minutes = Mathf.Floor(TimeLeft / 60); //Calculates minutes and seconds from the seconds
		Seconds = TimeLeft % 60;
		if (Seconds > 59) Seconds = 59;
		if (Minutes < 0) //If there is no minutes left, time is up!
		{
			TimerRunning = false;
			Minutes = 0;
			Seconds = 0;
		}

	}

	public void RemoveTime(float SecondsToRemove)
	{
		TimeLeft -= SecondsToRemove;
	}

	private IEnumerator TimerRoutine()
	{
		while (TimerRunning)
		{
			foreach (GameObject ClocksInLevel in GameObject.FindGameObjectsWithTag("ClockDisplay")) //Finds every clock in the level so that it can be updated
			{
				if (ClocksInLevel.GetComponent<TextMesh>())
				{
					ClocksInLevel.GetComponent<TextMesh>().text = (string.Format("{0:0}:{1:00}", Minutes, Seconds)); //Formats the time to display correct MM:SS
				}
			}
			yield return new WaitForSeconds(0.2f);
		}
	}


	public void PuzzleSolved(int RoomId,int PuzzleID) //Called when a puzzle is completed
	{
		if (RoomId == 0)
		{
			switch (PuzzleID)
			{
				case 1: //Sliding Block Puzzle
					SlidingBlockPuzzleCompleted = true;
					break;
				case 2: //Wire cutting puzzle
					WireCuttingPuzzleCompleted = true;
					break;
				default:
					Debug.Log("Tried to register a different puzzle");
					break;
			}

			if (WireCuttingPuzzleCompleted && SlidingBlockPuzzleCompleted)
			{
				AirlockDoor.UnlockDoor();
				GravityRoomDoor.UnlockDoor();
			}

		}
		else
		{
			switch (PuzzleID)
			{
				case 1: //Gravity Generator Repaired
					GravityGeneratorRepaired = true;	
					break;
				case 2: //Physics Puzzle
					PipeSolved = true;
					break;
				case 4: //Laser Grid Done
					LaserSolved = true;
					break;
				default:
					break;
			}
			if (LaserSolved && GravityGeneratorRepaired)
				GravityOn = true;
		}
	}



}