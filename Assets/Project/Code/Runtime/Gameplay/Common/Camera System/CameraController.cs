using UnityEngine;
namespace Assets.Project.Code.Runtime.Gameplay.Common.Camera_System
{
    public sealed class CameraController : MonoBehaviour
    {
        private const int DefaultCameraFov = 60;

        [SerializeField, Min(0)]
        public float sensitivity = 100.0f;
        [SerializeField]
        private float moveSpeed;
        [SerializeField]
        private float rotateSpeed;
        [SerializeField]
        private float zoomSpeed;
        [SerializeField, Min(0)]
        private float minFov = 45;
        [SerializeField, Min(0)]
        private float maxFov = 75;
        [SerializeField, Min(10)]
        private float maxDistance = 30f;

        private Vector3 basePosition;

        private float horizontalRotation = 0.0f;

        private Camera rootCamera;

        private void Awake() =>
            rootCamera = GetComponent<Camera>();

        private void Start()
        {
            rootCamera.fieldOfView = DefaultCameraFov;
            Vector3 rot = transform.localRotation.eulerAngles;
            horizontalRotation = rot.y;
        }

        private void FixedUpdate()
        {
            if (Input.GetMouseButton(1))
                Rotate();

            if (Input.GetKey(KeyCode.A) ||
                Input.GetKey(KeyCode.D) ||
                Input.GetKey(KeyCode.W) ||
                Input.GetKey(KeyCode.S))
                Move();

            Zoom();
        }

        private void Zoom()
        {
            float fov = rootCamera.fieldOfView;
            fov -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime;
            fov = Mathf.Clamp(fov, minFov, maxFov);
            Camera.main.fieldOfView = fov;
        }

        private void Rotate()
        {
            float mouseX = Input.GetAxis("Mouse X");
            horizontalRotation += mouseX * sensitivity * Time.deltaTime;
            Quaternion localRotation = Quaternion.Euler(55f, horizontalRotation, 0.0f);
            transform.rotation = localRotation;
        }

        private void Move()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 forward = transform.forward;
            forward.y = 0;
            forward.Normalize();

            Vector3 right = transform.right;
            right.y = 0;
            right.Normalize();

            Vector3 direction = (right * horizontal + forward * vertical).normalized;

            Vector3 pos = transform.position + moveSpeed * Time.deltaTime * direction;

            pos.x = Mathf.Clamp(pos.x, -maxDistance, maxDistance);
            pos.y = Mathf.Clamp(pos.y, 0, 50);
            pos.z = Mathf.Clamp(pos.z, -maxDistance, maxDistance);

            rootCamera.transform.position = pos;
        }
    }
}