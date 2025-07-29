using UnityEngine;

namespace Assets.Project.Code.Runtime.Gameplay.Common.NPC
{
    [CreateAssetMenu(fileName = "ActorsDataContainer", menuName = "Game / Actors / Actors Data Container", order = 2)]
    public sealed class ActorsDataContainer : ScriptableObject
    {
        [SerializeField]
        private ActorData[] actorsData;

        public ActorData[] ActorsData => actorsData;

        public ActorData GetActorDataById(int id)
        {
            foreach (var actor in actorsData)
            {
                if (actor.Id == id)
                    return actor;
            }
            return null;
        }
    }
}