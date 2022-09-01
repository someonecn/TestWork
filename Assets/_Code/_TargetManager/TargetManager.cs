using _Code_Figures;
using _Code_Hub;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Code_TargetManager
{
    public class TargetManager : MonoBehaviour
    {
        [SerializeField] private List<DestructableFigure> m_destructable = new List<DestructableFigure>();
        [Range(10f, 20f)]
        [SerializeField] private float m_appearMinRadius = 10f;
        [Range(21f, 100f)]
        [SerializeField] private float m_appearMaxRadius = 40f;
        [SerializeField] private LayerMask m_primitivesLayer = default;
        #region [Privates]
        private List<DestructableFigure> m_destructableFigures = new List<DestructableFigure>();
        private Vector3 m_position = Vector3.zero;
        #endregion
        private void Awake()
        {
            m_position = Vector3.zero + Vector3.up * 0.5f;

            foreach (var item in m_destructable)
            {
                while (true)
                {
                    Vector3 rand = UnityEngine.Random.insideUnitSphere * m_appearMaxRadius;

                    if (Mathf.Abs(rand.x) > m_appearMinRadius || Mathf.Abs(rand.z) > m_appearMinRadius)
                    {
                        m_position.x = rand.x;
                        m_position.z = rand.z;
                        RaycastHit[] raycastHit = new RaycastHit[2];
                        Physics.BoxCastNonAlloc(m_position + Vector3.up * 5f, Vector3.one * 3f, Vector3.down, raycastHit, Quaternion.identity, 10f, m_primitivesLayer);
                        bool finded = true;
                        for (int i = 0; i < raycastHit.Length; i++)
                        {
                            if (raycastHit[i].collider != null)
                            {
                                finded = false;
                                break;
                            }
                        }
                        if (finded)
                        {

                            DestructableFigure destructableFigure = Instantiate(item, transform);
                            destructableFigure.transform.position = m_position;
                            m_destructableFigures.Add(destructableFigure);
                            break;
                        }
                    }
                }

            }
        }
        private void OnEnable()
        {
            Hub.replaceFigure += ReplaceFigure;
        }
        private void OnDisable()
        {
            Hub.replaceFigure -= ReplaceFigure;
        }
        private void ReplaceFigure(DestructableFigure obj)
        {
            Debug.Log("Replace");
            m_position = Vector3.zero + Vector3.up * 0.5f;

            foreach (var item in m_destructableFigures)
            {
                if (obj == item)
                {
                    while (true)
                    {
                        Vector3 rand = UnityEngine.Random.insideUnitSphere * m_appearMaxRadius;

                        if (Mathf.Abs(rand.x) > m_appearMinRadius || Mathf.Abs(rand.z) > m_appearMinRadius)
                        {
                            m_position.x = rand.x;
                            m_position.z = rand.z;

                            RaycastHit[] raycastHit = new RaycastHit[2];
                            Physics.BoxCastNonAlloc(m_position + Vector3.up * 5f, Vector3.one * 3f, Vector3.down, raycastHit, Quaternion.identity, 10f, m_primitivesLayer);
                            bool finded = true;
                            for (int i = 0; i < raycastHit.Length; i++)
                            {
                                if (raycastHit[i].collider != null)
                                {
                                    finded = false;
                                    break;
                                }
                            }
                            if (finded)
                            {
                                obj.transform.position = m_position;
                                obj.EnablePrimitives();
                                break;
                            }
                        }
                    }
                }
            }
        }
    }


}
