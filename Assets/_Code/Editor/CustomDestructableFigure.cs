using UnityEditor;
using UnityEngine;

namespace _Code_Figures
{
    [CustomEditor(typeof(DestructableFigure))]
    public class CustomDestructableFigure : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            DestructableFigure _destructableFigure = (DestructableFigure)target;

            if (GUILayout.Button("Create Figure"))
            {
                _destructableFigure.CreateFigures();
            }
            if (GUILayout.Button("Destroy Figure"))
            {
                _destructableFigure.DestroyFigures();
            }
        }
    }
}
