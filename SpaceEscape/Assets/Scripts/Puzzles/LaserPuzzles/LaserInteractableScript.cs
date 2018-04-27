using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserInteractableScript : InteractableObjectScript
{
	public GameObject Laser;

	public int laserDistance = 10; //max raycasting distance
	public int laserLimit = 10; //the laser can be reflected this many times
	public LineRenderer laserRenderer; //the line renderer


	private void Update()
	{
		if (!Interactable)
			DrawLaser();
	}

	void DrawLaser()
	{


		int laserReflected = 1; //How many times it got reflected
		int vertexCounter = 1; //How many line segments are there
		bool loopActive = true; //Is the reflecting loop active?
		Vector3 laserDirection = transform.right; //direction of the next laser
		Vector3 lastLaserPosition = transform.position; //origin of the next laser

		laserRenderer.positionCount = 1;
		laserRenderer.SetPosition(0, transform.position);

		while (loopActive)
		{
			RaycastHit hitinfo;
			bool hit = Physics.Raycast(lastLaserPosition, laserDirection, out hitinfo, laserDistance);

			if (hit)
			{
				if (hitinfo.collider.gameObject.tag == "Target")
					SolvePuzzle();

				laserReflected++;
				vertexCounter += 3;
				laserRenderer.positionCount = vertexCounter;
				laserRenderer.SetPosition(vertexCounter - 3, Vector3.MoveTowards(hitinfo.point, lastLaserPosition, 0.01f));
				laserRenderer.SetPosition(vertexCounter - 2, hitinfo.point);
				laserRenderer.SetPosition(vertexCounter - 1, hitinfo.point);
				lastLaserPosition = hitinfo.point;
				laserDirection = Vector3.Reflect(laserDirection, hitinfo.normal);

			}
			else
			{
				laserReflected++;
				vertexCounter++;
				laserRenderer.positionCount = vertexCounter;
				laserRenderer.SetPosition(vertexCounter - 1, lastLaserPosition + (laserDirection.normalized * laserDistance));
				loopActive = false;
			}
			if (laserReflected > laserLimit)
				loopActive = false;
		}
	}
	public override void Interact()
	{
		DrawLaser();
		Interactable = false;
	}

	public override string FormatTooltip(bool UsingController)
	{
		return "Press " + (UsingController ? "X" : "F") + " to Fire " + TooltipName;
	}

	private void SolvePuzzle()
	{
		GameObject.Find("GameManager").GetComponent<GameManagerScript>().PuzzleSolved(1,4);
	}
}