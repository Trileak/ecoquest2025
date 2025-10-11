using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpenMainMenu : MonoBehaviour
{
    [SerializeField] private Button button;
    
    private void Start()
    {
        button.onClick.AddListener(OnEndless);
    }

    private void OnEndless()
    {
        SceneManager.UnloadSceneAsync("LosePopup");
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
