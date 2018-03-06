using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivaLight
{

    [CreateAssetMenu(menuName = "BotLight/Actions/ReachPosition")]
    public class ReachPositionAction : Action
    {

        public override void Act(StateController controller)
        {
            ReachPosition(controller);
        }

        private void ReachPosition(StateController controller)
        {
            controller.botMovement.Move(Vector3.zero);
        }
    }
}