using Assets.Project.Code.Runtime.Gameplay.Common.NPC;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.Code.Runtime.Gameplay.Common.InteriorSystem
{
    public class InteriorQueuePoint : MonoBehaviour
    {
        [SerializeField]
        private float spacing = 0.25f;

        [SerializeField]
        private List<ActorEntity> queue = new();

        [SerializeField]
        private int inQueueAmount = 0;

        public event Action<ActorEntity> OnActorLeaveQueue;

        private void OnDestroy()
        {
            queue.Clear();
        }

        public void PretendToBeInQueue() => inQueueAmount++;

        public Vector3 GetQueuePosition(int index) =>
            transform.position - transform.forward * spacing * index;

        public Vector3 GetQueuePosition() =>
            (inQueueAmount <= 0) ? transform.position - transform.forward * spacing
                                 : transform.position - transform.forward * spacing * inQueueAmount;

        public void JoinQueue(ActorEntity actor)
        {
            queue.Add(actor);
            Debug.Log("JoinQueue");
        }

        public void LeaveQueue(ActorEntity actor)
        {
            if (queue.Remove(actor))
            {
                inQueueAmount--;
                OnActorLeaveQueue?.Invoke(actor);
                Debug.Log("LeaveQueue");
            }
            else Debug.Log($"Can't remove actor {actor}");
        }

        public bool IsFirstInQueue(ActorEntity actor) =>
            queue.Count > 0 && queue[0] == actor;

        public void OnDrawGizmos()
        {
            for (int i = 0; i < inQueueAmount; i++)
            {
                Vector3 position = GetQueuePosition(i);
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(position, 0.1f);
            }
        }
    }
}