using UnityEngine;

[CreateAssetMenu(fileName = "carPhysParams", menuName = "Car Physics Parameters", order = 0)]
public class CarPhysicsParams : ScriptableObject
{
    public float Offset = 10f;
    public float SpeedScale = 200f;
    public float Drag = 3f;
    public float AngularDrag = 1f;
}