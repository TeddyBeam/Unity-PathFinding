using UnityEngine;
using BaseSystems.DesignPatterns.DependencyInjection;
using PathFinding;

namespace Main.Misc
{
    public class EditorGraphDrawGizmos : MonoBehaviour
    {
        private GridGraph gridGraph;

        // public Vector3 GridWorldPosition;
        // public Vector2 GridWorldSize;

        [Inject]
        public void Construct (GridGraph gridGraph)
        {
            this.gridGraph = gridGraph;
        }

        protected virtual void OnDrawGizmos()
        {
            // Gizmos.DrawWireCube(GridWorldPosition, new Vector3(GridWorldSize.x, 1f, GridWorldSize.y));

            if (gridGraph == null || gridGraph.Grid == null)
                return;

            foreach (Node node in gridGraph.Grid)
            {
                Gizmos.color = (node.Walkable) ? Color.white : Color.red;
                Gizmos.DrawCube(node.Position + Vector3.up, Vector3.one * (gridGraph.NodeDiameter));
            }
        }
    }
}
