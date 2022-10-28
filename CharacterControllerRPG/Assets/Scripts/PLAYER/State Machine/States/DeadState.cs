using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachinePlayerController;


public class DeadState : IState
{
    private PlayerStatesController _playerController;
    public DeadState(PlayerStatesController _playerController) => this._playerController = _playerController;
    
    public static DeadState Instance ;

    public static DeadState GetDeadState(PlayerStatesController _playerController)
    {
        if(Instance == null){   return new DeadState(_playerController);    } else {    return Instance;    }
    }

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
