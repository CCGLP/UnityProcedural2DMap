using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ccglp.Procedural
{
    public class ProceduralMap : MonoBehaviour
    {

        public enum SquareValue
        {
            COLLISION,
            FLOOR
        }



        private SquareValue[,] map;
        private GameObject father;
        private List<Line> lines;
        private float nodeSize;


        [SerializeField]
        private PositivePoint worldMapSize;

        [SerializeField]
        private MapSpecifications properties;

        [Header("Floor, can be null")]
        [SerializeField]
        private GameObject floorPrefab;

        [SerializeField]
        private GameObject prefabColision;



        public void Awake()
        {
            InitiliazeAndGenerateMap();
        }

        public void RemoveMap()
        {
            GameObject mapObject = GameObject.Find("map");
            if (mapObject != null)
                DestroyImmediate(mapObject);
        }

        public void InitiliazeAndGenerateMap()
        {
            InitializeFather();
            InitializeMap();
            GenerateMap();
            DrawMap();
        }

        private void GenerateMap()
        {
            GenerateLines();
            GenerateBranches();
            GenerateLimits();
        }

        private void InitializeMap()
        {
            nodeSize = prefabColision.transform.localScale.x;
            map = new SquareValue[worldMapSize.x, worldMapSize.y];
            lines = new List<Line>();
        }

        private void InitializeFather()
        {
            father = new GameObject("map");
            father.transform.position = new Vector3(0, 0, 0);
        }


        private void GenerateLines()
        {

            for (int iterations = 1; iterations < properties.NumberOfLines; iterations++)
            {
                GenerateRandomLine();
            }
            GenerateLastLine();
        }

        private void GenerateLastLine()
        {
            int horizontalCount = 0, verticalCount = 0;
            for (int i = 0; i < lines.Count; i++)
            {
                switch (lines[i].Type)
                {
                    case Line.LineType.HORIZONTAL:
                        horizontalCount++;
                        break;
                    case Line.LineType.VERTICAL:
                        verticalCount++;
                        break;
                    default:
                        break;
                }
            }

            GenerateLineWhenCountNeeds(horizontalCount, verticalCount);
        }

        private void GenerateLineWhenCountNeeds(int horizontalCount, int verticalCount)
        {

            if ((horizontalCount == 0 && verticalCount == 0) || (horizontalCount != 0 && verticalCount != 0))
            {
                GenerateRandomLine();
            }
            else if (horizontalCount == 0)
            {
                GenerateLineHorizontal();
            }
            else if (verticalCount == 0)
            {
                GenerateLineVertical();
            }
        }

        private void GenerateRandomLine()
        {
            if (Random.Range(0, 100) <= 50)
            {
                GenerateLineVertical();

            }
            else
            {
                GenerateLineHorizontal();
            }
        }

        private void GenerateLineVertical()
        {
            uint xPoint = (uint)Random.Range(0, (int)worldMapSize.x);
            lines.Add(new Line(new PositivePoint(xPoint, 0), Line.LineType.VERTICAL, nodeSize, worldMapSize));
            for (int j = 0; j < worldMapSize.y; j++)
            {
                map[xPoint, j] = SquareValue.FLOOR;
            }
        }

        private void GenerateLineHorizontal()
        {

            uint yPoint = (uint)Random.Range(0, (int)worldMapSize.y);
            lines.Add(new Line(new PositivePoint(0, yPoint), Line.LineType.HORIZONTAL, nodeSize, worldMapSize));
            for (int i = 0; i < worldMapSize.x; i++)
            {
                map[i, yPoint] = SquareValue.FLOOR;
            }

        }


        private void GenerateLimits()
        {
            GenerateHorizontalLimits();
            GenerateVerticalLimits();
        }


        private void GenerateHorizontalLimits()
        {
            for (int i = -1; i <= worldMapSize.x; i++)
            {
                ((GameObject)Instantiate(prefabColision, GetWorldPoint(i, -1), Quaternion.identity)).transform.SetParent(father.transform);
                ((GameObject)Instantiate(prefabColision, GetWorldPoint(i, (int)worldMapSize.y), Quaternion.identity)).transform.SetParent(father.transform);
            }
        }

        private void GenerateVerticalLimits()
        {
            for (int j = -1; j <= worldMapSize.y; j++)
            {
                ((GameObject)Instantiate(prefabColision, GetWorldPoint(-1, j), Quaternion.identity)).transform.SetParent(father.transform);
                ((GameObject)Instantiate(prefabColision, GetWorldPoint((int)worldMapSize.x, j), Quaternion.identity)).transform.SetParent(father.transform);
            }
        }


        public Vector2 GetWorldPoint(int x, int y)
        {
            Vector2 aux = new Vector2();

            aux.x = x < map.GetLength(0) * 0.5f ? (x - map.GetLength(0) * 0.5f) * (nodeSize) : (x - map.GetLength(0) * 0.5f) * nodeSize;
            aux.y = y < map.GetLength(1) * 0.5f ? (y - map.GetLength(1) * 0.5f) * (-nodeSize) : -(y - map.GetLength(1) * 0.5f) * nodeSize;
            aux.x += nodeSize * 0.5f;
            aux.y -= nodeSize * 0.5f;
            return aux;
        }



        private void GenerateBranches()
        {

            for (int i = 0; i < properties.BranchQuantity; i++)
            {
                int aux = Random.Range(0, lines.Count);
                GenerateBranch(lines[aux]);
            }

        }


        private void GenerateBranch(Line line)
        {

            if (Random.Range(0, 100) <= 50)
            {
                GenerateBranchUpRight(line);
            }
            else
            {
                GenerateBranchDownLeft(line);
            }
        }

        private void GenerateBranchUpRight(Line line)
        {
            int branchIterations = 0;
            PositivePoint pos = GetBranchStartPoint(line);
            while (branchIterations < properties.BranchLongitude && pos.y < worldMapSize.y - 1 && pos.x < worldMapSize.x - 1)
            {
                branchIterations++;
                if (Random.Range(0, 100) <= 50)
                {
                    pos.y++;
                }
                else
                {
                    pos.x++;
                }
                map[pos.x, pos.y] = SquareValue.FLOOR;

            }
        }

        private void GenerateBranchDownLeft(Line line)
        {
            int branchIterations = 0;
            PositivePoint pos = GetBranchStartPoint(line);

            while (branchIterations < properties.BranchLongitude && pos.y > 0 && pos.x > 0)
            {
                branchIterations++;
                if (Random.Range(0, 100) <= 50)
                {
                    pos.y--;
                }
                else
                {
                    pos.x--;
                }
                map[pos.x, pos.y] = SquareValue.FLOOR;

            }
        }

        private PositivePoint GetBranchStartPoint(Line line)
        {
            PositivePoint result = new PositivePoint(0, 0);
            switch (line.Type)
            {
                case Line.LineType.HORIZONTAL:
                    result.y = line.StartPoint.y;
                    result.x = (uint)Random.Range(0, (int)worldMapSize.x);
                    break;
                case Line.LineType.VERTICAL:
                    result.y = (uint)Random.Range(0, (int)worldMapSize.y);
                    result.x = line.StartPoint.x;
                    break;
                default:
                    break;
            }
            return result;
        }




        void DrawMap()
        {
            Vector2 worldPosition = MapStartWorldPoint;

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    InstantiateMapSquare(worldPosition, map[i, j]);
                    worldPosition.y -= nodeSize;
                }
                worldPosition.y = MapStartWorldPoint.y;
                worldPosition.x += nodeSize;
            }

        }

        private void InstantiateMapSquare(Vector2 worldPosition, SquareValue mapValue)
        {
            GameObject objectToInstantiate = mapValue == SquareValue.COLLISION ? prefabColision : floorPrefab;
            if (objectToInstantiate != null)
                ((GameObject)Instantiate(objectToInstantiate, new Vector3(worldPosition.x, worldPosition.y, 0), Quaternion.identity)).transform.SetParent(father.transform);
        }



        public Vector2 MapStartWorldPoint
        {
            get
            {
                return new Vector2(0 - (worldMapSize.x * nodeSize) * 0.5f + nodeSize * 0.5f, (worldMapSize.y * nodeSize) * 0.5f - nodeSize * 0.5f);
            }
        }


    }


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


    [System.Serializable]
    public class MapSpecifications
    {
        [SerializeField]
        private uint branchQuantity, branchLongitude, numberOfLines;

        public MapSpecifications(uint branchQuantity, uint branchLongitude, uint numberOfLines)
        {
            this.branchLongitude = branchLongitude;
            this.branchQuantity = branchQuantity;
            this.numberOfLines = numberOfLines;
        }

        public uint BranchLongitude
        {
            get
            {
                return branchLongitude;
            }
        }

        public uint BranchQuantity
        {
            get
            {
                return branchQuantity;
            }
        }

        public uint NumberOfLines
        {
            get
            {
                return numberOfLines;
            }
        }
    }

    [System.Serializable]
    public struct PositivePoint
    {
        public uint x;
        public uint y;

        public PositivePoint(uint x, uint y)
        {
            this.x = x;
            this.y = y;
        }
    }
}