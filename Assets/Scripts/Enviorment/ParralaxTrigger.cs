using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralaxTrigger : MonoBehaviour
{
    [SerializeField] private BoxCollider2D col;
    private GameObject[] grassBackground;
    private GameObject[] treeBackground;
    private GameObject[] mountainBackground;
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        grassBackground = GameObject.FindGameObjectsWithTag("GrassBG");
        treeBackground =  GameObject.FindGameObjectsWithTag("TreeBG");
        mountainBackground = GameObject.FindGameObjectsWithTag("MountainBG");

        col.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            if (gameObject.name == "MusicTrigger")
            {
                audioManager.Play("Music");
                Destroy(this);
            }
            if (gameObject.name == "GrassTrigger")
            {
                col.enabled = true;
                StopGrassParralax();
            }
            if (gameObject.name == "TreeTrigger")
            {
                col.enabled = true;
                StopTreeParralax();
            }
            if (gameObject.name == "MountainTrigger")
            {
                changeMountainParralax();
            }
        }
    }

    void StopGrassParralax()
    {
        foreach (GameObject background in grassBackground)
        {
            background.GetComponent<Parralax>().continueParralax = false;
        }
    }

    void StopTreeParralax()
    {
        foreach (GameObject background in treeBackground)
        {
            background.GetComponent<Parralax>().continueParralax = false;
        }
    }

    void StopMountainParralax()
    {
        foreach (GameObject background in mountainBackground)
        {
            background.GetComponent<Parralax>().continueParralax = false;
        }
    }

    void changeMountainParralax()
    {
        foreach (GameObject background in mountainBackground)
        {
            if (background.gameObject.name == "BackgroundMountain")
                background.GetComponent<Parralax>().parralaxEffect = 1f;
            else if (background.gameObject.name == "BackgroundMountain2")
                background.GetComponent<Parralax>().continueParralax = false;
        }
    }
}
