using _Code_Hub;
using UnityEngine;

namespace _Code_Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float m_horizontalSpeed = 5f;
        [SerializeField] private float m_verticalSpeed = 5f;
        [SerializeField] private Transform m_firePointTransform = default;

        #region [Privates]
        private Transform m_transform = default;
        private Vector3 m_rotateDirection = Vector3.zero;
        #endregion
        private void OnEnable()
        {
            m_transform = transform;
            m_rotateDirection = transform.rotation.eulerAngles;
            Hub.joysticDirection += RotateCamera;
            Hub.fire += FireDirection;
        }
        private void OnDisable()
        {
            Hub.joysticDirection -= RotateCamera;
            Hub.fire -= FireDirection;
        }

        private void FireDirection()
        {
            Hub.fireDirection?.Invoke(m_firePointTransform.position,m_transform.forward);
        }

        private void RotateCamera(Vector2 joysticDirection, float deltaTime)
        {
            m_rotateDirection += new Vector3(-joysticDirection.y * m_horizontalSpeed * deltaTime, joysticDirection.x * m_verticalSpeed * deltaTime, 0f);
            m_transform.rotation = Quaternion.Euler(m_rotateDirection);
        }
    }
}
