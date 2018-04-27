using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingPuzzleScript : MonoBehaviour
{

	public Texture2D image;
	public int PerLine = 4;
	public int ShuffleMoves = 20;
	public float MoveTime = .2f;
	public float ShuffleMoveTime = .1f;

	private enum PuzzleState { Solved, Shuffling, InPlay };
	private PuzzleState CurrentState;

	SegmentInteractionScript EmptySegment;
	SegmentInteractionScript[,] Segments;
	Queue<SegmentInteractionScript> Inputs;
	bool IsMoving;
	int ShuffleMovesLeft;
	Vector2 ShuffleOFfset;


	void Start()
	{
		CreatePuzzle();
		if (CurrentState == PuzzleState.Solved)
		{
			StartShuffle();
		}
		transform.localScale = new Vector3(0.2F, 0.2f, 0.2f);
	}

	void Update()
	{


	}

	void CreatePuzzle()
	{
		Segments = new SegmentInteractionScript[PerLine, PerLine];
		Texture2D[,] Slices = PuzzleSlicerScript.GetSlices(image, PerLine);
		for (int y = 0; y < PerLine; y++)
		{
			for (int x = 0; x < PerLine; x++)
			{
				GameObject blockObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
				blockObject.transform.position = transform.position + (Vector3)(-Vector2.one * (PerLine - 1) * .5f + new Vector2(x, y));
				blockObject.transform.parent = transform;

				SegmentInteractionScript block = blockObject.AddComponent<SegmentInteractionScript>();
				block.TilePressedEvent += PlayerMoveBlockInput;
				block.TileMovedEvent += OnBlockFinishedMoving;
				block.Init(new Vector2(x, y), Slices[x, y]);
				Segments[x, y] = block;

				if (y == 0 && x == PerLine - 1)
				{
					EmptySegment = block;
				}
			}
		}

		Inputs = new Queue<SegmentInteractionScript>();
	}

	void PlayerMoveBlockInput(SegmentInteractionScript blockToMove)
	{
		if (CurrentState == PuzzleState.InPlay)
		{
			Inputs.Enqueue(blockToMove);
			MakeNextPlayerMove();
		}
	}

	void MakeNextPlayerMove()
	{
		while (Inputs.Count > 0 && !IsMoving)
		{
			MoveBlock(Inputs.Dequeue(), MoveTime);
		}
	}

	void MoveBlock(SegmentInteractionScript blockToMove, float duration)
	{
		if ((blockToMove.Position - EmptySegment.Position).sqrMagnitude == 1)
		{
			Segments[(int)blockToMove.Position.x, (int)blockToMove.Position.y] = EmptySegment;
			Segments[(int)EmptySegment.Position.x, (int)EmptySegment.Position.y] = blockToMove;

			Vector2 targetCoord = EmptySegment.Position;
			EmptySegment.Position = blockToMove.Position;
			blockToMove.Position = targetCoord;

			Vector2 targetPosition = EmptySegment.transform.position;
			EmptySegment.transform.position = blockToMove.transform.position;
			blockToMove.MoveToPosition(targetPosition, duration);
			IsMoving = true;
		}
	}

	void OnBlockFinishedMoving()
	{
		IsMoving = false;
		CheckIfSolved();

		if (CurrentState == PuzzleState.InPlay)
		{
			MakeNextPlayerMove();
		}
		else if (CurrentState == PuzzleState.Shuffling)
		{
			if (ShuffleMovesLeft > 0)
			{
				MakeNextShuffleMove();
			}
			else
			{
				CurrentState = PuzzleState.InPlay;
			}
		}
	}

	void StartShuffle()
	{
		CurrentState = PuzzleState.Shuffling;
		ShuffleMovesLeft = ShuffleMoves;
		EmptySegment.gameObject.SetActive(false);
		MakeNextShuffleMove();
	}

	void MakeNextShuffleMove()
	{
		Vector2[] offsets = { new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1) };
		int randomIndex = Random.Range(0, offsets.Length);

		for (int i = 0; i < offsets.Length; i++)
		{
			Vector2 offset = offsets[(randomIndex + i) % offsets.Length];
			if (offset != ShuffleOFfset * -1)
			{
				Vector2 moveBlockCoord = EmptySegment.Position + offset;

				if (moveBlockCoord.x >= 0 && moveBlockCoord.x < PerLine && moveBlockCoord.y >= 0 && moveBlockCoord.y < PerLine)
				{
					MoveBlock(Segments[(int)moveBlockCoord.x, (int)moveBlockCoord.y], ShuffleMoveTime);
					ShuffleMovesLeft--;
					ShuffleOFfset = offset;
					break;
				}
			}
		}

	}

	void CheckIfSolved()
	{
		foreach (SegmentInteractionScript IndividualSegment in Segments)
		{
			if (!IndividualSegment.IsAtStartingCoord())
			{
				return;
			}
		}
		CurrentState = PuzzleState.Solved;
		GameObject.Find("GameManager").GetComponent<GameManagerScript>().PuzzleSolved(0, 2);
		EmptySegment.gameObject.SetActive(true);
	}
}
