using UnityEngine;
using UnityEngine.UI;

namespace MechJam {
    public class BattleUI : BattleConfigurableBehaviour
    {
        [SerializeField] private Button[] ActionButtons;
        [SerializeField] private Button Back;
        [SerializeField] private CanvasGroup Targets;
        [SerializeField] private Button[] TargetButtons;

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
            }
            _battleController.OnMechWasAttacked += DetermineAttackStatus;
        }

        private void DetermineAttackStatus(Mech arg1, MechPart arg2)
        {
            //TODO
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
    }
}