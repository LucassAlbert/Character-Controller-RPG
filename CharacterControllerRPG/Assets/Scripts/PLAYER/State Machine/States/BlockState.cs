using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachinePlayerController;


public class BlockState : IState
{
    private PlayerStatesController _playerController;
    public BlockState(PlayerStatesController _playerController) => this._playerController = _playerController;

    private float _timer;

    public static BlockState Instance ;

    public static BlockState GetBlockState(PlayerStatesController _playerController)
    {
        if(Instance == null){   return new BlockState(_playerController);    } else {    return Instance;    }
    }

    public void Enter()
    {
        _playerController.CurrentState = PlayerStatesController.States.Block;
        Debug.Log(_playerController.CurrentState);

        _timer = 0.0f;
        _playerController.inAction = true;
        _playerController._AnimatorHandler.setAnimator(3);
    }
    
    public void ExecuteUpdate()  =>    Timer(.75f);
    public void ExecuteFixedUpdate() { /* Content */ }
    public void Exit()               { /* Content */ }

    void Timer(float Value)
    {
        _timer += Time.deltaTime;
        if(_timer > Value) _playerController.inAction = false;
    }
}
