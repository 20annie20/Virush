using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    Player player; 
    public void StartButton()
    {
        player = FindObjectOfType<Player>();
        SceneManager.LoadScene("Level" + player.Level);
        player.Level = 1;
        player.LoadPlayer();       
    }

    public void ShopButton()
    {
        player = FindObjectOfType<Player>();
        SceneManager.LoadScene("Shop");
        player.LoadPlayer();
    }
    
    public void QuitButton()
    {
        Application.Quit();
    }

}