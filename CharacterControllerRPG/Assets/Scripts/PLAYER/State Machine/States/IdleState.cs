using StateMachinePlayerController;

public class IdleState : IState
{
    #region --------------- Variables ---------------
    private PlayerStatesController _playerController;
    public IdleState(PlayerStatesController _playerController) => this._playerController = _playerController;
    #endregion

    public void Enter()
    {
        _playerController.CurrentState = PlayerStatesController.States.Idle;
        _playerController._AnimatorHandler.setAnimator(0);
    }
    
    public void ExecuteUpdate()      { /* Content */ }
    public void ExecuteFixedUpdate() { /* Content */ }
    public void Exit()               { /* Content */ }
}

