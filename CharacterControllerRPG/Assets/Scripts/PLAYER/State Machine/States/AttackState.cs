using StateMachinePlayerController;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;


public class AttackState : IState
{
    #region --------------- Variables ---------------

    private PlayerStatesController _playerController;
    public AttackState(PlayerStatesController _playerController) => this._playerController = _playerController;

    private Vector3 _movementDirection;
    private int     _forceGravit;
    private float   _timer;

    #endregion

    public void Enter()
    {
        _playerController.CurrentState = PlayerStatesController.States.Attack;
        Debug.Log(_playerController.CurrentState);

        _timer = 0.0f;
        _playerController.inAction = true;
        _playerController._AnimatorHandler.setAnimator(6); 

        //Controle de Direcao Obs = talvez por em enter 
        _movementDirection   = _playerController._InputHandler.smoothInputMovement;
    }
    
    public void ExecuteUpdate()
    { 
        Timer(1.25f);

        //Controlle de Gravidade
        _forceGravit = (_playerController.inground()) ? 2 : 12;
    } 
    public void ExecuteFixedUpdate(){   if(_timer > .1f && _timer < 0.75f) MoveThePlayer();     }  
    public void Exit(){ /* Content */ }

    void MoveThePlayer()
    {
        _playerController._Rigidbody.AddForce(Physics.gravity * _forceGravit, ForceMode.Acceleration);

        Vector3 movement = _playerController._MyTransform.forward * 35 * Time.deltaTime;
        _playerController._Rigidbody.AddForce(movement,ForceMode.VelocityChange);     
    }

    void Timer(float Value)
    {
        _timer += Time.deltaTime;

        if(_timer > Value) 
            _playerController.inAction = false; 
    }
}
