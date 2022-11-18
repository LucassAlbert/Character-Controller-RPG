namespace StateMachinePlayerController
{
    public interface IState
    {
        public void Enter();
        public void ExecuteUpdate();
        public void ExecuteFixedUpdate();
        public void Exit();
    }
    public class StateMachine
    {
        IState currentState;
    
        public void ChangeState(IState newState)
        {
            if (currentState != null){    currentState.Exit();    }

            currentState = newState;
            currentState.Enter();
        }
    
        public void Update()      {       if(currentState != null) currentState.ExecuteUpdate();        } 
        public void FixedUpdate() {       if(currentState != null) currentState.ExecuteFixedUpdate();   }
            
    }
}