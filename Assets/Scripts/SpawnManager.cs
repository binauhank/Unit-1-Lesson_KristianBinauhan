using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] lilyPads;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnLilyPad", 2.0f, 2.0f);
    }

    void SpawnLilyPad()
    {
        foreach (GameObject lilyPad in lilyPads)
        {
            Instantiate(lilyPad);
        }
    }
}
