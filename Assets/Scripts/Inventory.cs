using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject[] items;

    void Start()
    {
        ItemCollect.ItemCollected += IncrementItem;
    }

    void IncrementItem(Item.VegetableType veggieType)
    {
        CountGUI count = items[(int)veggieType].GetComponent<CountGUI>();
        count.UpdateCountBroadcast();
    }
}
