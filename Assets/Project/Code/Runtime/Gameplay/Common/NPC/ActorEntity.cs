using UnityEngine;
using UnityEngine.AI;

namespace Assets.Project.Code.Runtime.Gameplay.Common.NPC
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class ActorEntity : MonoBehaviour
    {
        [Header("Configs")]
        [SerializeField]
        private ActorData actorData;

        [Space()]
        [Header("Components")]
        [SerializeField]
        private NavMeshAgent agent;
        [SerializeField]
        private StateMachine stateMachine;

        private void Awake()
        {
            if (agent == null)
            {
                if (TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))
                {
                    this.agent = agent;
                    agent.speed = actorData.MovementSpeed;
                    agent.angularSpeed = actorData.RotationSpeed;
                }
            }
        }

        private void Update()
        {
            if (stateMachine == null || agent == null)
                return;
            stateMachine.Update();
        }

        public void Initialize()
        {
            stateMachine ??= new();
            stateMachine.Initialize(this, agent);
            stateMachine.SetState<GetFoodState>();
        }
    }
}