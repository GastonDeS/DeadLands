using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float Damage => _damage;
    [SerializeField] private int _damage = 10;

    public float AttackCooldown => _attackCooldown;
    [SerializeField] private float _attackCooldown = 1f;
    private float _currentAttackCooldown = 0;
    private bool hit = false;

    public NavMeshAgent agent;
    public GameObject agent2;

    private Animator animator;

    // animator variables

    public void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent2 == null) return;
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

    private void Attack(IDamageable damageable)
    {
        Debug.Log(damageable.ToString());
        if (_currentAttackCooldown <= 0 && damageable != null)
        {
            damageable.TakeDamage(_damage);
            animator.SetBool("attack", true);
            _currentAttackCooldown = _attackCooldown;
        }
    }
}
