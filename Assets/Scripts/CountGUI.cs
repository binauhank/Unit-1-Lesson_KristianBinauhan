using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;

public class CountGUI : NetworkBehaviour
{
    private TextMeshProUGUI tmProElement;
    public string itemName;
    public NetworkVariable<int> count = new NetworkVariable<int>(0);

    void Start()
    {
        tmProElement = GetComponent<TextMeshProUGUI>();
        count.OnValueChanged += OnCountValueChanged;
    }

    public override void OnNetworkSpawn()
    {
        UpdateText();
    }

    public void UpdateCountBroadcast()
    {
        if (IsServer)
        {
            UpdateCount();
        }
        else
        {
            UpdateCountRpc();
        }
    }

    private void OnCountValueChanged(int previousValue, int newValue)
    {
        UpdateText();
    }

    public void UpdateCount()
    {
        count.Value++;
    }

    [Rpc(SendTo.Server)]
    public void UpdateCountRpc()
    {
        UpdateCount();
    }

    public void UpdateText()
    {
        tmProElement.text = itemName + ": " + count.Value;
    }
}
