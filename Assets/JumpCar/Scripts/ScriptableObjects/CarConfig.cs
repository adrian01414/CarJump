using UnityEngine;

[CreateAssetMenu(fileName = "CarConfig", menuName = "Configs/CarConfig")]
public class CarConfig : ScriptableObject
{
    public Color Color = Color.white;
    [Min(0f)] public float Speed = 5f;
    public bool OutOfBounds = false;
    public float RotationSpeed = 15f;
}
