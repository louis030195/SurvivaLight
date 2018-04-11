using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivaLight
{
    [CreateAssetMenu(menuName = "SurvivaLight/Actions/Chase")]
    public class ChaseAction : Action
    {
        public override void Act(StateController controller)
        {
            Chase(controller);
        }

        private void Chase(StateController controller)
        {
            // TODO : When player dies, his GameObject is destroyed, chaseTarget becomes null => exception
            controller.botMovement.navMeshAgent.destination = controller.chaseTarget ? controller.chaseTarget.position : controller.transform.position;
            controller.botMovement.navMeshAgent.isStopped = false;
        }
    }
}