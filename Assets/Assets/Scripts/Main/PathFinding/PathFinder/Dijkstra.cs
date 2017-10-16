using System.Collections.Generic;
using UnityEngine;
using Extension.ExtraTypes.Heap;

namespace PathFinding.PathFinder
{
    public class Dijkstra : GridGraphPathFinder
    {
        public Dijkstra(GridGraph gridGraph) : base (gridGraph) { }

        protected override void UpdateGraph(Node startNode, Node targetNode)
        {
            Heap<Node> openNodes = new Heap<Node>(gridGraph.Area);
            HashSet<Node> closedSet = new HashSet<Node>();
            openNodes.Add(startNode);

            int stepCount = 0;

            while (openNodes.Count > 0)
            {
                Node currentNode = openNodes.RemoveFirst();
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                    break;

                foreach (Node neighbour in currentNode.NeighbourNodes)
                {
                    stepCount++;

                    if (!neighbour.Walkable || closedSet.Contains(neighbour))
                        continue;

                    int newMovementCostToNeighbour = currentNode.GCost + gridGraph.GetDistance(currentNode, neighbour) + neighbour.Weight;
                    if (newMovementCostToNeighbour < neighbour.GCost || !openNodes.Contains(neighbour))
                    {
                        neighbour.GCost = newMovementCostToNeighbour;
                        neighbour.PreviousNode = currentNode;

                        if (!openNodes.Contains(neighbour))
                        {
                            openNodes.Add(neighbour);
                        }
                        else
                        {
                            openNodes.UpdateItem(neighbour);
                        }
                    }
                }
            }

            Debug.Log("Dijkstra Updated Graph in " + stepCount + " step.");
        }
    }
}
