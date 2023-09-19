using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementController : MonoBehaviour, IMovable
{
    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private AudioClip _leftStep;

    private AudioSource _audioSource;
    private bool _isLeftStep = false;
    private bool grounded = true;

    #region IMovable PROPERTIES

    public float MovementSpeed => _movementSpeed;
    [SerializeField] private float _movementSpeed = 15f;

    public void Move(Vector3 direction)
    {
        StartCoroutine(MoveSfxCoroutine());
        float y = _rigidbody.velocity.y;
        Vector3 aux = transform.TransformDirection(direction * MovementSpeed * Time.deltaTime * 120);
        aux.y = y;
        _rigidbody.velocity = Vector3.ClampMagnitude(aux, _movementSpeed);
    }

    public void Jump(Vector3 force)
    {
        if (!grounded) return;
        grounded = false;
        _rigidbody.AddRelativeForce(new Vector3(0, 3f, 0), ForceMode.Impulse);
    }

    public void Land()
    {
        if (!grounded)
        {
            grounded = true;
        }
    }

    #endregion

    #region UNITY EVENTS

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    #endregion

    #region COROUTINES

    IEnumerator MoveSfxCoroutine()
    {
        if (!_audioSource.isPlaying)
        {
            if (_isLeftStep)
            {
                _audioSource.Play();
                _isLeftStep = false;
            }
            else
            {
                _audioSource.PlayOneShot(_leftStep);
                _isLeftStep = true;
            }
            yield return new WaitForSeconds(0.7f);
        }
    }

    #endregion
}