using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NutCollecter : MonoBehaviour
{
    private AudioManager audioManager;
    [SerializeField] private TextMeshProUGUI textNuts;

    public float nutCount = 0f;

    void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Nut")
        {
            audioManager.Play("CollectNut");
            IncrementNuts();
            Destroy(other.gameObject);
        }
    }

    public void IncrementNuts()
    {
        nutCount++;
        textNuts.text = nutCount.ToString();
    }
    public void DecrementNuts()
    {
        if (nutCount != 0)
        {
            nutCount--;
            textNuts.text = nutCount.ToString();
        }
    }
}
