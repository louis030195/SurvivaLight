using UnityEngine;
using UnityEngine.UI;

namespace SurvivaLight
{
    public class BotAttack : MonoBehaviour
    {
        public int damage = 10;
        private bool attacked;
        private float nextAttackTime; // kind of attack speed thing, useful
        public AudioSource attackAudio;               // The audio source to play.
        public AudioClip attack;            // Audio to play when the bot attack.


        public void Attack(float attackRate, RaycastHit hit) // attackSpeed ?
        {
            if (hit.rigidbody)
            {
                BotHealth target = hit.rigidbody.GetComponent<BotHealth>();
                // Debug.Log("BotAttack : " + Time.time + " | " + nextAttackTime + " | " + target);
                // Find the BotHealth script associated with the rigidbody.
                if (target)
                {

                    if (Time.time > nextAttackTime && target.isActiveAndEnabled)
                    {
                        nextAttackTime = Time.time + attackRate;
                        // Set the fired flag so only Fire is only called once.
                        attacked = true;
                        Audio();


                        if (target.getHealth() <= 0)
                            return;

                        target.TakeDamage(damage);

                    }
                }
            }

        }

        private void Audio()
        {
            if (attackAudio)
            {
                attackAudio.clip = attack;
                attackAudio.Play();
            }
        }
    }
}