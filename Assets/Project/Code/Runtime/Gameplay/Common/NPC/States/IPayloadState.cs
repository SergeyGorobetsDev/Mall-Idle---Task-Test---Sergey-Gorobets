namespace Assets.Project.Code.Runtime.Gameplay.Common.NPC
{
    public interface IPayloadState<TPayload>
    {
        void Enter(TPayload payload);
    }
}