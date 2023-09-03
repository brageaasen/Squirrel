using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour
{
    // References
    private AudioManager audioManager;
    private GameObject player;
    private grantPlayerAbility grantPlayerAbility;
    [SerializeField] private Enemy enemy;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private int grantedAbility;

    // Fields
    [SerializeField] private string[] dialogue;
    [SerializeField] private string[] dialogue2;
    [SerializeField] private float wordSpeed;
    [SerializeField] private bool playerIsClose;
    private int index = 0;

    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        dialogueText.text = "";
        if (GetComponent<grantPlayerAbility>() != null)
        {
            grantPlayerAbility = GetComponent<grantPlayerAbility>();
        }

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x < transform.position.x)
            transform.localScale = new Vector2(-1f, 1f);
        else
            transform.localScale = new Vector2(1f, 1f);

        if (Input.GetButtonDown("Interact") && playerIsClose)
        {
            if (!dialoguePanel.activeInHierarchy)
            {
                audioManager.Play("Click");
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
            else if (dialogueText.text == dialogue[index])
            {
                audioManager.Play("Click");
                NextLine();
            }
        }

        // Speed up dialogue
        if (Input.GetButtonDown("Interact"))
            wordSpeed /= 4;
        else if (Input.GetButtonUp("Interact"))
            wordSpeed *= 4;

        // Quit dialogue
        if (Input.GetKeyDown(KeyCode.Q) && dialoguePanel.activeInHierarchy)
            RemoveText();

        // Check dialogue1 condition, and change dialogue
        if (enemy.isDead)
        {
            dialogue = dialogue2;
            grantedAbility = 2;
        }
        
    }

    public void RemoveText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            RemoveText();
            if (grantedAbility == 1 && grantPlayerAbility != null)
            {
                grantPlayerAbility.grantClimb();
                GameObject.Find("GrantClimb").GetComponent<TextTimer>().EnableText();
            }
            else if (grantedAbility == 2 && grantPlayerAbility != null)
            {
                grantPlayerAbility.grantCrouch();
                GameObject.Find("GrantCrouch").GetComponent<TextTimer>().EnableText();
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other) // Entering zone for interaction
    {
        if (other.CompareTag("Player"))
            playerIsClose = true;
    }

    private void OnTriggerExit2D(Collider2D other) // Leaving zone for interaction
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            RemoveText();
            index = 0;
        }
    }
}
