using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(DealDamage), menuName = "Ability System/Consequences/"+nameof(DealDamage))]
public class DealDamage : Consequence
{
    [SerializeField] private Stat _damageStat;

    protected override void ExecuteEffect(GameObject caller, Dictionary<ParameterId, object> parameters)
    {
        if (!parameters.TryGetValue(_targetListId, out var targetsRaw) || targetsRaw is not List<Character> targets)
        {
            Debug.LogWarning("DealDamage: Target list not found or invalid.");
            return;
        }

        foreach (var enemy in targets)
        {
            enemy.ApplyDamage(_damageStat.Value);
        }
    }
}