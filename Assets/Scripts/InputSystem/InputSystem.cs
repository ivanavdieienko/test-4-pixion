using System;
using UniRx;
using UnityEngine;

public class InputSystem : MonoBehaviour, IInputStream
{
    private readonly Subject<Vector2> _moveSubject = new();
    public IObservable<Vector2> MoveStream => _moveSubject;

    private void Update()
    {
        var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _moveSubject.OnNext(input);
    }
}