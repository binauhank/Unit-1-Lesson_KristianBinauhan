using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SpawnManager : NetworkBehaviour
{
    public GameObject[] lilyPads;

    public override void OnNetworkSpawn()
    {
        if (!IsServer)
        {
            return;
        }

        InvokeRepeating("SpawnLilyPad", 2.0f, 2.0f);
    }

    void SpawnLilyPad()
    {
        foreach (GameObject lilyPad in lilyPads)
        {
            NetworkObject lilyPadObject = Instantiate(lilyPad).GetComponent<NetworkObject>();
            lilyPadObject.Spawn();
        }
    }
}
