using Assets.Project.Code.Runtime.Architecture.Engine;
using UnityEngine;

namespace Assets.Project.Code.Runtime.Gameplay.Common.Extencions
{
    public class CanvasFaceCamera : MonoBehaviour
    {
        private Camera mainCamera;

        private void Start() =>
            mainCamera = EngineSystem.Instance.CameraProvider.GetMainCamera();

        void LateUpdate()
        {
            if (mainCamera == null)
                return;
            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
                             mainCamera.transform.rotation * Vector3.up);
        }
    }
}