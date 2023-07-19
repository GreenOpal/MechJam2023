using System;
using System.Collections;
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
        [SerializeField] private Button QuitButton;
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
            QuitButton.onClick.AddListener(() => Application.Quit());
            DisplayDifficulty();
            StartCoroutine(PlayMusicWhenReady());
        }

        private IEnumerator PlayMusicWhenReady()
        {
            while(AudioController.HasInstance == false)
            {
                yield return null;
            }
            yield return new WaitForEndOfFrame();
            AudioController.Instance.PlayMusic(AudioController.AudioKeys.Music_Title);
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
            Config.DifficultySettings.SelectedDifficulty = (Config.DifficultySettings.SelectedDifficulty + 1) % 3;
            DisplayDifficulty();
        }
        private void DisplayDifficulty ()
        {
            switch (Config.DifficultySettings.SelectedDifficulty)
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
