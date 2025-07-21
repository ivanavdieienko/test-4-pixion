using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(AbilityBehaviour), menuName = "Ability System/Abilities/"+nameof(AbilityBehaviour))]

public class AbilityBehaviour : ScriptableObject
{
    [SerializeField] private List<AbilityPhase> _phases = new();
    
    public void Execute(GameObject caller, Dictionary<ParameterId, object> parameters, Action onComplete, int index = 0)
    {
        if (index >= _phases.Count)
        {
            onComplete?.Invoke();
            return;
        }

        _phases[index].Execute(caller, parameters, () => Execute(caller, parameters, onComplete, index + 1));
    }

    public void Reset()
    {
        _phases.ForEach(phase => phase.Initialize());
    }
}