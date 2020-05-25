using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ShopManager : MonoBehaviour
{
    public Text playerCoinsCountText;

    public Text updateLifeCountText;
    public Text updateSpeedCountText;
    public Text updateWeaponCountText;

    int updateLife;
    int updateSpeed;
    int updateWeapon;

    public Text priceLifeText;
    public Text priceSpeedText;
    public Text priceWeaponText;

    int priceL;
    int priceS;
    int priceW;

    public Button buttonBuyLife;
    public Button buttonBuySpeed;
    public Button buttonBuyWeapon;

    public Button buttonExit;

    private static ShopManager _instance;
    public static ShopManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Shop Manager is Null");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }

    public void OpenShop(Player player)
    {
        playerCoinsCountText.text = "Coins: " + player.Coins + " C";
        StatusUpdate(player);
    }

    public void BuyUpdateLife(Player player)
    {
        //variable player.MaxHealth
        if (player.Coins >= priceL && updateLife < 3)
        {
            player.Coins -= priceL;
            player.MaxHealth++;
            playerCoinsCountText.text = "Coins: " + player.Coins + " C";
            StatusUpdateLife(player);
        }
    }

    public void BuyUpdateSpeed(Player player )
    {
        //variable player.SpeedRate
        //sprawdza, czy stać biedaka
        if (player.Coins >= priceS && updateSpeed < 3 && player.SpeedRate < 1.8)
        {
            //wyciągamy hajs z portfela
            player.Coins -= priceS;
            player.SpeedRate += 0.2f;
            
            playerCoinsCountText.text = "Coins: " + player.Coins + " C";
            //stać więc burżuj wydaje
            StatusUpdateSpeed(player);
        }
    }

    public void BuyUpdateWeapon(Player player)
    {
        //use Variable player.DamageValue
        if (player.Coins >= priceW && updateWeapon < 3)
        {
            player.Coins -= priceW;
            player.DamageValue++;
            playerCoinsCountText.text = "Coins: " + player.Coins + " C";
            StatusUpdateWeapon(player);
        }
    }

    private void StatusUpdateLife (Player player)
    {
        Debug.Log("MaxHealth = " + player.MaxHealth);

        switch (player.MaxHealth)
        {
            case 10:
                updateLife = 0;
                updateLifeCountText.text = "+" + updateLife;
                priceL = 3;
                priceLifeText.text = "Price: " + priceL + " C";
                break;
            case 11:
                updateLife = 1;
                updateLifeCountText.text = "+" + updateLife;
                priceL = 5;
                priceLifeText.text = "Price: " + priceL + " C";
                break;
            case 12:
                updateLife = 2;
                updateLifeCountText.text = "+" + updateLife;
                priceL = 10;
                priceLifeText.text = "Price: " + priceL + " C";
                break;
            case 13:
                updateLife = 3;
                updateLifeCountText.text = "+" + updateLife;
                priceLifeText.text = "FULL";
                break;
        }
    }
    
    private int speed;
    private void StatusUpdateSpeed(Player player)  
    {
        string speedString = "" + player.SpeedRate;
        Debug.Log("SpeedRate = " + speedString);
        var valueS = new Tuple<string, string, string, string>( "1", "1,2", "1,4", "1,6");

        if (speedString == valueS.Item1)
            speed = 1;
        if (speedString == valueS.Item2)
            speed = 2;
        if (speedString == valueS.Item3)
            speed = 3;
        if (speedString == valueS.Item4)
            speed = 4;

        switch (speed)
        {
            case 1 :
                updateSpeed = 0;
                updateSpeedCountText.text = "+" + updateSpeed;
                priceS = 3;
                priceSpeedText.text = "Price: " + priceS + " C";
                break;
            case 2:
                updateSpeed = 1;
                updateSpeedCountText.text = "+" + updateSpeed;
                priceS = 5;
                priceSpeedText.text = "Price: " + priceS + " C";
                break;
            case 3:
                updateSpeed = 2;
                updateSpeedCountText.text = "+" + updateSpeed;
                priceS = 10;
                priceSpeedText.text = "Price: " + priceS + " C";
                break;
            case 4:
                updateSpeed = 3;
                updateSpeedCountText.text = "+" + updateSpeed;
                priceSpeedText.text = "FULL";
                break;
        }
    }

    private void StatusUpdateWeapon(Player player)
    {
        Debug.Log("DamageValue = " + player.DamageValue);

        switch (player.DamageValue)
        {
            case 1:
                updateWeapon = 0;
                updateWeaponCountText.text = "+" + updateWeapon;
                priceW = 3;
                priceWeaponText.text = "Price: " + priceW + " C";
                break;
            case 2:
                updateWeapon = 1;
                updateWeaponCountText.text = "+" + updateWeapon;
                priceW = 5;
                priceWeaponText.text = "Price: " + priceW + " C";
                break;
            case 3:
                updateWeapon = 2;
                updateWeaponCountText.text = "+" + updateWeapon;
                priceW = 10;
                priceWeaponText.text = "Price: " + priceW + " C";
                break;
            case 4:
                updateWeapon = 3;
                updateWeaponCountText.text = "+" + updateWeapon;
                priceWeaponText.text = "FULL";
                break;
        }
    }
    public void StatusUpdate(Player player)
    {
        StatusUpdateLife(player);
        StatusUpdateSpeed(player);
        StatusUpdateWeapon(player);
    }
    public void Reset(Player player)
    {
        player.MaxHealth = 10;
        player.SpeedRate = 1;
        player.DamageValue = 1;
        player.Coins = 100;
        OpenShop(player);
    }

    public void Exit(Player player)
    {
        Debug.Log("wychodzimy ze sklepu");
        player.SavePlayer(); 
        SceneManager.LoadScene("Menu"); 
    }
}
