using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachinePlayerController;

public abstract class MovementState : IState
{
    #region --------------- Variables ---------------

    public  PlayerStatesController _playerController;

    [Header("Movement Settings")]
    public  float   _movementSpeed      ;
    public  float   _inSprint    = 1f   ;
    public  int     _forceGravit = 2    ;
    private float   _turnSpeed   = 0.3f ;
    private Vector3 _movementDirection  ;
    
    #endregion

    public void Enter() => SetParameters(); 
    
    public void ExecuteUpdate()      
    {   
        _movementDirection   =  _playerController._InputHandler.smoothInputMovement ;
    }
    public void ExecuteFixedUpdate()
    { 
        MoveThePlayer();
        TurnThePlayer();
    }
    public void Exit(){ }  

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

    public virtual void  SetParameters(){}
 
}
