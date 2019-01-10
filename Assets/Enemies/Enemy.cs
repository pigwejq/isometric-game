using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour, IDamageable
{

    [SerializeField] float maxHp = 100f;
    [SerializeField] float currentHp = 100f;
    [SerializeField] float attackDistance = 6f;
    [SerializeField] float moveDistance = 2f;
    [SerializeField] float damagePerShot = 9f;
    [SerializeField] GameObject projectileToUse;
    [SerializeField] GameObject projectileSocket;

    AICharacterControl ai;
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;

        ai = GetComponent<AICharacterControl>();
        player = FindObjectOfType<Player>().transform;
    }

    public float healthAsPercentage
    {
        get { return currentHp / maxHp; }
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        MoveToPlayer(distanceToPlayer);
        AttackPlayer(distanceToPlayer);
    }

    private void MoveToPlayer(float distanceToPlayer)
    {
        if (distanceToPlayer <= moveDistance)
        {
            ai.SetTarget(player);
        }
        else
        {
            ai.SetTarget(transform);
        }
    }

    private void AttackPlayer(float distanceToPlayer)
    {
        if (distanceToPlayer <= attackDistance)
        {
            GameObject newProjectile = Instantiate(projectileToUse, projectileSocket.transform.position, Quaternion.identity);
            Projectile projectileComponent = newProjectile.GetComponent<Projectile>();
            projectileComponent.damage = damagePerShot;

            Vector3 unitVectorToPlayer = (player.transform.position - projectileSocket.transform.position).normalized;
            float projectileSpeed = projectileComponent.projectileSpeed;
            newProjectile.GetComponent<Rigidbody>().velocity = unitVectorToPlayer * projectileSpeed;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHp = Mathf.Clamp(currentHp - damage, 0f, maxHp);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, moveDistance);
    }
}
