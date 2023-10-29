using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "BG/Inventory")]
public class Inventory : ScriptableObject
{
    public List<Category> categories = new List<Category>();

    // Add functions to manage the inventory, e.g., AddItem, RemoveItem, etc.
}

