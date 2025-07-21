using UniRx;
using UnityEngine;

public class Hero : Character
{
    [SerializeField] private Ability[] _abilities = new Ability[5];

    private void Start()
    {
        Observable.EveryUpdate()
            .Subscribe(_ =>
            {
                for (int i = 0; i < _abilities.Length; i++)
                {
                    if (Input.GetKeyDown(_abilities[i].AbilityKey) && _abilities[i])
                    {
                        _abilities[i].Execute(gameObject);
                    }
                }
            })
            .AddTo(this);
    }
}