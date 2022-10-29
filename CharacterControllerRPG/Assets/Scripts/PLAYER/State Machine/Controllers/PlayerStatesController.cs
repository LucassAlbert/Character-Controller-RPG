using StateMachinePlayerController;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerStatesController : MonoBehaviour
{
    #region --------------- Variables ---------------

    [field : SerializeField] public  Transform       _MyTransform     {  get; set;   }
    [field : SerializeField] public  Rigidbody       _Rigidbody       {  get; set;   }
    [field : SerializeField] public  InputHandler    _InputHandler    {  get; set;   }
    [field : SerializeField] public  AnimatorHandler _AnimatorHandler {  get; set;   }

    [Header("State Machine")]
    private StateMachine stateMachine = new StateMachine();


    public enum States { Idle, Walk, Rolling, Sprint, Attack, Falling, Block, Dead };
    public States CurrentState{   get; set;   }

    [SerializeField] private Transform feet_pos;
    [SerializeField] private LayerMask ground;
                     private float     _timerDead ;

    [SerializeField] public bool inAction;
    [SerializeField] public bool inGround;
    [SerializeField] public bool inMove;
    [SerializeField] public static bool isDead;

    #endregion

    void Start()
    {
        Application.targetFrameRate = 60;

        _timerDead     = 0.0f ;
        inAction       = false;
        isDead         = false;
        ground         = LayerMask.GetMask("Ground")   ;
        _MyTransform   = transform                     ;

        //Estado Inicial
        stateMachine.ChangeState(new IdleState(this))  ;
        CurrentState   = States.Idle;
    }

    void Update()
    {
        HandlerStates();
        CallDeadState();

        _InputHandler.TickInput(Time.deltaTime);
        stateMachine.Update();

        /* DEBUG */ inGround = inground()                      ;
        /* DEBUG */ inMove   = ( _InputHandler.moveAmount > 0 );
    }
    void FixedUpdate()  => stateMachine.FixedUpdate();

    public void HandlerStates()
    {
        if(!isDead)
        {
            if(inground())
            {
                if(!inAction)
                {
                    ActionsInputs();
                    if(inAction) return;

                    if(inMove)
                    {
                        if(_InputHandler.SprintInput())   
                            if(CurrentState != States.Sprint) stateMachine.ChangeState(SprintState.GetSprintState(this));
                        else
                            if(CurrentState != States.Walk) stateMachine.ChangeState(WalkState.GetWalkState(this));
                    }
                    else
                        if(CurrentState != States.Idle) stateMachine.ChangeState(IdleState.GetIdleState(this));
                }
            }
            else 
                if(CurrentState != States.Falling) stateMachine.ChangeState(FallingState.GetFallingState(this));
        }
        else
            if(CurrentState != States.Dead) stateMachine.ChangeState(DeadState.GetDeadState(this));     
    }

    public bool inground(){   return Physics.CheckBox(feet_pos.position, new Vector3(.15f, .5f, 0.15f), Quaternion.identity, ground); }

    public void ActionsInputs()
    {
        if( _InputHandler.RollingInput())//Botao "X" Rolar
            if(CurrentState != States.Rolling)  stateMachine.ChangeState(new RollingState(this));

        if( _InputHandler.AttackInput())//Botao "B" Atacar
             if(CurrentState != States.Attack)  stateMachine.ChangeState(new AttackState(this));

        if( _InputHandler.BlockInput())//Botao "Y" Bloquear
             if(CurrentState != States.Block)  stateMachine.ChangeState(new BlockState(this));
    }

    private void CallDeadState()
    {
        //Morrer quando cair da plataforma
        if(!inground()) {       _timerDead += Time.deltaTime ;      }
        else            {       _timerDead  = 0              ;      }

        if(_timerDead >= .85f)
            isDead = true;
    }

    #region ---------- Debug ----------
    void OnDrawGizmos()
    {
        if(inground())  {   Gizmos.color = Color.green;   }
        else            {   Gizmos.color = Color.red;     }
        
        //Desenhar colisão que fica de baixo do jogador;
        Gizmos.DrawCube(feet_pos.position, new Vector3(.15f, .5f, 0.15f));
    }
    #endregion
}
