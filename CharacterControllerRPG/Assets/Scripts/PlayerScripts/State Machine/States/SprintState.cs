public class SprintState : MovementState
{
    public  SprintState(PlayerStatesController _playerController) => this._playerController = _playerController;

    public static SprintState Instance ;

    public static SprintState GetSprintState(PlayerStatesController _playerController)
    {
        if(Instance == null){   return new SprintState(_playerController);    } else {    return Instance;    }
    }

    public override void SetParameters()
	{
        _inSprint       = 1.55f           ;
        _forceGravit    = 2               ;
        _movementSpeed  =  _inSprint * 50 ;
        _playerController._AnimatorHandler.setAnimator(2); 
        _playerController.CurrentState = PlayerStatesController.States.Sprint;
	}
}

