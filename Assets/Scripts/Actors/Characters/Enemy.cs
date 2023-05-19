using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]
public class Enemy : LifeController
{
    public float Damage => _damage;
    [SerializeField] private int _damage = 10;

    public float AttackCooldown => _attackCooldown;
    [SerializeField] private float _attackCooldown = 1f;
    private float _currentAttackCooldown = 0;
    private bool hit = false;
    private bool isDead = false;

    [SerializeField] private NavMeshAgent agent;
    private GameObject _mainCharacter;

    private Animator animator;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _dyingSound;
    [SerializeField] private AudioClip _hitSound;

    public new void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
        _mainCharacter = GameObject.FindWithTag("MainCharacter");
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (_mainCharacter == null || isDead) return;
        agent.SetDestination(_mainCharacter.transform.position);
        if (_currentAttackCooldown >= 0)
        {
            _currentAttackCooldown -= Time.deltaTime;
        }
    }

    public void BoostStats(float statsBoost) {
        _damage = (int) (_damage * statsBoost);
        UpdateMaxLife((int) (MaxLife * statsBoost));
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (hit) return;
        hit = true;
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        Attack(damageable);
        hit=false;
    }

    public void OnCollisionExit(Collision collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        animator.SetBool("attack", false);
    }

    public override void Die() 
    {
        Destroy(this.gameObject, 2f);
        _audioSource.PlayOneShot(_dyingSound);
        animator.applyRootMotion = true;
        animator.SetBool("death", true);
        isDead = true;
        foreach(Collider m_Collider in gameObject.GetComponentsInChildren<Collider>())
        {
            m_Collider.enabled = false;
        } 
        agent.enabled = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        EventManager.instance.ActionNewKill();
    }


    private void Attack(IDamageable damageable)
    {
        if (_currentAttackCooldown <= 0 && damageable != null)
        {
            damageable.TakeDamage(_damage);
            animator.SetBool("attack", true);
            _audioSource.PlayOneShot(_hitSound);
            _currentAttackCooldown = _attackCooldown;
        }
    }
}
