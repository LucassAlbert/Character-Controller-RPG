public class WalkState : MovementState
{
    public  WalkState(PlayerStatesController _playerController) => this._playerController = _playerController;

    public static WalkState Instance ;

    public static WalkState GetWalkState(PlayerStatesController _playerController)
    {
        if(Instance == null){   return new WalkState(_playerController);    } else {    return Instance;    }
    }

    public override void SetParameters()
	{
        _inSprint       = 1;
        _forceGravit    = 2;
        _movementSpeed  =  _inSprint * 50;
        _playerController._AnimatorHandler.setAnimator(1); 
        _playerController.CurrentState = PlayerStatesController.States.Walk;
	}
}
