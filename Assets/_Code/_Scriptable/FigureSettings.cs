using _Code_Enums;
using _Code_Figures;
using UnityEngine;

[CreateAssetMenu(fileName = "Figure Settings", menuName = "ScriptableObjects/Figure Settings", order = 1)]
public class FigureSettings : ScriptableObject
{
    public FigureTypes _figureType = default;
    public FigureComprises _figureComprises = default;
    public int _row = 5;
    public int _column = 5;
    public Primitive _cubePrefab = default;
    public Primitive _spherePrefab = default;
}