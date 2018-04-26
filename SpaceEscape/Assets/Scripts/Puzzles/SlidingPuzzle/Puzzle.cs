using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour {

    public Texture2D image;
    public int blocksPerLine = 4;
    public int shuffleLength = 20;
    public float defaultMoveDuration = .2f;
    public float shuffleMoveDuration = .1f;

    private enum PuzzleState { Solved, Shuffling, InPlay };
	private PuzzleState CurrentState;

    Segment emptyBlock;
    Segment[,] Segments;
    Queue<Segment> inputs;
    bool blockIsMoving;
    int shuffleMovesRemaining;
	Vector2 prevShuffleOffset;


	void Start()
    {
        CreatePuzzle();
		if (CurrentState == PuzzleState.Solved)
		{
			StartShuffle();
		}
		transform.localScale = new Vector3(0.1F,0.1f,0.1f);
	}

    void Update()
    {
	

	}

	void CreatePuzzle()
    {
        Segments = new Segment[blocksPerLine, blocksPerLine];
        Texture2D[,] imageSlices = ImageSlicer.GetSlices(image, blocksPerLine);
        for (int y = 0; y < blocksPerLine; y++)
        {
            for (int x = 0; x < blocksPerLine; x++)
            {
                GameObject blockObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
                blockObject.transform.position = transform.position+(Vector3)(-Vector2.one * (blocksPerLine - 1) * .5f + new Vector2(x, y));
                blockObject.transform.parent = transform;

                Segment block = blockObject.AddComponent<Segment>();
                block.TilePressedEvent += PlayerMoveBlockInput;
                block.TileMovedEvent += OnBlockFinishedMoving;
                block.Init(new Vector2(x, y), imageSlices[x, y]);
                Segments[x, y] = block;

                if (y == 0 && x == blocksPerLine - 1)
                {
                    emptyBlock = block;
                }
            }
        }

        inputs = new Queue<Segment>();
    }

    void PlayerMoveBlockInput(Segment blockToMove)
    {
        if (CurrentState == PuzzleState.InPlay)
        {
            inputs.Enqueue(blockToMove);
            MakeNextPlayerMove();
        }
    }

    void MakeNextPlayerMove()
    {
		while (inputs.Count > 0 && !blockIsMoving)
		{
            MoveBlock(inputs.Dequeue(), defaultMoveDuration);
		}
    }

    void MoveBlock(Segment blockToMove, float duration)
    {
		if ((blockToMove.Position - emptyBlock.Position).sqrMagnitude == 1)
		{
            Segments[(int)blockToMove.Position.x, (int)blockToMove.Position.y] = emptyBlock;
            Segments[(int)emptyBlock.Position.x, (int)emptyBlock.Position.y] = blockToMove;

			Vector2 targetCoord = emptyBlock.Position;
			emptyBlock.Position = blockToMove.Position;
			blockToMove.Position = targetCoord;

			Vector2 targetPosition = emptyBlock.transform.position;
			emptyBlock.transform.position = blockToMove.transform.position;
            blockToMove.MoveToPosition(targetPosition, duration);
            blockIsMoving = true;
		}
    }

    void OnBlockFinishedMoving()
    {
        blockIsMoving = false;
        CheckIfSolved();

        if (CurrentState == PuzzleState.InPlay)
        {
            MakeNextPlayerMove();
        }
        else if (CurrentState == PuzzleState.Shuffling)
        {
            if (shuffleMovesRemaining > 0)
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
        shuffleMovesRemaining = shuffleLength;
        emptyBlock.gameObject.SetActive(false);
        MakeNextShuffleMove();
    }

    void MakeNextShuffleMove()
    {
		Vector2[] offsets = { new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1) };
        int randomIndex = Random.Range(0, offsets.Length);

        for (int i = 0; i < offsets.Length; i++)
        {
			Vector2 offset = offsets[(randomIndex + i) % offsets.Length];
            if (offset != prevShuffleOffset * -1)
            {
				Vector2 moveBlockCoord = emptyBlock.Position + offset;

                if (moveBlockCoord.x >= 0 && moveBlockCoord.x < blocksPerLine && moveBlockCoord.y >= 0 && moveBlockCoord.y < blocksPerLine)
                {
                    MoveBlock(Segments[(int)moveBlockCoord.x, (int)moveBlockCoord.y], shuffleMoveDuration);
                    shuffleMovesRemaining--;
                    prevShuffleOffset = offset;
                    break;
                }
            }
        }
      
    }

    void CheckIfSolved()
    {
        foreach (Segment IndividualSegment in Segments)
        {
            if (!IndividualSegment.IsAtStartingCoord())
            {
                return;
            }
        }
        CurrentState = PuzzleState.Solved;
		Debug.Log("PuzzleSolved");
        emptyBlock.gameObject.SetActive(true);
    }
}
