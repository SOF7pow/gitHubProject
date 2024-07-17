using UnityEngine;
using UnityEngine.AI;

namespace _gitProject.logic.Components {
    public class PositionFollower {
        
        private readonly NavMeshAgent _agent;
        public PositionFollower(NavMeshAgent agent) => _agent = agent;
        public void UpdateTargetPosition(Vector3 position) => _agent.SetDestination(position);
    }   
}
