using UnityEngine;

[CreateAssetMenu(fileName = "Stat", menuName = "Ability System/Stats")]
public class Stat : ScriptableObject
{
    [SerializeField] private float _cooldown;
    [SerializeField] private float _overlapRadius;
    [SerializeField] private float _abilityPhaseDuration;
    public float Cooldown => _cooldown;
    public float OverlapRadius => _overlapRadius;
    public float AbilityPhaseDuration => _abilityPhaseDuration;
}