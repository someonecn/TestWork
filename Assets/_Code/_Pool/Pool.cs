using _Code_Bullet;
using _Code_Enums;
using _Code_Hub;
using _Code_Random;
using System.Collections.Generic;
using UnityEngine;

namespace _Code_Pool
{
    [System.Serializable]
    public class BulletPoolItem
    {
        public BulletTypes _bulletType = default;
        public int _amountToPool = 100;
        public Bullet _objectToPool = default;
        public BulletSettings _bulletSettings = default;
    }

    public class Pool : MonoBehaviour
    {
        #region [Singleton]
        public static Pool Instance { get; private set; }
        #endregion
        [SerializeField] private List<BulletPoolItem> m_bulletsToPool = new List<BulletPoolItem>();

        #region [Properties]
        private HashSet<Bullet> m_pooledBullets = new HashSet<Bullet>();
        private Transform m_transform = default;
        #endregion
        private void Awake()
        {
            Instance = this;

            m_transform = transform;

            m_pooledBullets = new HashSet<Bullet>();

            foreach (BulletPoolItem item in m_bulletsToPool)
            {
                for (int i = 0; i < item._amountToPool; i++)
                {
                    m_pooledBullets.Add(InstantiateBullet(item));
                }
            }

           
        }
        private void OnEnable()
        {
            Hub.returnBulletToPool += ReturnBulletToPool;
        }
        private void OnDisable()
        {
            Hub.returnBulletToPool -= ReturnBulletToPool;
        }

        private Bullet InstantiateBullet(BulletPoolItem item)
        {
            Bullet bullet = Instantiate(item._objectToPool, m_transform);
            bullet.SetBulletParametrs(item._bulletType, item._bulletSettings);
            bullet.gameObject.SetActive(false);
            return bullet;
        }

        private Bullet GetBulletByType(BulletTypes bulletType)
        {
            foreach (Bullet bullet in m_pooledBullets)
            {
                if (bullet.BulletType == bulletType)
                {
                    m_pooledBullets.Remove(bullet);
                    return bullet;
                }
            }

            BulletPoolItem bulletPoolItem = GetBulletTypeFromList(bulletType);

            return InstantiateBullet(bulletPoolItem);
        }
        private BulletPoolItem GetBulletTypeFromList(BulletTypes bulletType)
        {
            foreach (BulletPoolItem item in m_bulletsToPool)
            {
                if (item._bulletType == bulletType)
                {
                    return item;
                }
            }

            return null;
        }

        internal Bullet GetRandomBullet()
        {
            BulletTypes value = RandomExtensions.RandomEnumValue<BulletTypes>();
            return GetBulletByType(value);
        }

        private void ReturnBulletToPool(Bullet bullet)
        {
            bullet.gameObject.SetActive(false);

            if (!m_pooledBullets.Contains(bullet))
            {
                m_pooledBullets.Add(bullet);
            }
        }
    }
}
