using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    #region --------------- Variables ---------------

    private ActionsPlayer input;

    [Header("Movements Components")]
    //private Vector2 movementInput   ;
    public  Vector3 direction       ;
    public  float   horizontal      ;
    public  float   vertical        ;
    public  float   moveAmount      ; 
    public  float   distaceAnalogic ;

    public  Vector3 smoothInputMovement;
    private Vector2 value;
    public  float   movementSmoothingSpeed = 1f;

    [Header("Camera Components")]
    [SerializeField]private Camera  mainCamera      ;
                    public  Vector3 cameraForward   ;
                    public  Vector3 cameraRight     ;

    [Header("Actions Inputs")]
    private bool _sprintInput   ;
    private bool _rollingInput  ;
    private bool _attackInput   ;
    private bool _blockInput    ;

    #endregion

    public void OnEnable() 
    {
        if(input == null){   input = new ActionsPlayer();  } 
        input.Enable();
    }   
    public void OnDisable()  => input.Disable() ;
    
    public void Awake()      => mainCamera = Camera.main;

    public void TickInput(float delta) 
    {
        MovementInput(delta);
        SettingsCamera();
        RollingInput();
    }
    public void MovementInput(float delta)
    {
        horizontal = input.PlayerMove.Movement.ReadValue<Vector2>().x;
        vertical   = input.PlayerMove.Movement.ReadValue<Vector2>().y;

        direction           = new Vector3(horizontal,0,vertical);
        smoothInputMovement = Vector3.Lerp(smoothInputMovement, direction, delta * movementSmoothingSpeed);

        distaceAnalogic = Mathf.Clamp(Vector2.Distance(Vector2.zero, new Vector2(horizontal,vertical)),0.7f,1);
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal)+Mathf.Abs(vertical));
    }
    public void SettingsCamera()
    {
        cameraForward = mainCamera.transform.forward ;
        cameraRight   = mainCamera.transform.right   ;
    }
    public bool SprintInput()
    {
        _sprintInput = input.PlayerActions.Sprint.phase == UnityEngine.InputSystem.InputActionPhase.Performed;
        return _sprintInput;
    }
    public bool RollingInput()
    {
        _rollingInput = input.PlayerActions.Rolling.triggered;
        return _rollingInput;
    }
    public bool AttackInput()
    {
        _attackInput = input.PlayerActions.Attack.triggered;
        return _attackInput;
    }
    public bool BlockInput()
    {
        _blockInput = input.PlayerActions.Block.triggered;
        return _blockInput;
    }
}
