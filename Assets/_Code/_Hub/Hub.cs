using _Code_Bullet;
using _Code_Figures;
using System;
using UnityEngine;

namespace _Code_Hub
{
    public static class Hub
    {
        #region [System.Updates]
        public static Action<float> onSystemUpdate = null;
        public static Action<float> onSystemFixedUpdate = null;
        public static Action<float> onSystemLateUpdate = null;
        #endregion

        #region [joystic]
        public static Action<Vector2, float> joysticDirection = null;
        #endregion

        #region [ShootActions]
        public static Action fire = null;
        public static Action<Vector3, Vector3> fireDirection = null;
        public static Action<Bullet> returnBulletToPool = null;
        public static Action<DestructableFigure> replaceFigure = null;
        #endregion

    }
}
