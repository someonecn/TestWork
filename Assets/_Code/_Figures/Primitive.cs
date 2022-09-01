using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace _Code_Figures
{
    public class Primitive : MonoBehaviour
    {
        [SerializeField] private Rigidbody m_rigidbody = default;
        [SerializeField] private Collider m_collider = default;
        [SerializeField] private DestructableFigure m_destructableFigure = default;

        #region [Properties]
        public Collider Collider => m_collider;
        public DestructableFigure DestructableFigure => m_destructableFigure;
        public Transform Transform => m_transform;
        #endregion
        #region [Privates]
        private Transform m_transform = default;
        private Vector3 m_startPos = Vector3.zero;
        private Quaternion m_startRot = Quaternion.identity;
        #endregion

        private void OnEnable()
        {
            m_transform = transform;
            m_startPos = m_transform.localPosition;
            m_startRot = m_transform.localRotation;
        }
        internal void ResetToStart()
        {
            m_rigidbody.isKinematic = true;
            m_transform.localPosition = m_startPos;
            m_transform.localRotation = m_startRot;
            EnableObject(false);
        }
        internal void EnableObject(bool flag)
        {
            gameObject.SetActive(flag);
        }
        public void SetDestructableFigure(DestructableFigure destructableFigure)
        {
            m_destructableFigure = destructableFigure;
        }
        internal void TakeImpulse(Vector3 position)
        {
            float power = (m_transform.position - position).magnitude;
            Vector3 impusle = m_transform.position - position;
            m_rigidbody.isKinematic = false;
            m_rigidbody.AddForce(impusle * power, ForceMode.Impulse);
        }

    }
}
