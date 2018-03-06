using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SurvivaLight
{
    [CreateAssetMenu(menuName = "SurvivaLight/Decisions/LookNonStatic")]
    public class LookNonStaticDecision : Decision
    {

        public override bool Decide(StateController controller)
        {
            bool animalVisible = Look(controller);
            return animalVisible;
        }

        private bool Look(StateController controller)
        {
            Debug.DrawRay(controller.eyes.position, controller.eyes.forward.normalized * controller.aiParameters.lookRange, Color.green);
            


            RaycastHit hit;


            // SphereCast is used to detect moving object, so prob only for carnivorous animals
            // only detect collider with Eatable layer => huge optimisation
            // TODO : Change to overlapsphere (check all direction around)
            if (Physics.SphereCast(controller.eyes.position, controller.aiParameters.lookSphereCastRadius, controller.eyes.forward, out hit, controller.aiParameters.lookRange, LayerMask.GetMask("Eatable")) && hit.rigidbody.tag != "StaticEatable")
            {
                controller.chaseTarget = hit.transform;
                return true;
            }
            

            return false; // looked through all hit colliders but none was an eatable

        }
    }
}