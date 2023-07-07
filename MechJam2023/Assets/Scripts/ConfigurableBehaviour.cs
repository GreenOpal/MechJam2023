using UnityEngine;


namespace MechJam
{
    public abstract class ConfigurableBehaviour : MonoBehaviour
    {
        #region Fields
        protected GameController _controller;
        protected bool IsPlaying;
        #endregion

        #region Methods
        public virtual void Initialize(GameController controller)
        {            
            _controller = controller;
        } 
        public virtual void DeInitialize() { }

        public virtual void StartGame()
        {
            IsPlaying = true;
        }

        public virtual void FinishGame()
        {
            IsPlaying = false;
        }
        #endregion

    }
}