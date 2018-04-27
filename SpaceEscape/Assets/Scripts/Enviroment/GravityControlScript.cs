﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityControlScript : MonoBehaviour
{


	private static Vector3 ZeroG = new Vector3(0F, 0F, 0F);
	private static Vector3 Down = new Vector3(0F, -2.5F, 0F);
	private static Vector3 Up = new Vector3(0F, 2.5F, 0F);
	private static Vector3 Left = new Vector3(-2.5F, 0F, 0F);
	private static Vector3 Right = new Vector3(2.5F, 0F, 0F);
	private static Vector3 Forward = new Vector3(0F, 0F, 2.5F);
	private static Vector3 Back = new Vector3(0F, 0F, -2.5F);

	public static GravityTypes CurrentGravity { get; private set; }

	public enum GravityTypes
	{
		OFF,
		DOWN,
		UP,
		LEFT,
		RIGHT,
		FORWARD,
		BACKWARD
	}



	public static void SetGravity(GravityTypes GravityToSet)
	{
		switch (GravityToSet)
		{
			case GravityTypes.OFF:
				Physics.gravity = ZeroG;
				break;
			case GravityTypes.DOWN:
				Physics.gravity = Down;
				break;
			case GravityTypes.UP:
				Physics.gravity = Up;
				break;
			case GravityTypes.LEFT:
				Physics.gravity = Left;
				break;
			case GravityTypes.RIGHT:
				Physics.gravity = Right;
				break;
			case GravityTypes.FORWARD:
				Physics.gravity = Forward;
				break;
			case GravityTypes.BACKWARD:
				Physics.gravity = Back;
				break;
		}

		CurrentGravity = GravityToSet;


	}

	public static string GetNameOfGravity(GravityTypes Grav) //Returns text friendly name
	{
		switch (Grav)
		{
			case GravityTypes.OFF:
				return "0G";
			case GravityTypes.DOWN:
				return "Y-";
			case GravityTypes.UP:
				return "Y+";
			case GravityTypes.LEFT:
				return "X-";
			case GravityTypes.RIGHT:
				return "X+";
			case GravityTypes.FORWARD:
				return "Z+";
			case GravityTypes.BACKWARD:
				return "Z-";
		}	
	return "0G";
	}
}