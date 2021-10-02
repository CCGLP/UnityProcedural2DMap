using UnityEngine;

namespace ccglp.Procedural
{
    public readonly struct Line
    {
        public enum LineType
        {
            NULL,
            HORIZONTAL,
            VERTICAL
        }

        private readonly LineType type;
        private readonly float nodeSize;
        private readonly PositivePoint worldSize, startPoint;

        public Line(PositivePoint startPoint, LineType type, float nodeSize, PositivePoint worldSize)
        {
            this.startPoint = startPoint;
            this.type = type;
            this.nodeSize = nodeSize;
            this.worldSize = worldSize;
        }

        public Vector2 GetWorldStartPoint()
            => new Vector2
            {
                x = (startPoint.x - worldSize.x * 0.5f) * nodeSize,
                y = (startPoint.y < worldSize.y * 0.5f)
                    ? +(startPoint.y - worldSize.y * 0.5f) * -nodeSize
                    : -(startPoint.y - worldSize.y * 0.5f) * +nodeSize,
            };

        public LineType Type
            => type;

        public PositivePoint StartPoint
            => startPoint;
    }
}
