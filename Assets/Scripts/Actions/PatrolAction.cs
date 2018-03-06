using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivaLight
{

    [CreateAssetMenu(menuName = "SurvivaLight/Actions/Patrol")]
    public class PatrolAction : Action
    {

        public override void Act(StateController controller)
        {
            Patrol(controller);
        }

        private void Patrol(StateController controller)
        {
            controller.botMovement.MoveRandom(20);
        }
    }
}