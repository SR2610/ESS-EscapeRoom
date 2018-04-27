using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

	private Animation DoorAnimation;

	void Start () {
		DoorAnimation = GetComponent<Animation>();
	}

	public void UnlockDoor(){ //Plays the door opening animation
		DoorAnimation.Play();
	}
}
