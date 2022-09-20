using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachinePlayerController;


public class AttackState : IState
{
    #region --------------- Variables ---------------

    private PlayerStatesController _playerController;
    public AttackState(PlayerStatesController _playerController) => this._playerController = _playerController;

    private Vector3 _movementDirection;
    private int forceGravit;

    private float _timer;

    #endregion


    public void Enter()
    {
        _playerController.CurrentState = PlayerStatesController.States.Attack;
        Debug.Log(_playerController.CurrentState);

        _timer = 0.0f;
        _playerController.inAction = true;
        _playerController._AnimatorHandler.setAnimator(6); 
        _playerController.ControllerSword(true);
        //_playerController.ParticleSwordEffect();
        _playerController.Invoke("ParticleSwordEffect",.35f);

        //Controle de Direcao Obs = talvez por em enter 
        _movementDirection   = _playerController._InputHandler.smoothInputMovement;
    }
    
    public void ExecuteUpdate()
    { 
        Timer(.74f);
        
        //Controlle de Gravidade
        forceGravit = (_playerController.inground()) ? 2 : 12;
    } 
    public void ExecuteFixedUpdate(){   if(_timer > .1f && _timer < .6f) MoveThePlayer();     }  
    public void Exit()              
    {   
        //_playerController._AnimatorHandler.ChangeAnimationState("Idle");
        _playerController.ControllerSword(false);
    }

    void MoveThePlayer()
    {
        _playerController._Rigidbody.AddForce(Physics.gravity * forceGravit, ForceMode.Acceleration);

        Vector3 movement = _playerController._MyTransform.forward * 20 * Time.deltaTime;
        _playerController._Rigidbody.AddForce(movement,ForceMode.VelocityChange);     
    }

    void Timer(float Value)
    {
        _timer += Time.deltaTime;
        if(_timer > Value) 
        {
            _playerController.inAction = false;
            //_playerController._AnimatorHandler.ChangeAnimationState("Attack");
        }
    }
}
