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
            InitNavMeshAgentComponent();
        }

        private void Update()
        {
            if (stateMachine == null || agent == null)
                return;
            stateMachine.Update();
        }

        private void InitNavMeshAgentComponent()
        {
            if (this.agent != null) return;

            if (TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))
            {
                this.agent = agent;
                this.agent.speed = actorData.MovementSpeed;
                this.agent.angularSpeed = actorData.RotationSpeed;
                this.agent.enabled = false;
            }
        }

        public void Initialize()
        {
            stateMachine ??= new();
            stateMachine.Initialize(this, agent);
            stateMachine.SetState<SpawnState>();
        }
    }
}