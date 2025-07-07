using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health;
    public float Health => _health;

    private void Update()
    {
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}