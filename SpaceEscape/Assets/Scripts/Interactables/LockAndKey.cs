using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockAndKey : InteractableObjectScript
{


	public enum Type
	{
		LOCK,
		KEY
	}
	public Type LockOrKey;


	public GameObject ToolBox;


	public override void Interact()
	{
		switch (LockOrKey)
		{
			case Type.LOCK:
				if (GameObject.Find("GameManager").GetComponent<GameManagerScript>().HasKey)
				{
					ToolBox.GetComponent<Animation>().Play("ToolBox");
					Destroy(gameObject);
				}
				break;
			case Type.KEY:
				GameObject.Find("GameManager").GetComponent<GameManagerScript>().HasKey = true;
				GameObject.Find("GameManager").GetComponent<AudioManager>().PlaySFX("Pickup", transform);
				Destroy(gameObject,0.1F);
				break;
		}
	}

	public override string FormatTooltip(bool UsingController)
	{
		switch (LockOrKey)
		{
			case Type.LOCK:
				if (GameObject.Find("GameManager").GetComponent<GameManagerScript>().HasKey)
					return "Press " + (UsingController ? "X" : "F") + " to Unlock";
				else
					return "Key Required to Unlock";

			case Type.KEY:
				return "Press " + (UsingController ? "X" : "F") + " to Pickup " + TooltipName;
		}
		return "";
	}
}
