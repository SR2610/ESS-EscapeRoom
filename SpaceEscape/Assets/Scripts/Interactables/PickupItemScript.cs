using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItemScript : InteractableObjectScript {


	public enum PickupType
	{
		SCREWDRIVER,
		IDCARD,
		GRAVREMOTE,
		ENERGYCELL,
		GENPART
	}

	public PickupType Pickup;


	public override void Interact()
	{
		switch (Pickup)
		{
			case PickupType.SCREWDRIVER:
				GameObject.Find("FirstPersonItem").GetComponent<PlayerControlScript>().HasScrewdriver = true;
				break;
			case PickupType.IDCARD:
				GameObject.Find("FirstPersonItem").GetComponent<PlayerControlScript>().HasIDCard = true;
				break;
			case PickupType.GRAVREMOTE:
				GameObject.Find("FirstPersonItem").GetComponent<PlayerControlScript>().HasGravRemote = true;
				break;
			case PickupType.ENERGYCELL:
				GameObject.Find("GameManager").GetComponent<GameManagerScript>().HasPowerCell = true;
				break;
			case PickupType.GENPART:
				GameObject.Find("GameManager").GetComponent<GameManagerScript>().GravityGeneratorParts++;
				break;
		}
		Destroy(gameObject);
	}

	public override string FormatTooltip(bool UsingController)
	{
		return "Press " + (UsingController ? "X" : "F") + " to Pickup " + TooltipName;
	}
}
