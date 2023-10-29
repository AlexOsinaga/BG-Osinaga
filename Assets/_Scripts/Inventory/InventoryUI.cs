//**Osinaga Yujra Gaabriel Alex**
/// <summary>
////probably the most imposrtant for the task
////here we will display on screen a dynamic inventory 
////manage the buy/sell/and equip mechanics 
////after buy/sell the gold in wallet will update
////after  equip the player will update
/// </summary>
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Wallet _wallet; // Reference to the Wallet script.
    [SerializeField] private GameObject _categoryButtonPrefab;
    [SerializeField] private Transform _categoryButtonParent;
    [SerializeField] private GameObject _productButtonPrefab;
    [SerializeField] private Transform _productButtonParent;
    [SerializeField] private TMP_Text _categoryNameText;
    [SerializeField] private TMP_Text _goldBalanceText; // TMP for displaying current gold balance.
    [SerializeField] private Button _buyButton; // Reference to the Buy button.
    [SerializeField] private Button _sellButton; // Reference to the Sell button.
    [SerializeField] private Button _closeButton;
    [SerializeField] private GameObject _uiMenu;

    private GameObject _playerObject;

    // Store the currently selected product button.
    private GameObject _selectedProductButton;
    // Store the currently selected category index.
    private int _currentCategory = 0;

    private void Start()
    {
        // Initialize the UI.
        InitializeUI();
        // Get the player from the scene, the player from the prefab won't update in time
        _playerObject = GameObject.Find("Player");
        // Add a click listener to the Close button.
        _closeButton.onClick.AddListener(CloseUI);
    }

    private void InitializeUI()
    {
        // Clear any existing UI elements.
        ClearUI();

        // Set the Buy and Sell buttons as inactive by default.
        _buyButton.gameObject.SetActive(false);
        _sellButton.gameObject.SetActive(false);

        // Iterate through categories in the inventory.
        foreach (var category in _inventory.categories)
        {
            // Instantiate a Category button for each category.
            GameObject categoryButton = Instantiate(_categoryButtonPrefab, _categoryButtonParent);
            categoryButton.GetComponent<Button>().onClick.AddListener(() => SetSelectedCategory(category));

            // Assign the category image from the Category ScriptableObject to the button image.
            categoryButton.transform.Find("CategoryImage").GetComponent<Image>().sprite = category.categoryImage;
        }

        // Set the default selected category.
        if (_inventory.categories.Count > 0)
        {
            SetSelectedCategory(_inventory.categories[0]);
        }

        // Update the gold balance text.
        UpdateGoldBalance();
    }

    private void ClearUI()
    {
        // Clear the Category buttons.
        foreach (Transform child in _categoryButtonParent)
        {
            Destroy(child.gameObject);
        }

        // Clear the Product buttons.
        foreach (Transform child in _productButtonParent)
        {
            Destroy(child.gameObject);
        }
    }

    private void SetSelectedCategory(Category category)
    {
        // Set the selected category.
        _categoryNameText.text = category.categoryName;
        // Update the currently selected category index.
        _currentCategory = _inventory.categories.IndexOf(category);

        // Clear any existing Product buttons.
        foreach (Transform child in _productButtonParent)
        {
            Destroy(child.gameObject);
        }

        // Instantiate Product buttons for the selected category.
        foreach (var product in category.products)
        {
            GameObject productButton = Instantiate(_productButtonPrefab, _productButtonParent);
            productButton.transform.Find("ProductImage").GetComponent<Image>().sprite = product.productImage;
            productButton.transform.Find("ProductPriceText").GetComponent<TMP_Text>().text = $"Price: {product.price}";
            productButton.transform.Find("ProductQuantityText").GetComponent<TMP_Text>().text = $" {product.quantity}";

            // Get the HighlightImage component and set it as inactive by default.
            Image highlightImage = productButton.transform.Find("HighlightImage").GetComponent<Image>();


            productButton.GetComponent<Button>().onClick.AddListener(() => SelectProduct(product, highlightImage));
            highlightImage.gameObject.SetActive(false);
        }
    }

    private void SelectProduct(Category.Product product, Image highlightImage)
    {
        // Unhighlight the previously selected product, if any.
        if (_selectedProductButton != null)
        {
            _selectedProductButton.transform.Find("HighlightImage").GetComponent<Image>().gameObject.SetActive(false);
        }

        // Highlight the new product.
        highlightImage.gameObject.SetActive(true);

        // Set the Buy and Sell buttons as active.
        _buyButton.gameObject.SetActive(true);
        _sellButton.gameObject.SetActive(true);

        // Store the currently selected product button.
        _selectedProductButton = highlightImage.transform.parent.gameObject;

        // Add click listeners to the Buy and Sell buttons using the external BuyButton and SellButton references.
        _buyButton.onClick.RemoveAllListeners();
        _buyButton.onClick.AddListener(() => BuyProduct(product));

        _sellButton.onClick.RemoveAllListeners();
        _sellButton.onClick.AddListener(() => SellProduct(product));

        EquipProduct(product);
    }

    private void BuyProduct(Category.Product product)
    {
        // Check if the player has enough gold to buy the product.
        if (_wallet.GetCurrentGold() >= product.price)
        {
            // Implement your buying logic here.
            _wallet.SubtractGold(product.price);
            product.quantity++;
            // Update the UI to reflect the changes.
            UpdateGoldBalance();
            // Reset the product UI for the currently selected category.
            SetSelectedCategory(_inventory.categories[_currentCategory]);
        }
        else
        {
            // Not enough gold to buy the product. Display a message or take appropriate action.
        }
    }

    private void SellProduct(Category.Product product)
    {
        // Check if the player has the product in their inventory.
        if (product.quantity > 0)
        {
            // Implement your selling logic here.
            _wallet.AddGold(product.price);
            product.quantity--;
            // Update the UI to reflect the changes.
            UpdateGoldBalance();
            // Reset the product UI for the currently selected category.
            SetSelectedCategory(_inventory.categories[_currentCategory]);
        }
        else
        {
            // The player doesn't have the product to sell. Display a message or take appropriate action.
        }
    }

    // Update the gold balance text.
    private void UpdateGoldBalance()
    {
        _goldBalanceText.text = $"Gold: {_wallet.GetCurrentGold()}";
    }

    // Function to update the quantity text of a product in the UI.
    private void UpdateProductQuantity(Category.Product product)
    {
        // Find the product button associated with the product.
        Transform productButton = _productButtonParent.Find(product.productName); // Assuming the product name is unique.

        if (productButton != null)
        {
            TMP_Text quantityText = productButton.Find("ProductQuantityText").GetComponent<TMP_Text>();
            quantityText.text = $" {product.quantity}";

            // If the quantity is zero, you can disable or hide the product button as well.
            if (product.quantity == 0)
            {
                productButton.gameObject.SetActive(false);
            }
        }
    }

    private void EquipProduct(Category.Product product)
    {
        // Check if the product quantity is greater than 0 before equipping.
        if (product.quantity > 0)
        {
            // Unequip existing product in the category
            UnequipCategory(_inventory.categories[_currentCategory]);

            // Equip the new product
            product.isEquipped = true;

            // Reload the player
            _playerObject.GetComponent<AssassinDresser>().InitializeFromInventory();
            _playerObject.GetComponent<AssassinDresser>().UpdateCharacter();
        }
        else
        {
            // Handle the case where the product quantity is 0 and cannot be equipped.
            // You can display a message or take appropriate action here.
        }
    }

    private void UnequipCategory(Category category)
    {
        foreach (var prod in category.products)
        {
            prod.isEquipped = false;
        }
    }

    private void CloseUI()
    {
        // Close the shop UI here. I can't destroy it because I'm inside of it.
        _uiMenu.SetActive(false); // Disabling the entire UI for simplicity.
        // Enable player movement
        FindObjectOfType<AssassinMovement>().isOnMenu = false;
    }
}

