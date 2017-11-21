using System;
using System.Collections.Generic;
using UnityEngine;
using PathFinding;
using PathFinding.PathFinder;
using BaseSystems.TimingCoroutine;

namespace Main.Player
{
    public class PlayerWaypointsMovement
    {
        [Serializable]
        public class Setting
        {
            public Rigidbody playerTransfrom;
            public float movingTime;
        }

        private float movingTime;
        private Rigidbody playerRigidbody;
        private IPathFinder pathFinder;

        private bool isMoving;

        public PlayerWaypointsMovement(IPathFinder pathFinder, Setting setting)
        {
            playerRigidbody = setting.playerTransfrom;
            movingTime = setting.movingTime;
            this.pathFinder = pathFinder;
        }

        public void StartMoving(Vector3 target)
        {
            if (isMoving)
                Timing.KillCoroutines("Move");

            Timing.RunCoroutine(Move(target), "Move");
        }

        public IEnumerator<float> Move(Vector3 target)
        {
            Stack<Node> path = pathFinder.FindPath(playerRigidbody.position, target);

            if (path == null)
                yield break;

            isMoving = true;

            Vector3 currentTarget = path.Pop().Position;
            Debug.Log("running to: " + currentTarget);
            while (path.Count > 0)
            {
                if (Vector3.Distance(currentTarget, playerRigidbody.position) <= 0.1f)
                {
                    Debug.Log("running to next target: " + currentTarget);
                    currentTarget = path.Pop().Position;
                }

                yield return Timing.WaitUntilDone(Timing.RunCoroutine(SmoothMoveTo(currentTarget)));

                // yield return Timing.WaitForOneFrame;
            }

            isMoving = false;
        }

        public IEnumerator<float> SmoothMoveTo(Vector3 currentTarget)
        {
            float timeFrac = 0f;
            var startingPosition = playerRigidbody.position;

            while (timeFrac <= movingTime)
            {
                timeFrac += Time.deltaTime;
                playerRigidbody.transform.position = Vector3.Lerp(startingPosition, currentTarget, timeFrac / movingTime);
                yield return Timing.WaitForOneFrame;
            }
        }
    }
}
