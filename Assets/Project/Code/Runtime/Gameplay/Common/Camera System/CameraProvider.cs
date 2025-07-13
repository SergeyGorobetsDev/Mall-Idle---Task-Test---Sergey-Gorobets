using System;
using UnityEngine;
namespace Assets.Project.Code.Runtime.Gameplay.Common.Camera_System
{
    [Serializable]
    public sealed class CameraProvider : ICameraProvider
    {
        [SerializeField]
        private Camera mainCamera;

        private CameraController cameraController;

        public void SetMainCamera(Camera camera)
        {
            mainCamera = camera;

            cameraController = camera.TryGetComponent(out CameraController controller)
                            ? controller
                            : mainCamera.gameObject.AddComponent<CameraController>();
        }

        public Camera GetMainCamera() =>
            mainCamera;

        public CameraController GetCameraController() =>
           cameraController;
    }
}
