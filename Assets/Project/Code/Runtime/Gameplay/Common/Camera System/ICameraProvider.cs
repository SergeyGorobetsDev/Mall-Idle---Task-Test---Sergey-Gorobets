using UnityEngine;
namespace Assets.Project.Code.Runtime.Gameplay.Common.Camera_System
{
    public interface ICameraProvider
    {
        void SetMainCamera(Camera camera);
        Camera GetMainCamera();
    }
}
