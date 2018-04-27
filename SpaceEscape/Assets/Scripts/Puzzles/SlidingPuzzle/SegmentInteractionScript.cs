using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentInteractionScript : InteractableObjectScript {


	

	public override void Interact()
	{
		if (TilePressedEvent != null)
		{
			TilePressedEvent(this);
		}
	}

	public override string FormatTooltip(bool UsingController)
	{
		return "Press " + (UsingController ? "X" : "F") + " to Move Puzzle Piece";
	}

	public event System.Action<SegmentInteractionScript> TilePressedEvent;
	public event System.Action TileMovedEvent;

    public Vector2 Position;
	Vector2 StartingPosition;

    public void Init(Vector2 StartingPosition, Texture2D image)
    {
        this.StartingPosition = StartingPosition;
        Position = StartingPosition;

        GetComponent<MeshRenderer>().material = Resources.Load<Material>("Block");
        GetComponent<MeshRenderer>().material.mainTexture = image;
    }

    public void MoveToPosition(Vector2 target, float duration)
    {
        StartCoroutine(AnimateMove(target, duration));
    }

   
    IEnumerator AnimateMove(Vector2 TargetPosition, float TimeToMove)
    {
        Vector2 InitialPosition = transform.position;
        float PercentMoved = 0;

        while (PercentMoved < 1)
        {
            PercentMoved += Time.deltaTime / TimeToMove;
            transform.position = Vector2.Lerp(InitialPosition, TargetPosition, PercentMoved);
            yield return null;
        }

        if (TileMovedEvent != null)
        {
            TileMovedEvent();
        }
    }

    public bool IsAtStartingCoord()
    {
        return Position == StartingPosition;
    }
}
