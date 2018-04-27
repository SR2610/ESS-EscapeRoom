using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeInteractionScript : InteractableObjectScript {


	public enum SafeComponent
	{
		SAFE,
		BUTTON
	}

	public SafeComponent ComponentType;

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

	public string DesiredNumber = "1234";
	private string EnteredValue;
	private SafeInteractionScript SafeBase;



	private void Start()
	{
		SafeBase = transform.root.gameObject.GetComponent<SafeInteractionScript>();
	}

	public override void Interact()
	{
		if (ComponentType == SafeComponent.BUTTON)
		{

			switch (Button)
			{
				case ButtonType.NONE:
					break;
				case ButtonType.BUTTONONE:
					SafeBase.PressSafeButton(1);
					break;
				case ButtonType.BUTTONTWO:
					SafeBase.PressSafeButton(2);
					break;
				case ButtonType.BUTTONTHREE:
					SafeBase.PressSafeButton(3);
					break;
				case ButtonType.BUTTONFOUR:
					SafeBase.PressSafeButton(4);
					break;
				case ButtonType.BUTTONFIVE:
					SafeBase.PressSafeButton(5);
					break;
				case ButtonType.BUTTONSIX:
					SafeBase.PressSafeButton(6);
					break;
				case ButtonType.BUTTONSEVEN:
					SafeBase.PressSafeButton(7);
					break;
				case ButtonType.BUTTONEIGHT:
					SafeBase.PressSafeButton(8);
					break;
				case ButtonType.BUTTONNINE:
					SafeBase.PressSafeButton(9);
					break;
			}
		}
	}


	public void PressSafeButton(int Button)
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
		if (transform.Find("Door").gameObject.GetComponent<Animation>())
			transform.Find("Door").gameObject.GetComponent<Animation>().Play("Door");
	}


	public override string FormatTooltip(bool UsingController)
	{
		return "Press " + (UsingController ? "X" : "F") + " to Press " + TooltipName;
	}
}