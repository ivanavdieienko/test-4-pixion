using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityBehaviour", menuName = "Ability System/Ability Behaviour")]
public class AbilityBehaviour : ScriptableObject
{
    [SerializeField] private List<AbilityPhase> phases = new ();
    [SerializeField] private Ability nextAbility;
    
    public List<AbilityPhase> Phases => phases;
    public Ability NextAbility => nextAbility;
}