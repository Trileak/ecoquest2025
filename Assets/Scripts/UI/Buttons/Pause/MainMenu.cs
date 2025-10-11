using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
    [SerializeField] private Button button;

    private void Awake()
    {
        button.onClick.AddListener(OnMainMenuReturn);
    }

    private void OnMainMenuReturn()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
