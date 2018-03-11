﻿using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace SurvivaLight
{
    public class BotHealth : MonoBehaviour
    {

        public float startingHealth = 100f;               // The amount of health each bot starts with.
        public Slider slider;                             // The slider to represent how much health the bot currently has.
        public Image fillImage;                           // The image component of the slider.
        public Color fullHealthColor = Color.green;       // The color the health bar will be when on full health.
        public Color zeroHealthColor = Color.red;         // The color the health bar will be when on no health.
        
        private float currentHealth;                      // How much health the bot currently has.
        [HideInInspector]public bool dead;                                // Has the bot been reduced beyond zero health yet?
        public AudioSource healthAudio;               // The audio source to play.
        public AudioClip gettingHit;            // Audio to play when the bot is getting hit.
        public AudioClip dying;           // Audio to play when the bot is dying.


        public float getHealth() // can get, not set
        {
            return currentHealth;
        }

        private void Awake()
        {
            
        }


        private void OnEnable()
        {
            currentHealth = startingHealth;
            dead = false;

            // Update the health slider's value and color.
            SetHealthUI();
        }

        private void Audio()
        {
            if (healthAudio)
            {
                if (!dead)
                    healthAudio.clip = gettingHit;
                else
                    healthAudio.clip = dying;

                if (!healthAudio.isPlaying)
                {
                    healthAudio.Play();
                }
            }
        }


        public void TakeDamage(float amount)
        {
            
            // Reduce current health by the amount of damage done.
            currentHealth -= amount;
            // Change the UI elements appropriately.
            SetHealthUI();

            // If the current health is at or below zero and it has not yet been registered, call OnDeath.
            if (currentHealth <= 0f && !dead)
            {
                OnDeath();
            }
            Audio();
        }


        private void SetHealthUI()
        {
            // Set the slider's value appropriately.
            slider.value = currentHealth;

            // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
            fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, currentHealth / startingHealth);
        }


        private void OnDeath()
        {
            // Set the flag so that this function is only called once.
            dead = true;
            
            Destroy(gameObject);
            //if (gameObject.GetComponent<NavMeshAgent>())
            //    gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            //else
            //    gameObject.GetComponent<PlayerManager>().control = false;
        }
    }
}