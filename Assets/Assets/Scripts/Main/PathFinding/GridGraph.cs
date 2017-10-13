using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extension.ExtraTypes.IntVector2;

namespace PathFinding
{
    public class GridGraph
    {
        private LayerMask unwalkableMask;
        private Vector3 gridWorldPosition;
        private Vector2 gridWorldSize;
        private float nodeRadius;
        private IntVector2 gridSize;

        public float NodeDiameter { get; private set; }
        public Node[,] Grid { get; private set; }
        public int Area { get { return gridSize.X * gridSize.Y; } }

        public GridGraph() : this(0, Vector2.one, Vector3.zero) { }

        public GridGraph(LayerMask unwalkableMask, Vector2 gridWorldSize, Vector3 gridWorldPosition, float nodeRadius = 1f)
        {
            this.unwalkableMask = unwalkableMask;
            this.gridWorldPosition = gridWorldPosition;
            this.gridWorldSize = gridWorldSize;
            this.nodeRadius = nodeRadius;

            NodeDiameter = nodeRadius * 2;
            gridSize = new IntVector2(Mathf.RoundToInt(gridWorldSize.x / NodeDiameter), Mathf.RoundToInt(gridWorldSize.y / NodeDiameter));

            InitGrid();
        }

        private void InitGrid ()
        {
            Grid = new Node[gridSize.X, gridSize.Y];
            Vector3 worldBottomLeft = gridWorldPosition - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

            // Initialize all the nodes.
            for (int x = 0; x < gridSize.X; x++)
            {
                for (int y = 0; y < gridSize.Y; y++)
                {
                    Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * NodeDiameter + nodeRadius) + Vector3.forward * (y * NodeDiameter + nodeRadius);
                    bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));

                    Grid[x, y] = new Node(walkable, worldPoint, new IntVector2(x, y));
                }
            }

            // Set up neighbours for all the nodes.
            foreach (Node node in Grid)
            {
                UpdateNeighbours(node);
            }
        }

        /// <summary>
        /// Add all the nodes around that node into the NeighboursNode.
        /// </summary>
        private void UpdateNeighbours (Node node)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x != 0 || y != 0)
                    {
                        int checkX = node.Coordinate.X + x;
                        int checkY = node.Coordinate.Y + y;

                        if (checkX >= 0 && checkX < gridSize.X && checkY >= 0 && checkY < gridSize.Y)
                        {
                            node.NeighbourNodes.Add(Grid[checkX, checkY]);
                        }
                    }
                }
            }
        }

        public Node GetNodeFromWorldPosition(Vector3 worldPosition)
        {
            if (Grid != null)
            {
                float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
                float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
                percentX = Mathf.Clamp01(percentX);
                percentY = Mathf.Clamp01(percentY);

                int x = Mathf.RoundToInt((gridSize.X - 1) * percentX);
                int y = Mathf.RoundToInt((gridSize.Y - 1) * percentY);

                return Grid[x, y];
            }
            else
            {
                Debug.LogError("Null grid!!!");
                return null;
            }
        }

        public int GetDistance(Node nodeA, Node nodeB)
        {
            int distanceX = Mathf.Abs(nodeA.Coordinate.X - nodeB.Coordinate.X);
            int distanceY = Mathf.Abs(nodeA.Coordinate.Y - nodeB.Coordinate.Y);

            // formula for calculating heuristics : h = min(dx, dy) * 14 + abs(dx - dy) * 10
            return (distanceX > distanceY) ? 14 * distanceY + 10 * (distanceX - distanceY) : 14 * distanceX + 10 * (distanceY - distanceX);
        }

    }
}
