using System.Collections.Generic;
using UnityEngine;

public abstract class Consequence : ScriptableObject
{
    [SerializeField] protected List<Consequence> _consequences = new ();
    [SerializeField] protected LayerMask _targetLayer;
    [SerializeField] protected ParameterId _targetListId;

    private void Awake()
    {
        _targetLayer = LayerMask.GetMask("Enemy");
    }

    public virtual void Execute(GameObject caller, Dictionary<ParameterId, object> parameters)
    {
        ExecuteEffect(caller, parameters);

        foreach (var consequence in _consequences)
        {
            consequence.Execute(caller, parameters);
        }
    }

    protected abstract void ExecuteEffect(GameObject caller, Dictionary<ParameterId, object> parameters);
}