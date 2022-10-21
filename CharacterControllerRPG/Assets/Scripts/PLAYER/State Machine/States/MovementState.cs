using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachinePlayerController;

public abstract class MovementState : IState
{
    #region --------------- Variables ---------------

    public  PlayerStatesController _playerController;

    [Header("Movement Settings")]
    public  float   _movementSpeed     ;
    public  float   _inSprint  = 1f    ;
    public  int     _forceGravit = 2   ;
    private float   _turnSpeed = 0.3f  ;
    private Vector3 _movementDirection  ;
    
    #endregion

    public void Enter() => SetParameters(); //ChangeState();
    
    public void ExecuteUpdate()      
    {   
        //ChangeState();
        _movementDirection   =  _playerController._InputHandler.smoothInputMovement ;
    }
    public void ExecuteFixedUpdate()
    { 
        MoveThePlayer();
        TurnThePlayer();
    }
    public void Exit() { /* Content */ }  

    void MoveThePlayer()
    {   
        _playerController._Rigidbody.AddForce(Physics.gravity * _forceGravit, ForceMode.Acceleration);

        Vector3 movement = CameraDirection(_movementDirection) * _movementSpeed * Time.deltaTime;
        _playerController._Rigidbody.AddForce(movement,ForceMode.VelocityChange);     
    }
    void TurnThePlayer()
    {
        Quaternion rotation = Quaternion.Slerp(_playerController._Rigidbody.rotation, Quaternion.LookRotation (CameraDirection(_movementDirection)),_turnSpeed);
        _playerController._Rigidbody.MoveRotation(rotation);  
    }

    Vector3 CameraDirection(Vector3 movementDirection)
    {
        var camForward =  _playerController._InputHandler.cameraForward;
        var camRight   =  _playerController._InputHandler.cameraRight;

        camForward.y  = 0f;
        camRight.y    = 0f;
        
        return camForward * movementDirection.z + camRight * movementDirection.x; 
    } 

    public virtual void  SetParameters(){   /* ... */   }

    // void ChangeState()
    // {
    //     if (_playerController.inground())//Está no chao
    //     {   
    //         _forceGravit = 2 ;
    //         if(_playerController._InputHandler.SprintInput())//Está Pressionando o Botao de Correr
    //         {
    //            if(_playerController.CurrentState != PlayerStatesController.States.Sprint)
    //            {
    //               _inSprint = 1.55f;
    //               _playerController._AnimatorHandler.setAnimator(2); 
    //               _playerController.CurrentState = PlayerStatesController.States.Sprint;
    //               //Debug.Log(_playerController.CurrentState);
    //            } 
    //         }
    //         else//Não está Pressonando o Botao de Correr
    //         {
    //             if(_playerController.CurrentState != PlayerStatesController.States.Walk)
    //            {
    //               _inSprint = 1f;
    //               _playerController._AnimatorHandler.setAnimator(1); 
    //               _playerController.CurrentState = PlayerStatesController.States.Walk;
    //               //Debug.Log(_playerController.CurrentState);
    //            } 
    //         }  
    //     }
    //     else//Não está no chao
    //     {   
    //         _forceGravit = 12 ;
    //         if(_playerController.CurrentState != PlayerStatesController.States.Falling)
    //         {
    //           _playerController.inAction     = false ;
    //           _playerController.CurrentState = PlayerStatesController.States.Falling;
    //           _playerController._AnimatorHandler.setAnimator(4);  
    //           //Debug.Log(_playerController.CurrentState);
    //         }
    //     }
    // } 
}
