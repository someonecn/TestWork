using UnityEngine;

[CreateAssetMenu(fileName = "Bullet Settings", menuName = "ScriptableObjects/Bullet Settings", order = 1)]
public class BulletSettings : ScriptableObject
{
    public float _mass = 1f;
    public float _movingSpeed = 10f;
    public float _explosionRadius = 10f;
    public bool _needAutoDestoy = true;
    public float _autoDestroyTimer = 3f;
}