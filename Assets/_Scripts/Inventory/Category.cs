using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Category", menuName = "BG/InventoryCategory")]
public class Category : ScriptableObject
{
    public string categoryName;
    public Sprite categoryImage; // Add a category image
    public List<Product> products = new List<Product>();

    [System.Serializable]
    public class Product
    {
        public string productName;
        public int price;
        public int quantity;
        public Sprite productImage; // Include the product image.
        public bool isEquipped; // Add this variable to track whether the product is equipped.
    }
}
