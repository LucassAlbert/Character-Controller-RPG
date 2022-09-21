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
                             //private GameController   gameController;

    [Header("State Machine")]
    private StateMachine stateMachine = new StateMachine();


    public enum States { Idle, Walk, Rolling, Backstep, Sprint, Attack, Falling, Block, Dead };
    public States CurrentState{   get; set;   }

    [SerializeField] private Transform feet_pos;
    [SerializeField] private LayerMask ground;
    private float _timerDead ;
    private bool  auxContador;

    [SerializeField] public bool inAction;
    [SerializeField] public bool inGround;
    [SerializeField] public bool inMove;
    [SerializeField] public static bool isDead;

    [Header("Controller Sword")]
    [SerializeField] private GameObject Sword;
    [SerializeField] private GameObject Sheath;
    [SerializeField] private ParticleSystem _swordParticleEffect;

    #endregion


    void Start()
    {
        _timerDead     = 0.0f ;
        auxContador    = true ;
        inAction       = false;
        isDead         = false;
        //gameController = GameController.gameController ;
        CurrentState   = States.Idle                   ;
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
        if(!isDead)//Nao esta morto
        {
            if(inground())//Está no Chao
            {
                if(!inAction)//Não está fazendo nenhuma acão atomica
                {
                    ActionsInputs();

                    if(inAction) return;//Verificar inAction depois dos inputs;

                    if(inMove)//Está se movimentando
                    {
                        if(CurrentState != States.Walk && CurrentState != States.Sprint)
                            stateMachine.ChangeState(new MovementState(this));
                    }
                    else
                    {
                        if(CurrentState != States.Idle) stateMachine.ChangeState(new IdleState(this));
                    }
                }
            }
            else //Não está no Chao
            {
                if(CurrentState != States.Falling) stateMachine.ChangeState(new MovementState(this));
            }
        }
        else//Esta morto
        {
            //_AnimatorHandler.ChangeAnimationState("Dead");
            if(CurrentState != States.Dead) stateMachine.ChangeState(new DeadState(this));
        }
    }

    public bool inground(){   return Physics.CheckBox(feet_pos.position, new Vector3(.15f, .5f, 0.15f), Quaternion.identity, ground); }

    // public void ControllerSword(bool value)
    // {
    //     Sword.SetActive(value)   ; //Ativar Espada da Mao
    //     Sheath.SetActive(!value) ; //Desativar Espada da Bainha
    // }

    //public void ParticleSwordEffect()   =>  _swordParticleEffect.Play();

    //public bool  DustActive()       {   return (CurrentState == States.Walk || CurrentState == States.Sprint);       }

    //public float getValueMovement() {   return  100 + 3 * gameController.getSkill(0);                                }

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
        {
            if(auxContador)
            {
                //gameController.addMorte();
                auxContador = false;
            }
            //FadeController.controle = 1;
        }
    }

    #region ---------- Debug ----------
    void OnDrawGizmos()//desenha o inground(box de colisao)
    {
        if(inground())  {   Gizmos.color = Color.green;   }
        else            {   Gizmos.color = Color.red;     }
        
        //Desenhar colisão que fica de baixo do jogador;
        Gizmos.DrawCube(feet_pos.position, new Vector3(.15f, .5f, 0.15f));
    }
    #endregion
}
