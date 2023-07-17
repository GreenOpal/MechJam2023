using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;

namespace MechJam {
    public class BattleUI : BattleConfigurableBehaviour
    {
        [SerializeField] private Button[] ActionButtons;
        [SerializeField] private Button Back;
        [SerializeField] private CanvasGroup Targets;
        [SerializeField] private Button[] TargetButtons;

        [SerializeField] public Image PlayerHPBar;
        [SerializeField] public Image EnemyHPBar;
        [SerializeField] public Image MechPartHPBar;
        [SerializeField] private TextMeshProUGUI MechPartStatsText;

        private int selectedWeapon;
        private int selectedTarget;

        public override void Initialize(GameControllerBase controller)
        {
            base.Initialize(controller);
            for (int i = 0; i < ActionButtons.Length; i++)
            {
                var currentButton = i;
                ActionButtons[i].onClick.AddListener( () => UseAttackButton(currentButton) );
            }
            for (int i = 0; i < TargetButtons.Length; i++)
            {
                var currentButton = i;
                TargetButtons[i].onClick.AddListener(() => UseTargetButton(currentButton));
                var eventTrigger = TargetButtons[i].gameObject.AddComponent<EventTrigger>();
                AddEventTriggerListener(eventTrigger, EventTriggerType.PointerEnter, (data) => ShowMechPartStats(TargetButtons[currentButton]));
                AddEventTriggerListener(eventTrigger, EventTriggerType.PointerExit, HideMechPartStats);
            }
            _battleController.OnMechWasAttacked += DetermineAttackStatus;
        }

        private void DetermineAttackStatus(Mech mech, MechPart part)
        {
            UpdatePlayerHealth();
        }

        private void UpdatePlayerHealth()
        {
            PlayerHPBar.fillAmount = _battleController.PlayerMech.CurrentHealth;
            EnemyHPBar.fillAmount = _battleController.EnemyMech.CurrentHealth;
        }

        private void UseAttackButton( int i )
        {
            Debug.LogWarning($"Selected {System.Enum.GetName(typeof(Mech.AttackPart), i + 1)}");
            ActionButtons[i].Select();
            selectedWeapon = i + 1;
            Targets.alpha = 1;
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
                //TODO: UI Tooltip kinda element hopefully
            }
        }

        private void OnBack()
        {
            selectedWeapon = 0;
            selectedTarget = 0;
            Targets.alpha = 0;
        }

        private void StartAttack()
        {
            _battleController.PlayerMech.Attack((Mech.AttackPart)selectedWeapon, _battleController.EnemyMech, (Mech.AttackPart)selectedTarget);
            OnBack();
        }

        private void ShowMechPartStats(Button button)
        {
            MechPart part;
            if (_battleController.PlayerMech.IsPlayer)
            {
                part = _battleController.PlayerMech.PartMap[(Mech.AttackPart)(button.transform.GetSiblingIndex() - 1)];
            }
            else
            {
                part = _battleController.EnemyMech.PartMap[(Mech.AttackPart)(button.transform.GetSiblingIndex() - 1)];
            }

            MechPartHPBar.transform.parent.gameObject.SetActive(true);
            MechPartHPBar.gameObject.SetActive(true);
            MechPartHPBar.fillAmount = (float)part.Durability / part.MaxDurability;
            MechPartStatsText.text = $"Attack: {part.Attack}\nDefense: {part.Defense}";
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
