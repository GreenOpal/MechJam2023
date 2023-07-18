using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button StartButton;
    [SerializeField] private Button DifficultyButton;
    [SerializeField] private Button CreditsButton;
    [SerializeField] private Button CloseCreditsButton;
    [SerializeField] private CanvasGroup CreditsPanel;
    private void Awake()
    {
        StartButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
