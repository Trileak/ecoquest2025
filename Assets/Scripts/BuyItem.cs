using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    private Button myButton;

    void Start()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(HandleClick);
    }

    void HandleClick()
    {
        Debug.Log("The button hath been pressed!");
    }
}