using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = nameof(Ability), menuName = "Ability System/Abilities/"+nameof(Ability))]
public class Ability : ScriptableObject, IInitializable
{
    [SerializeField] protected AbilityBehaviour _behaviour;
    [SerializeField] protected Stat _cooldownDuration;
    [SerializeField] protected KeyCode _inputKey;
    
    private static bool _isAnyAbilityExecuting = false;
    private float _lastUsedTime = float.NegativeInfinity;
    private GameObject _hero;
    
    public KeyCode AbilityKey => _inputKey;
    
    private bool CanExecute()
    {
        return Time.time >= _lastUsedTime + _cooldownDuration.Value && !_isAnyAbilityExecuting;
    }
    
    public void Initialize()
    {
        _hero ??= GameObject.FindGameObjectWithTag("Player");
        _lastUsedTime = float.NegativeInfinity;
    }
    
    public void Execute(GameObject caller)
    {
        if (CanExecute())
        {
            _behaviour.Reset();
            _lastUsedTime = Time.time;
            _isAnyAbilityExecuting = true;
            _behaviour.Execute(caller, new Dictionary<ParameterId, object>(), () => _isAnyAbilityExecuting = false);
        }
    }
}