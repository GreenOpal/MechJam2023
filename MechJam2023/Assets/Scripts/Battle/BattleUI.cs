using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;

namespace MechJam {
    public class BattleUI : BattleConfigurableBehaviour
    {
        [SerializeField] private CanvasGroup Actions;
        [SerializeField] private Button[] ActionButtons;
        [SerializeField] private CanvasGroup Targets;
        [SerializeField] private Button[] TargetButtons;

        [SerializeField] public Image PlayerHPBar;
        [SerializeField] public Image EnemyHPBar;
        [SerializeField] public Image MechPartHPBar;
        [SerializeField] private TextMeshProUGUI MechPartStatsText;

        [SerializeField] private GameObject QuitPanel;
        [SerializeField] private Button QuitButton;
        [SerializeField] private Button QuitCancelButton;

        [SerializeField] private GameObject EndGamePanel;
        [SerializeField] private TMP_Text EndGameText;
        [SerializeField] private Button EndGameButton;
        [SerializeField] private string WinText = "YOU WIN!!!";
        [SerializeField] private string LoseText = "YOU LOSE...";

        private int selectedWeapon;
        private int selectedTarget;
        private PlayerInput _input;

        public override void Initialize(GameControllerBase controller)
        {
            base.Initialize(controller);

            for (int i = 0; i < ActionButtons.Length; i++)
            {
                var currentButton = i;
                ActionButtons[i].onClick.AddListener(() => UseAttackButton(currentButton));

                var eventTrigger = ActionButtons[i].gameObject.AddComponent<EventTrigger>();
                AddEventTriggerListener(eventTrigger, EventTriggerType.PointerEnter, (data) => ShowMechPartStats(ActionButtons[currentButton], _battleController.PlayerMech));
                AddEventTriggerListener(eventTrigger, EventTriggerType.PointerExit, HideMechPartStats);
            }

            for (int i = 0; i < TargetButtons.Length; i++)
            {
                var currentButton = i;
                TargetButtons[i].onClick.AddListener(() => UseTargetButton(currentButton));

                var eventTrigger = TargetButtons[i].gameObject.AddComponent<EventTrigger>();
                AddEventTriggerListener(eventTrigger, EventTriggerType.PointerEnter, (data) => ShowMechPartStats(TargetButtons[currentButton], _battleController.EnemyMech));
                AddEventTriggerListener(eventTrigger, EventTriggerType.PointerExit, HideMechPartStats);
            }
            QuitButton.onClick.AddListener(Quit);
            QuitCancelButton.onClick.AddListener(CancelQuit);

            _input = new PlayerInput();
            _input.Player.Enable();
            _input.Player.Cancel.performed += ShowQuitMenu;

            EndGameButton.onClick.AddListener(Quit);


            _battleController.OnMechWasAttacked += DetermineAttackStatus;
            _battleController.OnPartDestroyed += ShowDestroyedPart;
            _battleController.OnMechDestroyed += ShowEndScreen;

            SetTargetPanelState(false);
            EndGamePanel.SetActive(false);
            QuitPanel.SetActive(false);
        }

        private void ShowEndScreen(Mech mech)
        {
            EndGameText.SetText(mech.IsPlayer ? LoseText : WinText);
            AudioController.Instance.PlaySFX(mech.IsPlayer ? AudioController.AudioKeys.SFX_Ambience_Laugh: AudioController.AudioKeys.SFX_Ambience_Cheer);
            EndGamePanel.SetActive(true);
        }

        private void ShowDestroyedPart(Mech arg1, MechPart arg2)
        {
            //TODO: set button disabled/change colour
        }

        public override void DeInitialize()
        {
            base.DeInitialize();
            _battleController.OnMechWasAttacked -= DetermineAttackStatus;
            _input.Player.Disable();
            _input.Dispose();
            _input.Player.Cancel.performed -= ShowQuitMenu;
        }


        private void Quit()
        {
            _controller.Quit();
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        private void CancelQuit()
        {
            QuitPanel.SetActive(false);
        }
        private void ShowQuitMenu(UnityEngine.InputSystem.InputAction.CallbackContext _)
        {
            QuitPanel.SetActive(!QuitPanel.activeSelf);
        }
        private void SetTargetPanelState(bool state)
        {
            Targets.alpha = state? 1 : 0;
            Targets.interactable = state;
            Targets.blocksRaycasts = state;
        }

        private void DetermineAttackStatus(Mech mech, MechPart part, int _)
        {
            Actions.alpha = mech.IsPlayer ? 1 : 0;
            Actions.blocksRaycasts = mech.IsPlayer;
            Actions.interactable = mech.IsPlayer;
            UpdatePlayerHealth();
        }

        private void UpdatePlayerHealth()
        {
            PlayerHPBar.fillAmount = _battleController.PlayerMech.CurrentHealth;
            EnemyHPBar.fillAmount = _battleController.EnemyMech.CurrentHealth;

            for (int i = 0; i < ActionButtons.Length; i++)
            {
                var attackPart = (Mech.AttackPart)i;
                ActionButtons[i].interactable = _battleController.PlayerMech.PartMap[attackPart].Durability > 0;
            }
            for (int i = 0; i < TargetButtons.Length; i++)
            {
                var attackPart = (Mech.AttackPart)i;
                TargetButtons[i].interactable = _battleController.EnemyMech.PartMap[attackPart].Durability > 0;
            }
        }

        private void UseAttackButton( int i )
        {
            Debug.LogWarning($"Selected {System.Enum.GetName(typeof(Mech.AttackPart), i + 1)}");
            selectedWeapon = i + 1;
            SetTargetPanelState(true);


            AudioController.Instance.PlaySFX(AudioController.AudioKeys.SFX_UI_Select);
        }

        private void UseTargetButton(int currentButton)
        {
            TargetButtons[currentButton].Select();
            if (selectedTarget == currentButton)
            {
                StartAttack();
            }
            else
            {
                Debug.LogWarning($"Target {System.Enum.GetName(typeof(Mech.AttackPart), currentButton)}");
                selectedTarget = currentButton;
                AudioController.Instance.PlaySFX(AudioController.AudioKeys.SFX_UI_Select);
                //TODO: UI Tooltip kinda element hopefully
            }
        }

        private void OnBack()
        {
            selectedWeapon = 0;
            selectedTarget = 0;
            SetTargetPanelState(false);
        }

        private void StartAttack()
        {
            if (_battleController.PlayerMech.Attack((Mech.AttackPart)selectedWeapon, _battleController.EnemyMech, (Mech.AttackPart)selectedTarget))
            {
                OnBack();
            }
        }

        private void ShowMechPartStats(Button button, Mech mech)
        {
            int index = button.transform.GetSiblingIndex();
            MechPart part;
            if (mech.PartMap.TryGetValue((Mech.AttackPart)index, out part))
            {
                MechPartHPBar.transform.parent.gameObject.SetActive(true);
                MechPartHPBar.gameObject.SetActive(true);
                MechPartHPBar.fillAmount = (float)part.Durability / part.MaxDurability;
                MechPartStatsText.text = $"{(mech.IsPlayer ? "Player" : "Enemy")} Attack: {part.Attack}\n{(mech.IsPlayer ? "Player" : "Enemy")} Defense: {part.Defense}";
                AudioController.Instance.PlaySFX(AudioController.AudioKeys.SFX_UI_Move);
            }
            else
            {
                MechPartHPBar.transform.parent.gameObject.SetActive(false);
                MechPartHPBar.gameObject.SetActive(false);
                MechPartStatsText.text = string.Empty;
            }
        }






        private void HideMechPartStats(BaseEventData eventData)
        {
            MechPartHPBar.gameObject.SetActive(false);
            MechPartStatsText.text = string.Empty;
            MechPartHPBar.transform.parent.gameObject.SetActive(false);
        }

        private void AddEventTriggerListener(EventTrigger eventTrigger, EventTriggerType triggerType, Action<BaseEventData> callback)
        {
            var entry = new EventTrigger.Entry();
            entry.eventID = triggerType;
            entry.callback.AddListener((data) => callback.Invoke(data));
            eventTrigger.triggers.Add(entry);
        }
    }
}
