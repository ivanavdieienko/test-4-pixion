using System.Linq;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _attackRadius = 1f;
    [SerializeField] private float _damageAmount = 1f;
    [SerializeField] private float _attackCooldown = 1f;

    private float _lastAttackTime;
    
    private Transform _targetToFollow;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        if (_targetLayer == default)
            _targetLayer = LayerMask.GetMask("Player");
        
        _targetToFollow = GameObject.FindWithTag("Player").transform;
        _rigidbody = GetComponent<Rigidbody>();
    }

    protected override void Update()
    {
        base.Update();
        
        Move();
        Attack();
    }

    private void Attack()
    {
        if (Time.time < _lastAttackTime + _attackCooldown) return;
        
        _lastAttackTime = Time.time;
        
        var targets = Physics.OverlapSphere(transform.position, _attackRadius, _targetLayer)
            .Select(collider => collider.GetComponent<Character>()).Where(character => character).ToList();

        foreach (var character in targets)
        {
            character.ApplyDamage(_damageAmount);
        }
    }

    private void Move()
    {
        if (!_targetToFollow) return;

        Vector3 direction = (_targetToFollow.position - _rigidbody.position).normalized;
        _rigidbody.MovePosition(_rigidbody.position + direction * (_moveSpeed * Time.deltaTime));

    }
}