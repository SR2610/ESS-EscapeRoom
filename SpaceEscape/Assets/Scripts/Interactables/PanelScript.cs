using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelScript : InteractableObjectScript {

	private int NumberOfScrews;

	private void Start()
	{
		Interactable = false;
	}

	public override void Interact()
	{
		Destroy(gameObject);
	}

	public override string FormatTooltip(bool UsingController)
	{
		return "Press " + (UsingController ? "X" : "F") + " to Remove " + TooltipName;

	}

	public void SetupScrew()
	{
		NumberOfScrews++;
	}
	public void RemoveScrew()
	{
		NumberOfScrews--;
		if (NumberOfScrews == 0)
		{
			Interactable = true;
		}
	}
}
