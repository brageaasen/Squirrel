using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // References
    [SerializeField] private Animator animator;
    [SerializeField] private HealthBar healthBar;
    private PlayerMovement playerMovement;
    private PlayerCombat playerCombat;
    private NutCollecter nuts;

    // Fields
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int nutHealing = 40;
    [HideInInspector] public bool isSleeping = false;
    public bool isDead = false;
    public int currentHealth;
    public bool isUnderwater = false;
    public static Vector2 lastCheckpointPos = new Vector2(-24.87f, -8.65f);
    private Vector3 startPos = new Vector3(-24.87f, -8.65f, 0f);

    void Awake()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = lastCheckpointPos;
        nuts = GetComponent<NutCollecter>();
        playerMovement = GetComponent<PlayerMovement>();
        playerCombat = GetComponent<PlayerCombat>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (transform.position == startPos)
        {
            isSleeping = true;
            animator.SetBool("IsSleeping", isSleeping);
        }

        // Change start-HP
        currentHealth = maxHealth - 40;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
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

        // Disable player movement
        GetComponent<PlayerMovement>().enabled = false;
    }

    public void Eat()
    {
        playerMovement.eating = false;
        nuts.DecrementNuts();
        currentHealth += nutHealing;

        if (currentHealth > maxHealth)
            currentHealth -= (currentHealth - maxHealth);

        healthBar.SetHealth(currentHealth);
        animator.SetBool("IsEating", false);
    }

    public void WakePlayer()
    {
        isSleeping = false;
        animator.SetBool("IsSleeping", isSleeping);
        playerCombat.canAttack = true;
    }
}
