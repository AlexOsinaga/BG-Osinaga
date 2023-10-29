//**Osinaga Yujra Gaabriel Alex**
/// <summary>
////we recieve prites from our inventory and equip them intto the assasin
/// </summary>

using UnityEngine;
using System.Collections.Generic;

public class AssassinDresser : MonoBehaviour {

    public Inventory inventory;
    
    public SpriteRenderer hoodRenderer;
    public SpriteRenderer maskRenderer;
    public SpriteRenderer shirtRenderer; 
    public SpriteRenderer pantsRenderer;

    void Awake() {

        InitializeFromInventory();
        
        //EquipClothes("Hood", 1);
        //EquipClothes("Mask", 2);
    }
    void OnEnable() 
    {
        InitializeFromInventory();
    }

    public void EquipClothes(string categoryName, int productIndex) {
        
        Category category = FindCategory(categoryName);
        if (category == null) return;
        
        Category.Product product = category.products[productIndex];   
        if (product == null) return;

        SpriteRenderer renderer = GetRenderer(categoryName);
        if (renderer == null) return;
        
        renderer.sprite = product.productImage;
        product.isEquipped = true;
    }

    SpriteRenderer GetRenderer(string category) {
        switch(category) {
        case "Hood": return hoodRenderer;
        case "Mask": return maskRenderer; 
        case "Shirt": return shirtRenderer;
        case "Pants": return pantsRenderer;
        }
        return null;
    }

    
    Category FindCategory(string name) {
        return inventory.categories.Find(c => c.categoryName == name); 
    }

    //equip the isequiped product

    public void InitializeFromInventory() {

        foreach (Category category in inventory.categories) {
            int equippedIndex = -1;
            for (int i = 0; i < category.products.Count; i++)
            {
                if (category.products[i].isEquipped) 
                {
                    equippedIndex = i;
                    break; 
                }
            }

            if (equippedIndex != -1) {
                EquipClothes(category.categoryName, equippedIndex); 
            }
        }

    }
    public void UpdateCharacter() 
    {
    // Loop through each category
    
        foreach (Category category in inventory.categories)
        {
            // Find equipped product
            Category.Product equippedProduct = null;
            for (int i = 0; i < category.products.Count; i++)
            {
            if (category.products[i].isEquipped)
            {
                equippedProduct = category.products[i];
                break; 
            }
            }

            // If found, equip it again
            if(equippedProduct != null)
            {
                Debug.Log("product is quiped and character updated");
                // Get index of equipped product
                int equippedIndex = category.products.IndexOf(equippedProduct);

                // Pass index instead of product to EquipClothes
                EquipClothes(category.categoryName, equippedIndex);
            }
        }
    }

}