using _Code_Bullet;
using _Code_Hub;
using _Code_Pool;
using UnityEngine;

namespace _Code__FireController
{
    public class FireController : MonoBehaviour
    {
        [SerializeField] private Bullet m_bullet = default;

        private void OnEnable()
        {
            Hub.fireDirection += Fire;
        }
        private void OnDisable()
        {
            Hub.fireDirection -= Fire;
        }

        private void Fire(Vector3 firePosition, Vector3 fireDirection)
        {
            m_bullet = Pool.Instance.GetRandomBullet();
            m_bullet.Transform.position = firePosition;
            m_bullet.gameObject.SetActive(true);
            Vector3 direction = firePosition + fireDirection;
            m_bullet.ShootToPosition(direction);
        }
    }
}
