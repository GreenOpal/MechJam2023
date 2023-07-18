using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MechJam {

    public class MenuController : MonoBehaviour
    {
        [SerializeField] private GameConfig Config;
        [SerializeField] private Button StartButton;
        [SerializeField] private Button DifficultyButton;
        [SerializeField] private Button CreditsButton;
        [SerializeField] private Button CloseCreditsButton;
        [SerializeField] private TMP_Text DifficultyText;
        [SerializeField] private CanvasGroup CreditsPanel;
        private string DifficultyBase = "Difficulty\n";
        private string DifficultyEasy = "Easy";
        private string DifficultyMedium = "Medium";
        private string DifficultyHard = "Hard";
        private void Awake()
        {
            StartButton.onClick.AddListener(StartGame);
            DifficultyButton.onClick.AddListener(SetDifficulty);
            CreditsButton.onClick.AddListener(ShowCredits);
            CloseCreditsButton.onClick.AddListener(HideCredits);
            DisplayDifficulty();
        }

        private void ShowCredits()
        {
            CreditsPanel.alpha = 1;
            CreditsPanel.interactable = true;
            CreditsPanel.blocksRaycasts = true;
        }

        private void HideCredits()
        {
            CreditsPanel.alpha = 0;
            CreditsPanel.interactable = false;
            CreditsPanel.blocksRaycasts = false;
        }

        private void SetDifficulty()
        {
            Config.SelectedDifficulty = (Config.SelectedDifficulty + 1) % 3;
            DisplayDifficulty();
        }
        private void DisplayDifficulty ()
        {
            switch (Config.SelectedDifficulty)
            {
                case 0:
                    DifficultyText.SetText(DifficultyBase + DifficultyEasy);
                    break;
                case 1:
                    DifficultyText.SetText(DifficultyBase + DifficultyMedium);
                    break;
                case 2:
                    DifficultyText.SetText(DifficultyBase + DifficultyHard);
                    break;
            }
        }

        private void StartGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }
}
