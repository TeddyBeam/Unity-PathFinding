using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFinding;
using PathFinding.PathFinder;
using Extension.Attributes;
using System.Diagnostics;

namespace MainGame
{
    public class MapCreator : MonoBehaviour
    {
        [SerializeField]
        private Transform seeker, target;

        [SerializeField]
        private LayerMask unwalkableMask;

        [SerializeField]
        private Vector3 gridWorldPosition;

        [SerializeField]
        private Vector2 gridWorldSize;

        [SerializeField, Positive]
        private float nodeRadius;

        [SerializeField]
        private bool displayGizmos;

        private Stack<Node> currentPath;

        private GridGraph graph;
        private IPathFinder astarPathFinder;
        private IPathFinder dijkstraPathFinder;

        protected virtual void Awake ()
        {
            graph = new GridGraph(unwalkableMask, gridWorldSize, gridWorldPosition, nodeRadius);
            astarPathFinder = new Astar(graph);
            dijkstraPathFinder = new Dijkstra(graph);
        }

        protected virtual void OnDrawGizmos ()
        {
            if (!displayGizmos)
                return;

            Gizmos.DrawWireCube(gridWorldPosition, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

            if (graph != null)
            {
                if (graph.Grid != null)
                {
                    foreach (Node node in graph.Grid)
                    {
                        Gizmos.color = (node.Walkable) ? Color.white : Color.red;
                        Gizmos.DrawCube(node.Position, Vector3.one * (graph.NodeDiameter - 0.1f));
                    }
                }
            }

            if (currentPath != null)
            {
                foreach (Node node in currentPath)
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawSphere(node.Position, 0.05f);
                }
            }
        }

        [InspectorButton]
        private void TestAstarPathFinder ()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            currentPath = astarPathFinder.FindPath(seeker.position, target.position);

            stopwatch.Stop();
            UnityEngine.Debug.Log("Found the path in " + stopwatch.ElapsedMilliseconds + " ms.");
        }

        [InspectorButton]
        private void TestDijkstraPathFinder()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            currentPath = dijkstraPathFinder.FindPath(seeker.position, target.position);

            stopwatch.Stop();
            UnityEngine.Debug.Log("Found the path in " + stopwatch.ElapsedMilliseconds + " ms.");
        }
    }
}
