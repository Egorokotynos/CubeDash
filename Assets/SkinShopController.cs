using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using UnityEngine.Events;
using System;
using CubeDash.Assets.Skins;

public class SkinShop : MonoBehaviour
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
    private string _MONEY_200_ID => DonateSystemIAP.MONEY_200_ID;
    private string _MONEY_500_ID => DonateSystemIAP.MONEY_500_ID;

    private string SKIN1_PRODUCT_ID => DonateSystemIAP.SKIN1_PRODUCT_ID;
    private string SKIN2_PRODUCT_ID => DonateSystemIAP.SKIN2_PRODUCT_ID;


    private void Start()
    {
        UpdateButtonStates();
        UpdateCoinsText();

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
        DonateSystemIAP.SYSTEM.BuyProduct(SKIN1_PRODUCT_ID, ActionIfBought);
    }

    public void BuySkin2()
    {
        Debug.Log("BuySkin2 called.");
        DonateSystemIAP.SYSTEM.BuyProduct(SKIN2_PRODUCT_ID, ActionIfBought);
    }

    private void OnBuyDonateCurrencyGame(string count)
    {
        DonateSystemIAP.SYSTEM.BuyProduct(count, ActionIfBought);
    }
    private void ActionIfBought(string PRODUCT_ID)
    {

        if (PRODUCT_ID == SKIN1_PRODUCT_ID)
        {
            PlayerPrefs.SetInt("skin1isbought", 1);
            EquipSkin("skin1");
        }
        else if (PRODUCT_ID == SKIN2_PRODUCT_ID)
        {
            PlayerPrefs.SetInt("skin2isbought", 1);
            EquipSkin("skin2");
        }

        int coins = PlayerPrefs.GetInt("allscore", 0);

        if (PRODUCT_ID == _MONEY_200_ID)
        {
            coins += 200;
            PlayerPrefs.SetInt("allscore", coins);
        }
        else if (PRODUCT_ID == _MONEY_500_ID)
        {
            coins += 500;
            PlayerPrefs.SetInt("allscore", coins);
        }

        UpdateButtonStates();
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

    private void UpdateButtonStates()
    {
        Debug.Log("UpdateButtonStates called.");
        // Update the text on each button based on whether the corresponding skin is bought or equipped
        UpdateButtonText(diamondButton, "diamond", diamondPrice.ToString());
        UpdateButtonText(magmaButton, "magma", magmaPrice.ToString());
        UpdateButtonText(goldButton, "gold", goldPrice.ToString());
        UpdateButtonText(spaceButton, "space", spacePrice.ToString());

        UpdateButtonText(skin1Button, "skin1", "");
        UpdateButtonText(skin2Button, "skin2", "");



        UpdateButtonText(_money200Button, "null", "");
        UpdateButtonText(_money500Button, "null", "");

        try
        {
            Product productSkin1 = DonateSystemIAP.SYSTEM.GetProductByUID(SKIN1_PRODUCT_ID);
            Product productSkin2 = DonateSystemIAP.SYSTEM.GetProductByUID(SKIN2_PRODUCT_ID);

            if (productSkin1 != null)
            {
                UpdateButtonText(skin1Button, "skin1", productSkin1.metadata.localizedPriceString);
            }
            else
            {
                Debug.LogError($"Product with ID {SKIN1_PRODUCT_ID} not found.");
            }

            if (productSkin2 != null)
            {
                UpdateButtonText(skin2Button, "skin2", productSkin2.metadata.localizedPriceString);
            }
            else
            {
                Debug.LogError($"Product with ID {SKIN2_PRODUCT_ID} not found.");
            }

            Product productMoney200 = DonateSystemIAP.SYSTEM.GetProductByUID(_MONEY_200_ID);
            Product productMoney500 = DonateSystemIAP.SYSTEM.GetProductByUID(_MONEY_500_ID);

            if (productMoney200 != null)
            {
                UpdateButtonText(_money200Button, "null", productMoney200.metadata.localizedPriceString);
            }
            else
            {
                Debug.LogError($"Product with ID {_MONEY_200_ID} not found.");
            }

            if (productMoney500 != null)
            {
                UpdateButtonText(_money500Button, "null", productMoney500.metadata.localizedPriceString);
            }
            else
            {
                Debug.LogError($"Product with ID {_MONEY_500_ID} not found.");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error updating button states: {ex.Message}");
        }

    }

    private void UpdateButtonText(Button button, string skinKey, string price = "")
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





}
