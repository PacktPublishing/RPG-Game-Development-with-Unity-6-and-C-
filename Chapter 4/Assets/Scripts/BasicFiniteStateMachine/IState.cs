namespace BasicFiniteStateMachine
{
    public interface IState
    {
        void OnEntry();
        void OnUpdate();
        void OnExit();
    }
}