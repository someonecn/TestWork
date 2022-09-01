using _Code_Figures;
using _Code_Hub;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Code_TargetManager
{
    public class TargetManager : MonoBehaviour
    {
        [SerializeField] private List<DestructableFigure> _destructable = new List<DestructableFigure>();
        [Range(5f, 10f)]
        [SerializeField] private float _appearMinRadius = 5f;
        [Range(10f, 50f)]
        [SerializeField] private float _appearMaxRadius = 40f;
        #region [Privates]
        private List<DestructableFigure> _destructableFigures = new List<DestructableFigure>();
        Vector3 position = Vector3.zero;
        #endregion
        private void Awake()
        {
            position = Vector3.zero + Vector3.up * 0.5f;

            foreach (var item in _destructable)
            {
                while (true)
                {
                    Vector3 rand = UnityEngine.Random.insideUnitSphere * _appearMaxRadius;

                    if (Mathf.Abs(rand.x) > _appearMinRadius || Mathf.Abs(rand.z) > _appearMinRadius)
                    {
                        position.x = rand.x;
                        position.z = rand.z;
                        if (Physics.Raycast(position + Vector3.up * 5f, Vector3.down, out RaycastHit raycastHit, 10f))
                        {
                            if (!raycastHit.collider.gameObject.TryGetComponent(out Primitive primitive))
                            {
                                DestructableFigure destructableFigure = Instantiate(item, transform);
                                destructableFigure.transform.position = position;
                                _destructableFigures.Add(destructableFigure);
                                break;
                            }
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
            position = Vector3.zero + Vector3.up * 0.5f;

            foreach (var item in _destructableFigures)
            {
                if (obj == item)
                {
                    while (true)
                    {
                        Vector3 rand = UnityEngine.Random.insideUnitSphere * _appearMaxRadius;

                        if (Mathf.Abs(rand.x) > _appearMinRadius || Mathf.Abs(rand.z) > _appearMinRadius)
                        {
                            position.x = rand.x;
                            position.z = rand.z;
                            if (Physics.Raycast(position + Vector3.up * 5f, Vector3.down, out RaycastHit raycastHit, 10f))
                            {
                                if (!raycastHit.collider.gameObject.TryGetComponent(out Primitive primitive))
                                {
                                    obj.transform.position = position;
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


}
