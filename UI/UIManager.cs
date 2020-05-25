using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("UI Manager is Null");
            }
            return _instance;
        }
    }

    public Text playerHealth;
    public Text playerCoins;

    public GameObject hudPanel;
    public GameObject mobileSingleStickControl;
    public GameObject gameOverPanel;
    public GameObject congratulationsPanel;
    public GameObject finishedGamePanel;

    private void Awake()
    {
        _instance = this;
    }

    //these are not in PauseMenu class, because these functions are called by player without constructing unnecessary objects
    public void OpenGameOver()
    {
        hudPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void DisplayCongratulations()
    {
        hudPanel.SetActive(false);
        mobileSingleStickControl.SetActive(false);
        congratulationsPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void DisplayFinishedGame()
    {
        hudPanel.SetActive(false);
        mobileSingleStickControl.SetActive(false);
        finishedGamePanel.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void TryAgain()
    {
        //resume game
        GameManager.Instance.Respawn(false);
        gameOverPanel.SetActive(false);
    }

    public void RenewPanelHealth(int health)
    {
        playerHealth.text = "" + health;
    }

    public void RenewPanelCoins(int coins)
    {
        playerCoins.text = "" + coins;
    }
}
