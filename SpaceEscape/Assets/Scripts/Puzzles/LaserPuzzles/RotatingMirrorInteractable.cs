using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingMirrorInteractable : InteractableObjectScript
{
	private Vector3 Rotation = new Vector3(0, 0, 90);

	public override void Interact()
	{
		transform.Rotate(Rotation);
	}

	public override string FormatTooltip(bool UsingController)
	{
			return "Press " + (UsingController ? "X" : "F") + " to Rotate " + TooltipName;
	}
}
