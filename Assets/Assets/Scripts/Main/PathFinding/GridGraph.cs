using System;
using System.Collections.Generic;
using UnityEngine;
using Extension.ExtraTypes.IntVector2;

namespace PathFinding
{
    public class GridGraph
    {
        [Serializable]
        public class Setting
        {
            public LayerMask unwalkableMask;
            public Vector3 gridWorldPosition;
            public Vector2 gridWorldSize;
            public float nodeRadius;
        }

        private LayerMask unwalkableMask;
        private float nodeRadius;
        private IntVector2 gridSize;

        public Vector3 GridWorldPosition { get; private set; }
        public Vector2 GridWorldSize { get; private set; }
        public float NodeDiameter { get; private set; }
        public Node[,] Grid { get; private set; }
        public int Area { get { return gridSize.X * gridSize.Y; } }

        public GridGraph(Setting setting)
        {
            unwalkableMask = setting.unwalkableMask;
            GridWorldPosition = setting.gridWorldPosition;
            GridWorldSize = setting.gridWorldSize;
            nodeRadius = setting.nodeRadius;

            NodeDiameter = nodeRadius * 2;
            gridSize = new IntVector2(Mathf.RoundToInt(GridWorldSize.x / NodeDiameter), Mathf.RoundToInt(GridWorldSize.y / NodeDiameter));

            InitGrid();
        }

        private void InitGrid ()
        {
            Grid = new Node[gridSize.X, gridSize.Y];
            Vector3 worldBottomLeft = GridWorldPosition - Vector3.right * GridWorldSize.x / 2 - Vector3.forward * GridWorldSize.y / 2;

            // Initialize all the nodes.
            for (int x = 0; x < gridSize.X; x++)
            {
                for (int y = 0; y < gridSize.Y; y++)
                {
                    Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * NodeDiameter + nodeRadius) + Vector3.forward * (y * NodeDiameter + nodeRadius);
                    bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                    Grid[x, y] = new Node(walkable, worldPoint, new IntVector2(x, y), 0);
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
                float percentX = (worldPosition.x + GridWorldSize.x / 2) / GridWorldSize.x;
                float percentY = (worldPosition.z + GridWorldSize.y / 2) / GridWorldSize.y;
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

        /// <summary>
        /// Clear Hcost and Gcost of all the nodes in the map.
        /// Use this before switching to another path finding solution in the same scene.
        /// </summary>
        public void ClearMapData ()
        {
            foreach (Node node in Grid)
            {
                node.HCost = 0;
                node.GCost = 0;
            }
        }
    }
}
