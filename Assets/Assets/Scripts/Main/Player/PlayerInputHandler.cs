using System;
using System.Linq;
using UnityEngine;
using BaseSystems.DesignPatterns.DependencyInjection;
using BaseSystems.DesignPatterns.Observer;

namespace Main.Player
{
    public class PlayerInputHandler: ITickable
    {
        [Serializable]
        public class Setting
        {
            public Camera mainCamera;
            public float rayCastMaxDistance;
            public LayerMask inputLayers;
        }

        private Camera mainCamera;
        private float rayCastMaxDistance;
        private LayerMask inputLayers;

        public PlayerInputHandler (Setting setting)
        {
            mainCamera = setting.mainCamera;
            rayCastMaxDistance = setting.rayCastMaxDistance;
            inputLayers = setting.inputLayers;
        }

        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray.origin, ray.direction, out hit, rayCastMaxDistance, inputLayers))
                {
                    this.PostEvent(ObserverEventID.OnPlayerClickDetected, hit.point);
                }
            }
        }
    }
}
