using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;


namespace MechJam
{
    public abstract class GameControllerBase : MonoBehaviour
    {
        #region Fields

        public GameConfig Config => _config;

        [SerializeField] private GameConfig _config;

        private ConfigurableBehaviour[] _configurables;


        #endregion

        #region Actions

        public Action<bool> OnGameStarted;
        public Action<bool> OnGameFinished;


        #endregion

        #region Implementation

        private void Awake()
        {
            _configurables = GetComponentsInChildren<ConfigurableBehaviour>();
            Init();
        }


        private void Init()
        {
            foreach (var behaviour in _configurables)
            {
                behaviour.Initialize(this);
            }
            StartGame();
        }

        #endregion

        #region Methods



        public void StartGame()
        {
            foreach (var behaviour in _configurables)
            {
                behaviour.StartGame();
            }
        }

        public void FinishGame(bool endEarly)
        {
            OnGameFinished?.Invoke(endEarly);
            Cleanup();
        }

        private void Cleanup()
        {
            foreach (var behaviour in _configurables)
            {
                behaviour.FinishGame();
            }
        }


        public void Quit()
        {
            foreach (var behaviour in _configurables)
            {
                behaviour.DeInitialize();
            }
        }

        public void ResetGame()
        {
        }

        #endregion

    }


}
