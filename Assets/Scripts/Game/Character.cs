using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float _health;
    public float Health => _health;
    
    private float _maxHealth;

    private void Awake()
    {
        _maxHealth = _health;
    }

    public void ApplyDamage(float damage)
    {
        _health -= damage;
    }

    public void Heal(float health)
    {
        _health += health;
        
        if (_health >= _maxHealth)
        {
            _health = _maxHealth;
        }
    }

    protected virtual void Update()
    {
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}