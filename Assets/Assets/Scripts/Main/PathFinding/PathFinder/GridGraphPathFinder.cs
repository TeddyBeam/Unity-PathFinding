using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding.PathFinder
{
    public abstract class GridGraphPathFinder : IPathFinder
    {
        protected GridGraph gridGraph;

        public GridGraphPathFinder(GridGraph gridGraph)
        {
            this.gridGraph = gridGraph;
        }

        public Stack<Node> FindPath(Vector3 startPosition, Vector3 targetPosition)
        {
            Node startNode = gridGraph.GetNodeFromWorldPosition(startPosition);
            Node targetNode = gridGraph.GetNodeFromWorldPosition(targetPosition);

            UpdateGraph(startNode, targetNode);

            return RetracePath(startNode, targetNode);
        }

        protected abstract void UpdateGraph(Node startNode, Node targetNode);

        protected Stack<Node> RetracePath(Node startNode, Node targetNode)
        {
            Stack<Node> path = new Stack<Node>();
            Node currentNode = targetNode;

            while (currentNode != startNode)
            {
                path.Push(currentNode);

                if (currentNode.PreviousNode == null)
                {
                    Debug.LogWarning("Coundn't find a path. RetracePath break when try to find previous node of " + currentNode);
                    return null;
                }

                currentNode = currentNode.PreviousNode;
            }

            return path;
        }
    }
}