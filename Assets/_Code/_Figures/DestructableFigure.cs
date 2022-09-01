using _Code_Enums;
using _Code_Hub;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _Code_Figures
{
    public class DestructableFigure : MonoBehaviour
    {
        [SerializeField] private FigureSettings m_figureSettings = default;
        [SerializeField] private List<Primitive> m_originalPrimitives = new List<Primitive>();
        [SerializeField] private float m_resetTimer = 5f;

        #region [Privates]
        private bool m_exploded = false;
        private HashSet<Primitive> m_primitives = new HashSet<Primitive>();
        private float m_timer = 0f;
        #endregion


        private void Awake()
        {
            foreach (var item in m_originalPrimitives)
            {
                m_primitives.Add(item);
            }
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
            if (!m_exploded) return;

            m_timer += obj;
            if (m_timer >= m_resetTimer)
            {
                ResetFigure();
            }
        }


        private void ResetFigure()
        {
            foreach (var item in m_primitives)
            {
                item.ResetToStart();
            }
            m_exploded = false;
            m_timer = 0f;
            Hub.replaceFigure?.Invoke(this);
        }
        public void EnablePrimitives()
        {
            foreach (var item in m_primitives)
            {
                item.EnableObject(true);
            }
        }
        public void ExplodeObject(Primitive primitive, float radius)
        {
            if (m_exploded) return;
            m_exploded = true;
            Vector3 position = primitive.Transform.position;
            float sqrRadius = radius * radius;
            foreach (var item in m_primitives)
            {
                if ((item.Transform.position - position).sqrMagnitude < sqrRadius)
                {
                    item.EnableObject(false);
                }
                else
                {
                    item.TakeImpulse(position);
                }
            }
        }
        #region [EDITOR]
#if UNITY_EDITOR
        public void CreateFigures()
        {
            switch (m_figureSettings._figureType)
            {
                case FigureTypes.pyramid:
                    {
                        CreatePyramid();
                    }
                    break;
                case FigureTypes.cube:
                    {
                        CreateCube();
                    }
                    break;
            }
        }

        public void DestroyFigures()
        {
            if (m_originalPrimitives.Count == 0) return;

            for (int i = 0; i < m_originalPrimitives.Count; i++)
            {
                if (m_originalPrimitives[i] != null)
                {
                    DestroyImmediate(m_originalPrimitives[i].gameObject);
                }
            }

            m_originalPrimitives.Clear();
        }

        private void CreatePyramid()
        {
            switch (m_figureSettings._figureComprises)
            {
                case FigureComprises.cube:
                    {
                        BuildPyramid(m_figureSettings._cubePrefab);
                    }
                    break;
                case FigureComprises.sphere:
                    {
                        BuildPyramid(m_figureSettings._spherePrefab);
                    }
                    break;
            }
        }

        private void CreateCube()
        {
            switch (m_figureSettings._figureComprises)
            {
                case FigureComprises.cube:
                    {
                        BuildCube(m_figureSettings._cubePrefab);
                    }
                    break;
                case FigureComprises.sphere:
                    {
                        BuildCube(m_figureSettings._spherePrefab);
                    }
                    break;
            }
        }

        private void BuildCube(Primitive @object)
        {
            Vector3 position = Vector3.zero;

            for (int i = 0; i < m_figureSettings._column; i++)
            {
                for (int j = 0; j < m_figureSettings._row; j++)
                {
                    for (int k = 0; k < m_figureSettings._row; k++)
                    {
                        Primitive go = PrefabUtility.InstantiatePrefab(@object, transform) as Primitive;
                        go.transform.position = position;
                        go.SetDestructableFigure(this);
                        m_originalPrimitives.Add(go);
                        position.x += 1;
                    }
                    position.x = 0;
                    position.z += 1;
                }
                position.x = 0;
                position.z = 0;
                position.y += 1;
            }
        }
        private void BuildPyramid(Primitive @object)
        {
            Vector3 position = Vector3.zero;
            int rows = m_figureSettings._row;
            float x = 0f;
            for (int i = 0; i < m_figureSettings._column; i--)
            {
                for (int j = 0; j < rows; j++)
                {
                    for (int k = 0; k < rows; k++)
                    {
                        Primitive go = PrefabUtility.InstantiatePrefab(@object, transform) as Primitive;
                        go.transform.position = position;
                        go.SetDestructableFigure(this);
                        m_originalPrimitives.Add(go);
                        position.x += 1;
                    }
                    position.x = x;
                    position.z += 1;
                }

                rows -= 1;
                x += 0.5f;
                position.x = position.z = x;
                position.y += 1;
            }
        }
#endif
        #endregion

    }
}
