using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAirlockInteraction : InteractableObjectScript
{

	public override void Interact()
	{
		GameObject.Find("GameManager").GetComponent<GameManagerScript>().RemoveTime(60 * 60);
	}

	public override string FormatTooltip(bool UsingController)
	{
			return "Press " + (UsingController ? "X" : "F") + " to Open Airlock and DIE";
	
	}
}