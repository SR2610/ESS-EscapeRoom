using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlScript : MonoBehaviour {


	private GameObject GravRemote;
	private GameObject Screwdriver;
	private GameObject IDCard;

	public bool HasScrewdriver = false;
	public bool HasGravRemote = false;
	public bool HasIDCard = false;

	private TextMesh DisplayText;


	public enum Item
	{
		Empty,
		GravityRemote,
		Screwdriver,
		IDCard
	}
	public Item HeldItem = Item.Empty;




	void Start () {
		DisplayText = transform.Find("Gravity Remote").Find("Display").gameObject.GetComponent<TextMesh>();
		DisplayText.text = GravityControlScript.GetNameOfGravity(Gravity);

		GravRemote = transform.Find("Gravity Remote").gameObject;
		Screwdriver = transform.Find("Screwdriver").gameObject;
		IDCard = transform.Find("ID").gameObject;

		UpdateHeldItem();
	}

	void Update () {

		HandleSwitchItems();
		UpdateHeldItem();

		switch (HeldItem)
		{
			case Item.GravityRemote:
				HandleGravityRemote();
				break;
			case Item.Empty:
				break;
			case Item.Screwdriver:
				break;
		}
	}


	private void HandleSwitchItems()
	{
		if (Input.GetButtonDown("ItemOne")&&HasScrewdriver)
			HeldItem = Item.Screwdriver;
		else if (Input.GetButtonDown("ItemTwo")&&HasIDCard)
			HeldItem = Item.IDCard;
		else if (Input.GetButtonDown("ItemThree")&&HasGravRemote)
			HeldItem = Item.GravityRemote;
		else if (Input.GetButtonDown("PutAway"))
			HeldItem = Item.Empty;
	
}

	private void UpdateHeldItem()
	{
		GravRemote.SetActive(HeldItem == Item.GravityRemote);
		Screwdriver.SetActive(HeldItem == Item.Screwdriver);
		IDCard.SetActive(HeldItem == Item.IDCard);
	}


	private GravityControlScript.GravityTypes Gravity = GravityControlScript.GravityTypes.OFF;
	private int GravityType = 0;
	private int NumberOfGravityTypes = System.Enum.GetValues(typeof(GravityControlScript.GravityTypes)).Length;

	private void HandleGravityRemote()
	{
		if (Input.GetButtonDown("Cycle"))
		{
			GravityType++;
			if (GravityType == NumberOfGravityTypes)
				GravityType = 0;
			Gravity = (GravityControlScript.GravityTypes)(GravityType);
			DisplayText.text = GravityControlScript.GetNameOfGravity(Gravity);
		}
		else
			if (Input.GetButtonDown("Interact"))
		{
			if (GameObject.Find("GameManager").GetComponent<GameManagerScript>().GravityOn)
				GravityControlScript.SetGravity(Gravity);
			else
				Debug.Log("Offline");
				//Play Error Beep
		}
	}
}