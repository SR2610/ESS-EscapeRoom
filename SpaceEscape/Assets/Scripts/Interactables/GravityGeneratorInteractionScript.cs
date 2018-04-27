using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGeneratorInteractionScript : InteractableObjectScript
{

	private int PartsAdded = 0;
	private GameManagerScript GameManager;

	private void Start()
	{
		GameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();

	}

	public override void Interact()
	{
		if(GameManager.GravityGeneratorParts>0)
		{
			GameManager.GravityGeneratorParts--;
			PartsAdded++;
			GameObject.Find("GameManager").GetComponent<AudioManager>().PlaySFX("Weld", transform);
		}
		else if (PartsAdded == 3)
		{
			Interactable = false;
			GameManager.PuzzleSolved(1, 1);
			GameObject.Find("GameManager").GetComponent<AudioManager>().PlaySFX("Fixed", transform);
		}
	}


	public override string FormatTooltip(bool UsingController)
	{
		if (PartsAdded != 3 && GameManager.GravityGeneratorParts == 0)
		{
			return "Number of Missing Parts: " + (3-PartsAdded).ToString();

		}
		else if(GameManager.GravityGeneratorParts>0)
		{
			return "Press " + (UsingController ? "X" : "F") + " to Add Part";
		}
		return "Press " + (UsingController ? "X" : "F") + " to Turn On " + TooltipName;
		
	}

}
