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
        Debug.Log("SetAgent2");
        Debug.Log(agent2.ToString());
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

    private void Attack(IDamageable damageable)
    {
        Debug.Log(damageable.ToString());
        if (_currentAttackCooldown <= 0)
        {
            damageable?.TakeDamage(_damage);
            _currentAttackCooldown = _attackCooldown;
        }
    }
}
