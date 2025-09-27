using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Endless : MonoBehaviour
{
    [SerializeField] private Button button;

    private void Start()
    {
        button.onClick.AddListener(OnEndless);
    }

    private void OnEndless()
    {
        Debug.Log("Endless");
        Time.timeScale = 1f;

        Scene winScene = SceneManager.GetSceneByName("WinPopup");
        if (winScene.isLoaded)
        {
            Events.TriggerGameEnd();
            SceneManager.UnloadSceneAsync(winScene);
        }

        Events.MouseControl(true);
    }

}
