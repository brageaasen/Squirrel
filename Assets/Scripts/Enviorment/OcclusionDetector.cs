using Pathfinding;
using UnityEngine;

public class OcclusionDetector : MonoBehaviour
{
    // References
    private AudioManager audioManager;
    private GameObject player;
    private PlayerMovement playerMovement;
    private GameObject fox;
    private GameObject owl;
    private EnemyAI foxAI;
    private AIPath owlAI;

    // Fields
    public ParticleSystem bushPS;

    void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        player = GameObject.FindWithTag("Player");
        fox = GameObject.Find("Fox");
        owl = GameObject.Find("Owl");
        if (player != null)
            playerMovement = player.GetComponent<PlayerMovement>();
        if (fox != null)
            foxAI = fox.GetComponent<EnemyAI>();
        if (owl != null)
            owlAI = owl.GetComponent<AIPath>();
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Player")
        {
            playerMovement.behindForeGround = true;
            if (playerMovement.crouch) // Stealth player if crouching
            {
                audioManager.Play("Bush");
                CreateBushPS();

                // Disable enemy AI agents
                if (foxAI != null)
                    foxAI.enabled = false;
                if (owlAI != null)
                    owlAI.enabled = false;
                fox.GetComponentInChildren<MeleeEnemy>().enabled = false;

                // Stealth player
                playerMovement.Stealth();
                MakeSpritesTransparent();
            }
        }
    }

    void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Player")
        {
            if (playerMovement.crouch) // Stealth player if crouching
            {
                CreateBushPS();

                // Disable enemy AI agents
                if (foxAI != null)
                    foxAI.enabled = false;
                if (owlAI != null)
                    owlAI.enabled = false;
                fox.GetComponentInChildren<MeleeEnemy>().enabled = false;

                // Stealth player
                playerMovement.Stealth();
                MakeSpritesTransparent();
            }
            else if (!playerMovement.crouch) // Unstealth player if stopped crouching
            {
                MakeSpritesSolid();
                playerMovement.UnStealth();
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Player")
        {
            playerMovement.behindForeGround = false;

            // Enable enemy AI agents
            if (foxAI != null)
                foxAI.enabled = true;
            if (owlAI != null)
                owlAI.enabled = true;
            fox.GetComponentInChildren<MeleeEnemy>().enabled = true;
            owl.GetComponentInChildren<MeleeEnemy>().enabled = true;

            // Unstealth player
            playerMovement.UnStealth();
            MakeSpritesSolid();
        }
    }

    void MakeSpritesTransparent()
    {
        SpriteRenderer spriteRendererObject = gameObject.GetComponent<SpriteRenderer>();
        SpriteRenderer spriteRendererPlayer = player.GetComponent<SpriteRenderer>();
        if (spriteRendererObject != null)
            spriteRendererObject.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        if (spriteRendererPlayer != null)
            spriteRendererPlayer.color = new Color(1.0f, 1.0f, 1.0f, 0.75f);
    }

    void MakeSpritesSolid()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        SpriteRenderer spriteRendererPlayer = player.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        if (spriteRendererPlayer != null)
            spriteRendererPlayer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    void CreateBushPS()
	{
		bushPS.Play();
	}
}