using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button StartButton;
    private void Awake()
    {
        StartButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
