using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeInteractableScript : InteractableObjectScript
{
	public GameObject PowerCellBall;

	private void Start()
	{
		PowerCellBall.SetActive(false);

	}
	public override void Interact()
	{
		if (GameObject.Find("GameManager").GetComponent<GameManagerScript>().HasPowerCell)
		{
			PowerCellBall.SetActive(true);
			Interactable = false;
		}
	}

	public override string FormatTooltip(bool UsingController)
	{
		if (GameObject.Find("GameManager").GetComponent<GameManagerScript>().HasPowerCell)
			return "Press " + (UsingController ? "X" : "F") + " to Insert Power Cell";
		else
			return "Power Cell Missing";
	}
}
