using UnityEngine;
using UnityEngine.AI;

public class Enemy : LifeController
{
    public float Damage => _damage;
    [SerializeField] private int _damage = 10;

    public float AttackCooldown => _attackCooldown;
    [SerializeField] private float _attackCooldown = 1f;
    private float _currentAttackCooldown = 0;
    private bool hit = false;
    private bool isDead = false;

    public NavMeshAgent agent;
    public GameObject agent2;

    private Animator animator;
    private AudioSource _audioSource;
    [SerializeField] AudioClip _dyingSound;
    [SerializeField] AudioClip _hitSound;

    public new void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent2 == null || isDead) return;
        agent.SetDestination(agent2.transform.position);
        if (_currentAttackCooldown >= 0)
        {
            _currentAttackCooldown -= Time.deltaTime;
        }
    }

    public void SetAgent2(GameObject agent2)
    {
        this.agent2 = agent2;
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
