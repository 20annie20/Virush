using UnityEngine;

public class Shop : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        player.LoadPlayer();
        //player.Coins = 100;
        ShopManager.Instance.OpenShop(player);
    } 


    public void SelectUpdate(int button)
    {
        //0 - life
        //1 - speed
        //2 - weapon
        //3 - exit
        Debug.Log("SelectButton() : " + button);

        //switch beetwen item

        switch(button)
        {
            case 0: //update life
                ShopManager.Instance.BuyUpdateLife(player);
                break;
            case 1: //update speed
                ShopManager.Instance.BuyUpdateSpeed(player);
                break;
            case 2: //update weapon
                ShopManager.Instance.BuyUpdateWeapon(player);
                break;
            case 3: //button exit
                player.SavePlayer();
                ShopManager.Instance.Exit(player);
                break;
        }
    }
    public void Exit()
    {
        ShopManager.Instance.Exit(player);
    }

    public void Reset()
    {
        ShopManager.Instance.Reset(player);
    }
}
