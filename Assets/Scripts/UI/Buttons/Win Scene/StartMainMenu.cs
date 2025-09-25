using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMainMenu : MonoBehaviour
{
    [SerializeField] private Button button;
    
    private void Start()
    {
        button?.onClick.AddListener(OnMainMenu);
    }

    private void OnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
