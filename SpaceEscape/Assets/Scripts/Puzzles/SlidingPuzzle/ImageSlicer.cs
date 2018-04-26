using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ImageSlicer {

    public static Texture2D[,] GetSlices(Texture2D ImageToSlice, int SegmentsPerLine)
    {
        int ImageSize = Mathf.Min(ImageToSlice.width, ImageToSlice.height);
        int SegmentSize = ImageSize / SegmentsPerLine;

        Texture2D[,] Segments = new Texture2D[SegmentsPerLine, SegmentsPerLine];

        for (int y = 0; y < SegmentsPerLine; y++)
        {
            for (int x = 0; x < SegmentsPerLine; x++)
            {
				Texture2D Segment = new Texture2D(SegmentSize, SegmentSize)
				{
					wrapMode = TextureWrapMode.Clamp
				};
				Segment.SetPixels(ImageToSlice.GetPixels(x * SegmentSize, y * SegmentSize, SegmentSize, SegmentSize));
                Segment.Apply();
                Segments[x, y] = Segment;
            }
        }

        return Segments;
    }
}
