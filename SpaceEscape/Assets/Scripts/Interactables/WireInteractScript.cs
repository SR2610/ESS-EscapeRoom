using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireInteractScript : InteractableObjectScript
{

	public enum InteractType
	{
		WIRE,
		CONTROLLER
	}

	public int[] WiresToCut;
	private int WiresCut = 0;


	private WireInteractScript WireController;
	private GameManagerScript GameManager;

	public float WrongWirePenalty = 300;


	private void Start()
	{
		WireController = transform.parent.gameObject.GetComponent<WireInteractScript>();
		GameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
	}


	public InteractType CurrentType = InteractType.WIRE;

	public int WireNumber = 0;

	public override void Interact()
	{
		if (CurrentType == InteractType.WIRE)
		{
			//Play Wire Cutting Sound
			if (WireController.WiresToCut.ToString().Contains(WireNumber.ToString()))
			{
				WireController.CutWire();
			}
			else
			{
				//Play sound e.g beep
				GameManager.RemoveTime(WrongWirePenalty);
			}
			Destroy(gameObject);
		}
	}

	public override string FormatTooltip(bool UsingController)
	{
		return "Press " + (UsingController ? "X" : "F") + " to Cut " + TooltipName;

	}

	public void CutWire()
	{
		WiresCut++;
		if(WiresToCut.Length == WiresCut)
		{
			GameObject.Find("GameManager").GetComponent<GameManagerScript>().PuzzleSolved(0, 1);
		}
	}
}
