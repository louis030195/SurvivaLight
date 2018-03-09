using UnityEngine;

namespace SurvivaLight
{
    public class Bullet : MonoBehaviour
    {
        public ParticleSystem explosionParticles;         // Reference to the particles that will play on explosion.
        public AudioSource explosionAudio;                // Reference to the audio that will play on explosion.
        public float maxLifeTime = 2f;                    // The time in seconds before the shell is removed.


        private void Start()
        {
            // If it isn't destroyed by then, destroy the shell after it's lifetime.
            Destroy(gameObject, maxLifeTime);
        }


        private void OnTriggerEnter(Collider other)
        {
            // Find the BotHealth script associated with the rigidbody.
            BotHealth targetHealth = other.GetComponent<BotHealth>();

            // If there is no BotHealth script attached to the gameobject
            if (targetHealth)
                targetHealth.TakeDamage(5); // Deal this damage to the AI.
            /*
            // Unparent the particles from the bullet.
            explosionParticles.transform.parent = null;

            // Play the particle system.
            explosionParticles.Play();

            // Play the explosion sound effect.
            explosionAudio.Play();

            // Once the particles have finished, destroy the gameobject they are on.
            ParticleSystem.MainModule mainModule = explosionParticles.main;
            Destroy(explosionParticles.gameObject, mainModule.duration);
            */
            // Destroy the shell.
            Destroy(gameObject);
        }
    }
}