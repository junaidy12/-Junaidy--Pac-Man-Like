using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] Button retryButton;
    [SerializeField] Button mainMenuButton;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        retryButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Gameplay");
        });

        mainMenuButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainMenu");
        });
    }

    public void Click()
    {
        print("blokl");
    }
}


