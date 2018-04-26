using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectScript : MonoBehaviour {

	public string TooltipName;
	public bool Interactable = true;

	public virtual void Interact()
	{

	}

	public virtual string FormatTooltip(bool UsingController)
	{
		return "Press " + (UsingController ? "X" : "F") + " to Interact with " + TooltipName;
	}



}
