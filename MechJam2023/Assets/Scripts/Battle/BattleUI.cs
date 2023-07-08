using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MechJam {
    public class BattleUI : ConfigurableBehaviour
    {
        [SerializeField] private Button[] ActionButtons;

        public override void Initialize(GameControllerBase controller)
        {
            base.Initialize(controller);
            for (int i = 0; i < ActionButtons.Length; i++)
            {
                var currentButton = i;
                ActionButtons[i].onClick.AddListener( () => UseAttackButton(currentButton) );
            }
        }
        public override void DeInitialize()
        {
            base.DeInitialize();
            foreach (var button in ActionButtons)
            {
                button.onClick.RemoveAllListeners();
            }
        }

        private void UseAttackButton( int i )
        {
            Debug.LogWarning($"Pressed button {i}");
        }
    }
}