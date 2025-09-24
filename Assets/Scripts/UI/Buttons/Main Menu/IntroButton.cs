using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    private Canvas gameCanvas;
    
    [SerializeField] private Button myButton;

    private void Start()
    {
        myButton?.onClick.AddListener(OnButtonPressed);
    }

    private void OnButtonPressed()
    {
        Debug.Log("Pressed");
        SceneManager.LoadScene("IntroScene", LoadSceneMode.Additive);
    }
}