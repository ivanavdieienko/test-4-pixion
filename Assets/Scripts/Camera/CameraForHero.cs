using UnityEngine;
using Zenject;

public class CameraForHero : MonoBehaviour, IInitializable
{
    [Inject] private Hero _hero;

    [SerializeField] private float _cameraSpeed = 5f;
    [SerializeField] private Vector3 _offset = new (-10, 10, -10);

    private Transform _heroTransform;

    public void Initialize()
    {
        Application.targetFrameRate = 60;

        _heroTransform = _hero.transform;
    }
 
    private void LateUpdate()
    {
        if (!_heroTransform) return;

        var desiredPosition = _heroTransform.position + _offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, _cameraSpeed * Time.deltaTime);
        transform.LookAt(_heroTransform);
    }
}