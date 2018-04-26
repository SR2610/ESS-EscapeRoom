using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractScript : MonoBehaviour
{


	public float RaycastRange = 4.0F;
	private Text ToolTip;
	private GameObject InteractedObject;
	private GameObject LastObject;
	private Color LastColor;
	public bool UsingController = false;

	void Start()
	{
		ToolTip = GameObject.Find("TooltipText").GetComponent<Text>();
		ToolTip.transform.parent.gameObject.SetActive(false);
	}

	void Update()
	{
		var Hits = Physics.RaycastAll(transform.position, transform.forward, RaycastRange);

		InteractedObject = null;
		ToolTip.text = "";

		if (Hits.Length > 0)
		{
			foreach (var item in Hits)
			{
				if (item.transform.gameObject.GetComponent<InteractableObjectScript>() && item.transform.gameObject.GetComponent<InteractableObjectScript>().Interactable)
				{

					InteractedObject = item.transform.gameObject;
					ToolTip.text = item.transform.gameObject.GetComponent<InteractableObjectScript>().FormatTooltip(UsingController);
					if (LastObject != InteractedObject)
					{
						if (LastObject != null)
							LastObject.GetComponent<Renderer>().material.color = LastColor;
						LastObject = InteractedObject;
						LastColor = InteractedObject.GetComponent<Renderer>().material.color;
						InteractedObject.GetComponent<Renderer>().material.color = Color.yellow;
					}
					
				}
					ToolTip.transform.parent.gameObject.SetActive((InteractedObject == null ? false : true));

			}
			


			if (Input.GetButtonDown("Interact"))
			{
				if (InteractedObject != null)
				{
					InteractedObject.GetComponent<InteractableObjectScript>().Interact();
				}
			}


		}
		ToolTip.transform.parent.gameObject.SetActive((InteractedObject == null ? false : true));

		if (InteractedObject == null)
			if (LastObject != null)
				LastObject.GetComponent<Renderer>().material.color = LastColor;



	}
}
