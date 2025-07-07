using UnityEngine;

public abstract class Ability : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private AbilityBehaviour _behaviour;
    [SerializeField] private Stat _stats;
    [SerializeField] private KeyCode _key;

    public string Id => _id;
    public AbilityBehaviour Behaviour => _behaviour;
    public float Cooldown => _stats.Cooldown;
    public KeyCode InputKey => _key;
}