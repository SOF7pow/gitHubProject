using UnityEngine;
using UnityEngine.AI;

namespace _gitProject.logic.Components {
    public class TargetChaser {
        private readonly NavMeshAgent _agent;
        public TargetChaser(NavMeshAgent agent) => _agent = agent;
        public void UpdateTargetPosition(Vector3 position) => _agent.SetDestination(position);
    }   
}
