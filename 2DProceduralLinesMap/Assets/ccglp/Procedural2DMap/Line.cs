using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ccglp.Procedural
{
    public class Line
    {
        public enum LineType
        {
            NULL,
            HORIZONTAL,
            VERTICAL
        }

        private LineType type;
        private float nodeSize;
        private PositivePoint worldSize, startPoint;


        public Line(PositivePoint startPoint, LineType type, float nodeSize, PositivePoint worldSize)
        {
            this.startPoint = startPoint;
            this.type = type;
            this.nodeSize = nodeSize;
            this.worldSize = worldSize;
        }

        public Vector2 GetWorldStartPoint()
        {
            Vector2 aux = new Vector2();

            aux.x = startPoint.x < (worldSize.x * 0.5f) ? (startPoint.x - (worldSize.x) * 0.5f) * nodeSize : (startPoint.x - (worldSize.x) * 0.5f) * nodeSize;
            aux.y = startPoint.y < (worldSize.y * 0.5f) ? (startPoint.y - (worldSize.y) * 0.5f) * -nodeSize : -(startPoint.y - (worldSize.y) * 0.5f) * nodeSize;
            return aux;


        }



        public LineType Type
        {
            get
            {
                return type;
            }
        }

        public PositivePoint StartPoint
        {
            get
            {
                return startPoint;
            }
        }
    }
}