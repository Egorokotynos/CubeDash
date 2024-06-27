using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using UnityEngine.Events;
using System;

public class SkinShop : MonoBehaviour, IStoreListener
{
    public GameObject notEnoughMoneyPanel; // Reference to the panel informing the player about not enough money
    public Text coinsText; // Reference to the UI text displaying the number of coins
    public int diamondPrice = 100; // Price of the diamond skin
    public int magmaPrice = 150; // Price of the magma skin
    public int goldPrice = 200; // Price of the gold skin
    public int spacePrice = 250; // Price of the space skin

    public Button diamondButton; // Reference to the button for the diamond skin
    public Button magmaButton; // Reference to the button for the magma skin
    public Button goldButton; // Reference to the button for the gold skin
    public Button spaceButton; // Reference to the button for the space skin
    public Button skin1Button; // Reference to the button for the skin1
    public Button skin2Button; // Reference to the button for the skin2
    [Space]
    [SerializeField] private Button _money200Button;
    [SerializeField] private Button _money500Button;


    private IStoreController storeController;
    private IExtensionProvider extensionProvider;

    private readonly string SKIN1_PRODUCT_ID = "cube.skin1";
    private readonly string SKIN2_PRODUCT_ID = "cube.skin2";

    private readonly string _MONEY_200_ID = "gold.cube.200";
    private readonly string _MONEY_500_ID = "gold.cube.500";

    private void Awake()
    {
        InitPurchases();

    }

    void Start()
    {
        UpdateButtonStates();
        UpdateCoinsText();
        InitPurchases();
        notEnoughMoneyPanel.SetActive(false); // Ensure the panel is hidden at the start

        // Assign button click listeners
        diamondButton.onClick.AddListener(BuyDiamondSkin);
        magmaButton.onClick.AddListener(BuyMagmaSkin);
        goldButton.onClick.AddListener(BuyGoldSkin);
        spaceButton.onClick.AddListener(BuySpaceSkin);
        skin1Button.onClick.AddListener(BuySkin1);
        skin2Button.onClick.AddListener(BuySkin2);

        _money200Button.onClick.AddListener(() => OnBuyDonateCurrencyGame(_MONEY_200_ID));
        _money500Button.onClick.AddListener(() => OnBuyDonateCurrencyGame(_MONEY_500_ID));


        Debug.Log("SkinShop script started and buttons assigned.");
    }



    void UpdateCoinsText()
    {
        coinsText.text = PlayerPrefs.GetInt("allscore", 0).ToString(); // Display the current number of coins
        Debug.Log("Coins text updated: " + coinsText.text);
    }

    public void BuyDiamondSkin()
    {
        Debug.Log("BuyDiamondSkin called.");
        BuySkin("diamond", diamondPrice);
    }

    public void BuyMagmaSkin()
    {
        Debug.Log("BuyMagmaSkin called.");
        BuySkin("magma", magmaPrice);
    }

    public void BuyGoldSkin()
    {
        Debug.Log("BuyGoldSkin called.");
        BuySkin("gold", goldPrice);
    }

    public void BuySpaceSkin()
    {
        Debug.Log("BuySpaceSkin called.");
        BuySkin("space", spacePrice);
    }

    void BuySkin(string skinKey, int skinPrice)
    {
        Debug.Log($"BuySkin called for {skinKey} with price {skinPrice}.");
        if (PlayerPrefs.GetInt($"{skinKey}isbought", 0) == 1)
        {
            EquipSkin(skinKey);
            UpdateButtonStates();
            return;
        }

        int coins = PlayerPrefs.GetInt("allscore", 0);
        if (coins >= skinPrice)
        {
            coins -= skinPrice;
            PlayerPrefs.SetInt("allscore", coins);
            PlayerPrefs.SetInt($"{skinKey}isbought", 1);
            EquipSkin(skinKey);
            UpdateButtonStates();
            UpdateCoinsText();
        }
        else
        {
            notEnoughMoneyPanel.SetActive(true);
        }
    }

    public void BuySkin1()
    {
        Debug.Log("BuySkin1 called.");
        BuyProduct(SKIN1_PRODUCT_ID);
    }

    public void BuySkin2()
    {
        Debug.Log("BuySkin2 called.");
        BuyProduct(SKIN2_PRODUCT_ID);
    }

    private void OnBuyDonateCurrencyGame(string count)
    {
        BuyProduct(count);
    }

    void BuyProduct(string productId)
    {
        Debug.Log($"BuyProduct called for {productId}.");
        if (IsInitialized())
        {
            Product product = storeController.products.WithID(productId);

            if (product != null && product.availableToPurchase)
            {
                storeController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    void EquipSkin(string skinKey)
    {
        Debug.Log($"EquipSkin called for {skinKey}.");
        // Set all skins to not equipped
        PlayerPrefs.SetInt("diamondison", 0);
        PlayerPrefs.SetInt("magmaison", 0);
        PlayerPrefs.SetInt("goldison", 0);
        PlayerPrefs.SetInt("spaceison", 0);
        PlayerPrefs.SetInt("skin1ison", 0);
        PlayerPrefs.SetInt("skin2ison", 0);

        // Set the chosen skin to equipped
        PlayerPrefs.SetInt($"{skinKey}ison", 1);
    }

    void UpdateButtonStates()
    {
        Debug.Log("UpdateButtonStates called.");
        // Update the text on each button based on whether the corresponding skin is bought or equipped
        UpdateButtonText(diamondButton, "diamond", diamondPrice.ToString());
        UpdateButtonText(magmaButton, "magma", magmaPrice.ToString());
        UpdateButtonText(goldButton, "gold", goldPrice.ToString());
        UpdateButtonText(spaceButton, "space", spacePrice.ToString());

        Product productSkin1 = storeController.products.WithID(SKIN1_PRODUCT_ID);
        Product productSkin2 = storeController.products.WithID(SKIN2_PRODUCT_ID);

        UpdateButtonText(skin1Button, "skin1", productSkin1.metadata.localizedPriceString);
        UpdateButtonText(skin2Button, "skin2", productSkin2.metadata.localizedPriceString);

        Product productMoney200 = storeController.products.WithID(_MONEY_200_ID);
        Product productMoney500 = storeController.products.WithID(_MONEY_500_ID);

        UpdateButtonText(_money200Button, "null", productMoney200.metadata.localizedPriceString);
        UpdateButtonText(_money500Button, "null", productMoney500.metadata.localizedPriceString);

    }

    void UpdateButtonText(Button button, string skinKey, string price = "")
    {
        Text buttonText = button.GetComponentInChildren<Text>();
        if (PlayerPrefs.GetInt($"{skinKey}ison", 0) == 1)
        {
            buttonText.text = "Take off";
        }
        else if (PlayerPrefs.GetInt($"{skinKey}isbought", 0) == 1)
        {
            buttonText.text = "Take on";
        }
        else
        {
            buttonText.text = price.Length >= 1 ? $"Buy ({price})" : "Buy";
        }
    }

    private void InitPurchases()
    {
        Debug.Log("InitPurchases called.");
        if (IsInitialized())
        {
            return;
        }

        var storeBuilder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        storeBuilder.AddProduct(SKIN1_PRODUCT_ID, ProductType.NonConsumable);
        storeBuilder.AddProduct(SKIN2_PRODUCT_ID, ProductType.NonConsumable);

        storeBuilder.AddProduct(_MONEY_200_ID, ProductType.Consumable);
        storeBuilder.AddProduct(_MONEY_500_ID, ProductType.Consumable);

        UnityPurchasing.Initialize(this, storeBuilder);
    }

    private bool IsInitialized() => storeController != null && extensionProvider != null;

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
        extensionProvider = extensions;
        Debug.Log("OnInitialized called.");
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.Log($"OnInitializeFailed InitializationFailureReason: {error}, Message: {message}");
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        Debug.Log($"ProcessPurchase called for {args.purchasedProduct.definition.id}.");
        if (args.purchasedProduct.definition.id == SKIN1_PRODUCT_ID)
        {
            PlayerPrefs.SetInt("skin1isbought", 1);
            EquipSkin("skin1");
        }
        else if (args.purchasedProduct.definition.id == SKIN2_PRODUCT_ID)
        {
            PlayerPrefs.SetInt("skin2isbought", 1);
            EquipSkin("skin2");
        }

        int coins = PlayerPrefs.GetInt("allscore", 0);

        if (args.purchasedProduct.definition.id == _MONEY_200_ID)
        {
            coins += 200;
            PlayerPrefs.SetInt("allscore", coins);
        }
        else if (args.purchasedProduct.definition.id == _MONEY_500_ID)
        {
            coins += 500;
            PlayerPrefs.SetInt("allscore", coins);
        }


        UpdateButtonStates();
        return PurchaseProcessingResult.Complete;

    }
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}