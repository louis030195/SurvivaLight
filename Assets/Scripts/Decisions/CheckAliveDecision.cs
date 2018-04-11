using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SurvivaLight
{
    /// <summary>
    /// This decision is used in this tutorial to check if the target is dead or not
    /// Useful to check if eatable or food has already been eaten ...
    /// Or finished eating
    /// https://unity3d.com/fr/learn/tutorials/topics/navigation/chasing-until-target-dead?playlist=17105
    /// </summary>
    [CreateAssetMenu(menuName = "SurvivaLight/Decisions/CheckAlive")]
    public class CheckAliveDecision : Decision
    {
        public override bool Decide(StateController controller)
        {
            bool targetIsActive = controller.chaseTarget ? !controller.chaseTarget.GetComponent<BotHealth>().dead : false;
            return targetIsActive;
        }

    }
}