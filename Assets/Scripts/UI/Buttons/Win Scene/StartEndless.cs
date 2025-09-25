using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartEndless : MonoBehaviour
{
    [SerializeField] private Button button;

    void Start()
    {
        button?.onClick.AddListener(onStartEndless);
    }

    private void onStartEndless()
    {
        Time.timeScale = 1f;
        SceneManager.UnloadSceneAsync("WinScene");
    }
}
