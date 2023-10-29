using UnityEngine;

[CreateAssetMenu(menuName = "InvaderMovementData")]
public class InvaderMovementData : ScriptableObject
{
    [field : SerializeField] public float OriginalSpeed {get; private set;} = 0.25f;
    [field : SerializeField] public float SpeedChangeModifier {get; private set;} = 1.1f;
    [field : SerializeField] public float YPositionChange {get; private set;} = 0.25f;
    [field : SerializeField] public int NumberOfFramesToUpdatePosition {get; private set;} = 10;
}