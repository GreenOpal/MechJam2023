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
                Button button = ActionButtons[i];
                button.onClick.AddListener(() => Debug.LogWarning($"Pressed button {i}"));
            }
        }
    }
}