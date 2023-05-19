using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]
public class Enemy : LifeController
{
    public float Damage => _damage;
    [SerializeField] private int _damage = 10;

    public float AttackCooldown => _attackCooldown;
    [SerializeField] private float _attackCooldown = 1f;
    
    private float       _currentAttackCooldown = 0;
    private bool        _hit = false;
    private bool        _isDead = false;
    private GameObject  _mainCharacter;
    private Animator    _animator;
    private AudioSource _audioSource;

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private AudioClip _dyingSound;
    [SerializeField] private AudioClip _hitSound;

    #region UNITY EVENTS

    public new void Start()
    {
        _audioSource   = GetComponent<AudioSource>();
        _animator      = GetComponentInChildren<Animator>();
        _mainCharacter = GameObject.FindWithTag("MainCharacter");

        base.Start();
    }

    void Update()
    {
        if (_mainCharacter == null || _isDead) return;
        agent.SetDestination(_mainCharacter.transform.position);
        if (_currentAttackCooldown >= 0)
        {
            _currentAttackCooldown -= Time.deltaTime;
        }
    }

    #endregion

    #region OVERRIDES

    public override void Die() 
    {
        Destroy(this.gameObject, 2f);
        _audioSource.PlayOneShot(_dyingSound);
        _animator.applyRootMotion = true;
        _animator.SetBool("death", true);
        _isDead = true;

        foreach(Collider m_Collider in gameObject.GetComponentsInChildren<Collider>())
        {
            m_Collider.enabled = false;
        } 
        
        agent.enabled = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        EventManager.instance.ActionNewKill();
    }

    #endregion

    public void BoostStats(float statsBoost) {
        _damage = (int) (_damage * statsBoost);
        UpdateMaxLife((int) (MaxLife * statsBoost));
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (_hit) return;
        _hit = true;
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        Attack(damageable);
        _hit=false;
    }

    public void OnCollisionExit(Collision collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        _animator.SetBool("attack", false);
    }

    private void Attack(IDamageable damageable)
    {
        if (_currentAttackCooldown <= 0 && damageable != null)
        {
            damageable.TakeDamage(_damage);
            _animator.SetBool("attack", true);
            _audioSource.PlayOneShot(_hitSound);
            _currentAttackCooldown = _attackCooldown;
        }
    }
}
