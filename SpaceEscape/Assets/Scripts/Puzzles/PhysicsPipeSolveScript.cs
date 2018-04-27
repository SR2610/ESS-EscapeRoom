using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsPipeSolveScript : MonoBehaviour {

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Ball")
		{
			SolvePuzzle();
			Destroy(collision.gameObject);
		}
	}

	private void SolvePuzzle(){
		GameObject.Find("GameManager").GetComponent<GameManagerScript>().PuzzleSolved(1, 2);
	}
}
