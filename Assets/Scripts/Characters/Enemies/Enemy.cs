using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    // References
    [SerializeField] private Animator animator;
    [SerializeField] private HealthBar healthBar;
    private Rigidbody2D rb;

    // Fields
    public int maxHealth = 100;
    public int currentHealth;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        
        GetComponent<EdgeCollider2D>().enabled = false;

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Animator fields
        if ((rb.velocity.x > 0 || rb.velocity.x < 0) || (rb.velocity.y > 0 || rb.velocity.y < 0) && !animator.GetBool("IsSleeping"))
            animator.SetBool("IsMoving", true);
        else
            animator.SetBool("IsMoving", false);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        // Play hurt animation
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        isDead = true;

        // Die animation
        animator.SetBool("IsDead", true);

        // Disable enemy
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<EdgeCollider2D>().enabled = true;
        Destroy(GetComponent<EnemyAI>());
        Destroy(GetComponent<AIPath>());
        rb.gravityScale = 3;
    }

}
