using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachinePlayerController;


public class DeadState : IState
{
    #region --------------- Variables ---------------

    private PlayerStatesController _playerController;
    public DeadState(PlayerStatesController _playerController) => this._playerController = _playerController;
    
    #endregion

    public void Enter()
    {
        _playerController.CurrentState = PlayerStatesController.States.Dead;
        Debug.Log(_playerController.CurrentState);

        _playerController._AnimatorHandler.ChangeAnimationState("Dead");
    }
    public void ExecuteUpdate()      { /* Content */ }
    public void ExecuteFixedUpdate() { /* Content */ }
    public void Exit()               { /* Content */ }
}
