using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    private Canvas gameCanvas;
    
    [SerializeField] private Button myButton;

    private void Start()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);
        
        myButton?.onClick.AddListener(OnPlayPressed);
        Time.timeScale = 0f; // Pause game while in menu
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnPlayPressed()
    {
        Events.TriggerGameStart();
        Time.timeScale = 1f; // Resume game time
        SceneManager.UnloadSceneAsync("MainMenu");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}