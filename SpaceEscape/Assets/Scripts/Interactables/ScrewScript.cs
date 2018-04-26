using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewScript : InteractableObjectScript {

	public GameObject LinkedPanel;

	private void Start()
	{
		if (LinkedPanel != null)
			if (LinkedPanel.GetComponent<PanelScript>())
				LinkedPanel.GetComponent<PanelScript>().SetupScrew();
			else
				Destroy(gameObject);
	}

	public override void Interact()
	{
		if (GameObject.Find("FirstPersonItem").GetComponent<PlayerControlScript>().HeldItem == PlayerControlScript.Item.Screwdriver) {
			LinkedPanel.GetComponent<PanelScript>().RemoveScrew();
			Destroy(gameObject);
		}
	}

	public override string FormatTooltip(bool UsingController)
	{
		if (GameObject.Find("FirstPersonItem").GetComponent<PlayerControlScript>().HeldItem == PlayerControlScript.Item.Screwdriver)
			return "Press " + (UsingController ? "X" : "F") + " to Unscrew " + TooltipName;
		else
			return "Screwdriver Required to Unscrew " + TooltipName;
	}
}
