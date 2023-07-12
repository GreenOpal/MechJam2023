namespace MechJam
{
    public class BattleConfigurableBehaviour : ConfigurableBehaviour
    {
        protected BattleController _battleController;
        public override void Initialize(GameControllerBase controller)
        {
            base.Initialize(controller);
            _battleController = (BattleController)controller;
        }
    }
}
