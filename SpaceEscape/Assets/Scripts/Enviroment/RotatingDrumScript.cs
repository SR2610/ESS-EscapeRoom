using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingDrumScript : MonoBehaviour {

	public float RotationSpeed = 5F;
	
	void Update () { //Rotates the space station barrel smoothly
		transform.Rotate(new Vector3(0,RotationSpeed * Time.deltaTime,0));

	}
}
