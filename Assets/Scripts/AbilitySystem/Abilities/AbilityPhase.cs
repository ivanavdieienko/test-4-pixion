using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = nameof(AbilityPhase), menuName = "Ability System/Abilities/"+nameof(AbilityPhase))]
public class AbilityPhase : ScriptableObject, ITickable, IInitializable
{
    [SerializeField] private Stat _duration;
    [SerializeField] private TimeConsequencePair[] _consequences;
    
    private Dictionary<float,List<Consequence>> _consequenceDictionary;
    private List<float> _executedConsequences = new();
    
    private float _timer = 0;
    private int _index = -1;
    private GameObject _caller;
    private Dictionary<ParameterId, object> _parameters;
    private Action _onComplete;

    public void Execute(GameObject caller, Dictionary<ParameterId, object> parameters, Action onComplete)
    {
        _timer = 0;
        _caller = caller;
        _parameters = parameters;
        _onComplete = onComplete;
    }

    public void Initialize()
    {
        _consequenceDictionary = _consequences.ToDictionary(x => x.Time, x => x.Consequences);
        _executedConsequences.Clear();

        _timer = _duration.Value;
    }

    public void Tick()
    {
        _timer += Time.deltaTime;

        if (_timer >= _duration.Value) return;
        
        var normalizedTime = _timer / _duration.Value;

        foreach (var pair in _consequenceDictionary)
        {
            if (normalizedTime < pair.Key) continue;

            foreach (var consequence in pair.Value)
            {
                consequence.Execute(_caller, _parameters);
            }

            _executedConsequences.Add(pair.Key);
        }

        foreach (var time in _executedConsequences)
        {
            _consequenceDictionary.Remove(time);
        }

        if (_consequenceDictionary.Count == 0)
        {
            _onComplete?.Invoke();
        }
    }
}