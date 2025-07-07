using System;
using UniRx;
using UnityEngine;
using Zenject;

public class ReactivePlayerMover : IInitializable, IDisposable
{
    private readonly Transform _transform;
    private readonly float _speed;
    private readonly IInputStream _inputStream;
    private IDisposable _subscription;

    public ReactivePlayerMover(Transform transform, float speed, IInputStream inputStream)
    {
        _transform = transform;
        _speed = speed;
        _inputStream = inputStream;
    }

    public void Initialize()
    {
        _subscription = _inputStream.MoveStream
            .Where(input => input.sqrMagnitude > 0.01f)
            .Subscribe(input =>
            {
                Vector3 direction = new Vector3(input.x, 0f, input.y).normalized;
                _transform.Translate(direction * (_speed * Time.deltaTime), Space.World);
            });
    }

    public void Dispose()
    {
        _subscription?.Dispose();
    }
}