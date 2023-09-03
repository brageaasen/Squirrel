using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    // References
    [SerializeField] private GameObject gameOverMenuUI;
    [SerializeField] private Player player;
    
    // Update is called once per frame
    void Update()
    {
        if (player.currentHealth <= 0) // Enable game over screen on player-death
            gameOverMenuUI.SetActive(true);
    }
}