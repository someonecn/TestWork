using _Code_Enums;
using _Code_Figures;
using _Code_Hub;
using UnityEngine;

namespace _Code_Bullet
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody = default;
        [SerializeField] private BulletTypes _bulletType = default;

        #region [Properties]
        public BulletTypes BulletType => _bulletType;
        public Transform Transform => _transform;
        #endregion

        #region [Privates]
        private BulletSettings _bulletSettings = default;
        private Transform _transform = default;
        private bool _startTimer = false;
        private float _timer = 0f;
        #endregion

        private void Awake()
        {
            _transform = transform;
        }
        private void OnEnable()
        {
            Hub.onSystemLateUpdate += OnSystemLateUpdate;
        }
        private void OnDisable()
        {
            Hub.onSystemLateUpdate -= OnSystemLateUpdate;
        }

        private void OnSystemLateUpdate(float obj)
        {
            if (!_startTimer) return;

            _timer += obj;
            if (_timer >= _bulletSettings._autoDestroyTimer)
            {
                BackToPool();
            }
        }

        private void SetRigidBodyParametrs()
        {
            _rigidbody.mass = _bulletSettings._mass;
        }

        internal void SetBulletParametrs(BulletTypes bulletType, BulletSettings bulletSetting)
        {
            _bulletType = bulletType;
            _bulletSettings = bulletSetting;
            SetRigidBodyParametrs();
        }

        public void ShootToPosition(Vector3 position)
        {
            _transform.LookAt(position);
            _rigidbody.isKinematic = false;
            _rigidbody.velocity = _transform.forward * _bulletSettings._movingSpeed;
            if (_bulletSettings._needAutoDestoy)
            {
                _startTimer = true;
            }
        }

        private void BackToPool()
        {
            _rigidbody.isKinematic = true;
            _startTimer = false;
            _timer = 0f;
            Hub.returnBulletToPool?.Invoke(this);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Primitive primitive))
            {
                primitive.DestructableFigure.ExplodeObject(primitive, _bulletSettings._explosionRadius);
            }

            BackToPool();
        }
    }
}

