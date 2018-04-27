using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitKeypadScript : InteractableObjectScript
{


	public enum KeypadComponent
	{
		LOGIC,
		BUTTON
	}

	public KeypadComponent ComponentType;

	public enum ButtonType
	{
		NONE,
		BUTTONONE,
		BUTTONTWO,
		BUTTONTHREE,
		BUTTONFOUR,
		BUTTONFIVE,
		BUTTONSIX,
		BUTTONSEVEN,
		BUTTONEIGHT,
		BUTTONNINE
	}

	public ButtonType Button;

	public string DesiredNumber = "529";
	private string EnteredValue;
	private ExitKeypadScript Logic;



	private void Start()
	{
		Logic = transform.root.gameObject.GetComponent<ExitKeypadScript>();
	}

	public override void Interact()
	{
		if (!GameObject.Find("GameManager").GetComponent<GameManagerScript>().PipeSolved)
			return;
		else
		{

			if (ComponentType == KeypadComponent.BUTTON)
			{

				switch (Button)
				{
					case ButtonType.NONE:
						Logic.PressButton(0);
						break;
					case ButtonType.BUTTONONE:
						Logic.PressButton(1);
						break;
					case ButtonType.BUTTONTWO:
						Logic.PressButton(2);
						break;
					case ButtonType.BUTTONTHREE:
						Logic.PressButton(3);
						break;
					case ButtonType.BUTTONFOUR:
						Logic.PressButton(4);
						break;
					case ButtonType.BUTTONFIVE:
						Logic.PressButton(5);
						break;
					case ButtonType.BUTTONSIX:
						Logic.PressButton(6);
						break;
					case ButtonType.BUTTONSEVEN:
						Logic.PressButton(7);
						break;
					case ButtonType.BUTTONEIGHT:
						Logic.PressButton(8);
						break;
					case ButtonType.BUTTONNINE:
						Logic.PressButton(9);
						break;
				}
			}
		}
	}


	public void PressButton(int Button)
	{
		//Play Button Pressing Sound
		EnteredValue += Button.ToString();
		if (EnteredValue.Length == DesiredNumber.Length)
			CheckCode();
	}

	private void CheckCode()
	{
		if (EnteredValue == DesiredNumber)
			Unlock();
		else
		{
			//Play Sound to Indicate Incorrect
			EnteredValue = "";
		}
	}

	private void Unlock()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("WinningScene");
	}


	public override string FormatTooltip(bool UsingController)
	{
		if (GameObject.Find("GameManager").GetComponent<GameManagerScript>().PipeSolved)
			return "Press " + (UsingController ? "X" : "F") + " to Press " + TooltipName;
		else
			return "Power Offline";

	}
}
