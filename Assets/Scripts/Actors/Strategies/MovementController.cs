using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementController : MonoBehaviour, IMovable
{
    public NavMeshAgent Agent => _agent;
    [SerializeField] private NavMeshAgent _agent;

    [SerializeField] private AudioClip _leftStep;

    private AudioSource _audioSource;
    private bool        _isLeftStep = false;

    #region IMovable PROPERTIES

    public float MovementSpeed => _movementSpeed;
    [SerializeField] private float _movementSpeed = 1f;

    public void Move(Vector3 direction)
    {
        StartCoroutine(MoveSfxCoroutine());
        if (_agent.CalculatePath(transform.position + direction * MovementSpeed * Time.deltaTime, new UnityEngine.AI.NavMeshPath()))
        {
            transform.Translate(direction * MovementSpeed * Time.deltaTime);
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
            if (_isLeftStep) {
                _audioSource.Play();
                _isLeftStep = false;
            } else {
                _audioSource.PlayOneShot(_leftStep);
                _isLeftStep = true;
            }
            yield return new WaitForSeconds(0.7f);
        }
    }

    #endregion
}
