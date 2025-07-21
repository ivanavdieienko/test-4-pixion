using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(HealHp), menuName = "Ability System/Consequences/"+nameof(HealHp))]
public class HealHp : Consequence
{
    [SerializeField] private Stat _healStat;

    protected override void ExecuteEffect(GameObject caller, Dictionary<ParameterId, object> parameters)
    {
        if (!parameters.TryGetValue(_targetListId, out var targetsRaw) || targetsRaw is not List<Character> targets)
        {
            Debug.LogWarning("HealHp: Target list not found or invalid.");
            return;
        }

        foreach (var character in targets)
        {
            character.Heal(_healStat.Value);
        }
    }
}