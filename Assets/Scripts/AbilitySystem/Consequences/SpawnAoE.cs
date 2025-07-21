using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = nameof(SpawnAoE), menuName = "Ability System/Consequences/"+nameof(SpawnAoE))]
public class SpawnAoE : Consequence, ITickable
{
    [SerializeField] private GameObject _aoePrefab;
    [SerializeField] private Stat _radius;
    [SerializeField] private Stat _duration;
    
    private Dictionary<ParameterId, object> _parameters;
    private GameObject _areaOfEffect;
    private float _timeAlive;

    public override void Execute(GameObject caller, Dictionary<ParameterId, object> parameters)
    {
        ExecuteEffect(caller, parameters);
    }

    public void Tick()
    {
        if (!_areaOfEffect) return;

        _timeAlive += Time.deltaTime;

        var colliders = Physics.OverlapSphere(_areaOfEffect.transform.position, _radius.Value, _targetLayer);
        var targets = colliders.Select(c => c.gameObject.GetComponent<Character>()).ToList();

        _parameters[_targetListId] = targets;

        foreach (var consequence in _consequences)
        {
            consequence.Execute(_areaOfEffect, _parameters);
        }

        if (_timeAlive >= _duration.Value)
        {
            Destroy(_areaOfEffect);
            _areaOfEffect = null;
        }
    }

    protected override void ExecuteEffect(GameObject caller, Dictionary<ParameterId, object> parameters)
    {
        _parameters = parameters;
        _timeAlive = 0f;

        _areaOfEffect = Instantiate(_aoePrefab, caller.transform.position, caller.transform.rotation);
        _areaOfEffect.transform.localScale = Vector3.one * (_radius.Value * 2);
    }
}