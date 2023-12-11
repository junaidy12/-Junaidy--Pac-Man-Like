using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] Button exitButton;

    private void Start()
    {
        playButton.onClick.AddListener(() => 
        {
            SceneManager.LoadScene("Gameplay");
        });

        exitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}


