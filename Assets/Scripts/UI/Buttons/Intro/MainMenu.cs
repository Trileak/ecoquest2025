using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button button;

    private void Awake()
    {
        button.onClick.AddListener(OnBack);
    }

    private void OnBack()
    {
        SceneManager.UnloadSceneAsync("IntroScene");
    }
}
