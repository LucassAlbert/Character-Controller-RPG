public class FallingState : MovementState
{
    public  FallingState(PlayerStatesController _playerController) => this._playerController = _playerController;

    public static FallingState Instance ;

    public static FallingState GetFallingState(PlayerStatesController _playerController)
    {
        if(Instance == null){   return new FallingState(_playerController);    } else {    return Instance;    }
    }

    public override void SetParameters()
	{
        _inSprint       = 1.0f            ;
        _forceGravit    = 12              ;
        _movementSpeed  =  _inSprint * 50 ;
        _playerController._AnimatorHandler.setAnimator(4); 
        _playerController.CurrentState = PlayerStatesController.States.Falling;
	}
}
