using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = nameof(Status), menuName = "Ability System/Consequences/"+nameof(Status))]
public class Status : Consequence, IInitializable, ITickable
{
    [SerializeField] private Stat _tickInterval;
    [SerializeField] private Stat _duration;

    private Dictionary<ParameterId, object> _parameters;
    private GameObject _caller;
    private float _timeSinceLastTick;
    private float _timeAlive;

    public override void Execute(GameObject caller, Dictionary<ParameterId, object> parameters)
    {
        ExecuteEffect(caller, parameters);
    }

    public void Initialize()
    {
        _timeAlive = float.MaxValue;
    }

    public void Tick()
    {
        if (_timeAlive > _duration.Value) return;
        
        _timeAlive += Time.deltaTime;

        if (_timeSinceLastTick >= _tickInterval.Value)
        {
            _timeSinceLastTick = 0f;

            foreach (var consequence in _consequences)
            {
                consequence.Execute(_caller, _parameters);
            }
        }

        _timeSinceLastTick += Time.deltaTime;
    }

    protected override void ExecuteEffect(GameObject caller, Dictionary<ParameterId, object> parameters)
    {
        _parameters = parameters;
        _timeAlive = 0f;
        _caller = caller;

        // foreach (var character in targets)
        // {
        //     var hp = _hpInfluence.Value;
        //     if (hp < 0)
        //         character.ApplyDamage(hp);
        //     else
        //         character.Heal(hp);
        // }
    }
}