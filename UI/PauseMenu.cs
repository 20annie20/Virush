using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    Player player; 
    public GameObject pauseMenuUI;
    public GameObject questionBar;
    public GameObject panelHUD;
    public GameObject MobileSingleStickControl;
    public GameObject InstructionBar1;
    public GameObject InstructionBar2;

    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || CrossPlatformInputManager.GetButtonDown("PauseButton"))
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        panelHUD.SetActive(false);
        questionBar.SetActive(false);
        MobileSingleStickControl.SetActive(false);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void ResumeGame()
    {
        InstructionBar2.SetActive(false);
        pauseMenuUI.SetActive(false);
        questionBar.SetActive(false);
        panelHUD.SetActive(true);
        MobileSingleStickControl.SetActive(true);
        Time.timeScale = 1f;
        isGamePaused = false;
        GameObject.Find("PauseButton").GetComponentInChildren<Text>().text = "PAUSE";
        GameObject.Find("PauseButton").SetActive(true);
    }

    public void AskToGoToMenu()
    {
        pauseMenuUI.SetActive(false);
        questionBar.SetActive(true);
    }

    public void Settings()
    {
        player.SavePlayer();
        Debug.Log("loading settings...");
        //SceneManager.LoadScene("Settings");
    }

    public void LoadMenu()
    {
        questionBar.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
        player.SavePlayer();
        SceneManager.LoadScene("Menu");
    }
    
    public void LoadNextInstruction()
    {
        InstructionBar1.SetActive(false);
        InstructionBar2.SetActive(true);
    }
}
