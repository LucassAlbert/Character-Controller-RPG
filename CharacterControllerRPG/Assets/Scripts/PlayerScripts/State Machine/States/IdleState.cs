using StateMachinePlayerController;

public class IdleState : IState
{
    private PlayerStatesController _playerController;
    public IdleState(PlayerStatesController _playerController) => this._playerController = _playerController;

    public static IdleState Instance ;

    public static IdleState GetIdleState(PlayerStatesController _playerController)
    {
        if(Instance == null){   return new IdleState(_playerController);    } else {    return Instance;    }
    }

    public void Enter()
    {
        _playerController.CurrentState = PlayerStatesController.States.Idle;
        _playerController._AnimatorHandler.setAnimator(0);
    }
    
    public void ExecuteUpdate()      { /* Content */ }
    public void ExecuteFixedUpdate() { /* Content */ }
    public void Exit()               { /* Content */ }
}

