using System;
using UnityEngine;

public interface IInputStream
{
    IObservable<Vector2> MoveStream { get; }
}