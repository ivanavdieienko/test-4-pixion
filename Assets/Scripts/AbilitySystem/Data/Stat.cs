using UnityEngine;

[CreateAssetMenu(fileName = nameof(Stat), menuName = "Ability System/Data/"+nameof(Stat))]
public class Stat : ScriptableObject
{
    [SerializeField] private float _value;

    public float Value => _value;
}