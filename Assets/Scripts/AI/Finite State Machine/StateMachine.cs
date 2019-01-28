using UnityEngine;

public class StateMachine<EntityType>
{
    // Holds the owner of this state machine
    private EntityType owner;

    // Current state of this state machine's owner
    private State<EntityType> currentState;

    // Previous state of this state machine's owner
    private State<EntityType> previousState;

    // Global state of this state machine's owner
    // This state is called every frame
    private State<EntityType> globalState;

    public StateMachine(EntityType owner)
    {
        this.owner = owner;
        currentState = null;
        currentState = null;
        currentState = null;
    }

    // Execute the global state and current state (every frame)
    public void Update()
    {
        if(!SessionManager.Instance.IsGameStarted || SessionManager.Instance.IsGamePaused)
        {
            return;
        }

        if (globalState != null)
            globalState.Execute(owner);

        if (currentState != null)
        {
            currentState.Execute(owner);
            currentState.FixedExecute(owner);
        }
    }

    public void ChangeState(State<EntityType> newState)
    {
        if (newState == null)
        {
            Debug.LogError("New state is null !");
            return;
        }

        // Keep a copy of the current state in case we want to switch back to it
        previousState = currentState;

        // Call the exit method of the current state 
        currentState.Exit(owner);

        // Change the current state to the new state
        currentState = newState;

        // Call the Enter method of the new state
        currentState.Enter(owner);
    }

    // Used to revert the state machine to the previous state
    public void RevertToPreviousState()
    {
        ChangeState(previousState);
    }

    public void SetCurrentState(State<EntityType> s) { currentState = s; }
    public void SetPreviousState(State<EntityType> s) { previousState = s; }
    public void SetGlobalState(State<EntityType> s) { globalState = s; }

    public State<EntityType> GetCurrentState() { return currentState; }
    public State<EntityType> GetPreviousState() { return previousState; }
    public State<EntityType> GetGlobalState() { return globalState; }
}
