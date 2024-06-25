using UnityEngine;
using UnityEngine.UI;

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

    void Start()
    {
        UpdateButtonStates();
        UpdateCoinsText();
    }

    void UpdateCoinsText()
    {
        coinsText.text = PlayerPrefs.GetInt("allscore", 0).ToString(); // Display the current number of coins
    }

    public void BuyDiamondSkin()
    {
        if (PlayerPrefs.GetInt("diamondisbought", 0) == 1)
        {
            EquipSkin("diamond");
            UpdateButtonStates();
            return;
        }

        int coins = PlayerPrefs.GetInt("allscore", 0); // Get the current number of coins
        if (coins >= diamondPrice) // Check if the player has enough coins to buy the diamond skin
        {
            coins -= diamondPrice; // Deduct the price of the diamond skin from the coins
            PlayerPrefs.SetInt("allscore", coins); // Save the updated number of coins
            PlayerPrefs.SetInt("diamondisbought", 1); // Set the diamond skin as bought
            EquipSkin("diamond"); // Equip the diamond skin
            UpdateButtonStates(); // Update the button states
            UpdateCoinsText(); // Update the displayed number of coins
        }
        else
        {
            notEnoughMoneyPanel.SetActive(true); // Activate the panel informing the player about not enough money
        }
    }

    public void BuyMagmaSkin()
    {
        if (PlayerPrefs.GetInt("magmaisbought", 0) == 1)
        {
            EquipSkin("magma");
            UpdateButtonStates();
            return;
        }

        int coins = PlayerPrefs.GetInt("allscore", 0);
        if (coins >= magmaPrice)
        {
            coins -= magmaPrice;
            PlayerPrefs.SetInt("allscore", coins);
            PlayerPrefs.SetInt("magmaisbought", 1);
            EquipSkin("magma");
            UpdateButtonStates();
            UpdateCoinsText();
        }
        else
        {
            notEnoughMoneyPanel.SetActive(true);
        }
    }

    public void BuyGoldSkin()
    {
        if (PlayerPrefs.GetInt("goldisbought", 0) == 1)
        {
            EquipSkin("gold");
            UpdateButtonStates();
            return;
        }

        int coins = PlayerPrefs.GetInt("allscore", 0);
        if (coins >= goldPrice)
        {
            coins -= goldPrice;
            PlayerPrefs.SetInt("allscore", coins);
            PlayerPrefs.SetInt("goldisbought", 1);
            EquipSkin("gold");
            UpdateButtonStates();
            UpdateCoinsText();
        }
        else
        {
            notEnoughMoneyPanel.SetActive(true);
        }
    }

    public void BuySpaceSkin()
    {
        if (PlayerPrefs.GetInt("spaceisbought", 0) == 1)
        {
            EquipSkin("space");
            UpdateButtonStates();
            return;
        }

        int coins = PlayerPrefs.GetInt("allscore", 0);
        if (coins >= spacePrice)
        {
            coins -= spacePrice;
            PlayerPrefs.SetInt("allscore", coins);
            PlayerPrefs.SetInt("spaceisbought", 1);
            EquipSkin("space");
            UpdateButtonStates();
            UpdateCoinsText();
        }
        else
        {
            notEnoughMoneyPanel.SetActive(true);
        }
    }

    void EquipSkin(string skinKey)
    {
        // Set all skins to not equipped
        PlayerPrefs.SetInt("diamondison", 0);
        PlayerPrefs.SetInt("magmaison", 0);
        PlayerPrefs.SetInt("goldison", 0);
        PlayerPrefs.SetInt("spaceison", 0);

        // Set the chosen skin to equipped
        PlayerPrefs.SetInt($"{skinKey}ison", 1);
    }

    void UpdateButtonStates()
    {
        // Update the text on each button based on whether the corresponding skin is bought or equipped
        diamondButton.GetComponentInChildren<Text>().text = PlayerPrefs.GetInt("diamondison", 0) == 1 ? "Take off" : (PlayerPrefs.GetInt("diamondisbought", 0) == 1 ? "Take on" : "Buy");
        magmaButton.GetComponentInChildren<Text>().text = PlayerPrefs.GetInt("magmaison", 0) == 1 ? "Take off" : (PlayerPrefs.GetInt("magmaisbought", 0) == 1 ? "Take on" : "Buy");
        goldButton.GetComponentInChildren<Text>().text = PlayerPrefs.GetInt("goldison", 0) == 1 ? "Take off" : (PlayerPrefs.GetInt("goldisbought", 0) == 1 ? "Take on" : "Buy");
        spaceButton.GetComponentInChildren<Text>().text = PlayerPrefs.GetInt("spaceison", 0) == 1 ? "Take off" : (PlayerPrefs.GetInt("spaceisbought", 0) == 1 ? "Take on" : "Buy");
    }
}
