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

            _battleController.OnMechWasAttacked += DetermineAttackStatus;
        }


        private void DetermineAttackStatus(Mech mech, MechPart part)
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
            ActionButtons[i].Select();
            selectedWeapon = i + 1;
            Targets.alpha = 1;
            Targets.interactable = true;
            Targets.blocksRaycasts = true;
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
            Targets.interactable = false;
            Targets.blocksRaycasts = false;
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
            int index = mech.IsPlayer ? button.transform.GetSiblingIndex() : button.transform.GetSiblingIndex() - 1;

            MechPart part;
            if (mech.PartMap.TryGetValue((Mech.AttackPart)index, out part))
            {
                MechPartHPBar.transform.parent.gameObject.SetActive(true);
                MechPartHPBar.gameObject.SetActive(true);
                MechPartHPBar.fillAmount = (float)part.Durability / part.MaxDurability;
                MechPartStatsText.text = $"{(mech.IsPlayer ? "Player" : "Enemy")} Attack: {part.Attack}\n{(mech.IsPlayer ? "Player" : "Enemy")} Defense: {part.Defense}";
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
