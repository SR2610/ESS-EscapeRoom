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

	private WireInteractScript WireController;


	private void Start()
	{
		WireController = transform.parent.gameObject.GetComponent<WireInteractScript>();
	}


	public InteractType CurrentType = InteractType.WIRE;

	public int WireNumber = 0;

	public override void Interact()
	{
		if (CurrentType == InteractType.WIRE)
		{
			if (WireController.WiresToCut.ToString().Contains(WireNumber.ToString()))
			{
				Debug.Log("Right Wire");
			}
			else
				Debug.Log("Wrong Wire");
			Destroy(gameObject);
		}
	}

	public override string FormatTooltip(bool UsingController)
	{
		return "Press " + (UsingController ? "X" : "F") + " to Cut " + TooltipName;

	}
}
