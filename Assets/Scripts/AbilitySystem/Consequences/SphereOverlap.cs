using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(SphereOverlap), menuName = "Ability System/Consequences/"+nameof(SphereOverlap))]
public class SphereOverlap : Consequence
{
    [SerializeField] Stat _radius;

    protected override void ExecuteEffect(GameObject caller, Dictionary<ParameterId, object> parameters)
    {
        var center = caller.transform.position;
        var targets = Physics.OverlapSphere(center, _radius.Value, _targetLayer)
            .Select(collider => collider.GetComponent<Character>()).Where(enemy => enemy).ToList();

        parameters[_targetListId] = targets;
    }
}