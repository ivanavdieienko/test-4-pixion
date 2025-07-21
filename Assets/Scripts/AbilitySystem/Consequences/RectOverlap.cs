using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(RectOverlap), menuName = "Ability System/Consequences/"+nameof(RectOverlap))]
public class RectOverlap : Consequence
{
    [SerializeField] private Stat _width;
    [SerializeField] private Stat _height;
    [SerializeField] private Stat _depth;

    protected override void ExecuteEffect(GameObject caller, Dictionary<ParameterId, object> parameters)
    {
        var halfExtents = new Vector3(_width.Value / 2, _height.Value / 2, _depth.Value / 2);
        var targets = Physics.OverlapBox(caller.transform.position, halfExtents, caller.transform.rotation, _targetLayer)
            .Select(collider => collider.GetComponent<Character>())
            .Where(enemy => enemy).ToList();

        parameters[_targetListId] = targets;
    }
}