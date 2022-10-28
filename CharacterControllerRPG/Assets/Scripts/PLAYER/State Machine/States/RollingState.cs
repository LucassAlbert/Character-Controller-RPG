using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachinePlayerController;

public class RollingState : IState
{
    #region --------------- Variables ---------------

    private PlayerStatesController _playerController;
    public RollingState(PlayerStatesController _playerController) => this._playerController = _playerController;

    private Vector3 _movementDirection;
    private int forceGravit;

    private float _timer;
    
    #endregion

    public void Enter()
    {
        _playerController.CurrentState = PlayerStatesController.States.Rolling;
        Debug.Log(_playerController.CurrentState);

        _playerController.inAction = true;
        _timer = 0.0f;
        _playerController._AnimatorHandler.setAnimator(5); 

        //Controle de Direcao 
        _movementDirection   = _playerController._InputHandler.smoothInputMovement;
    }
    
    public void ExecuteUpdate()
    { 
        Timer(.9f);
        //Controlle de Gravidade
        //forceGravit = (_playerController.inground()) ? 2 : 12;
    }      
    
    public void ExecuteFixedUpdate(){   if(_timer > 0.15f && _timer < 0.75f ) MoveThePlayer();     }    
   
    public void Exit() { _playerController.inAction = false; }

    void MoveThePlayer()
    {
        _playerController._Rigidbody.AddForce(Physics.gravity * 2, ForceMode.Acceleration);

        Vector3 movement = _playerController._MyTransform.forward * 70 * Time.deltaTime;
        _playerController._Rigidbody.AddForce(movement,ForceMode.VelocityChange);     
    }

    void Timer(float Value)
    {
        _timer += Time.deltaTime;
        if(_timer > Value) _playerController.inAction = false;
    }
   
}
