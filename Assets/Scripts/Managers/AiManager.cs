﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SurvivaLight
{
    /// <summary>
    /// Could be used to handle enabling / disabling bots
    /// </summary>
    [Serializable]
    public class AiManager
    {
        
        [HideInInspector] public GameObject instance;         // A reference to the instance of the bot when it is created.
        private StateController stateController;              // Reference to the StateController for AI
        private BotAttack botAttack;
        private BotHealth botHealth;
        private BotMovement botMovement;

        public void SetupAI()
        {
            
            botAttack = instance.GetComponent<BotAttack>();

            botMovement = instance.GetComponent<BotMovement>();

            stateController = instance.GetComponent<StateController>();
            botMovement.SetupAI(stateController.SetupAI(true));

        }

        
        public void DisableControl()
        {
            if (botMovement != null)
                botMovement.enabled = false;

            if (stateController != null)
                stateController.enabled = false;

            botAttack.enabled = false;
        }

        
        public void EnableControl()
        {
            if (botMovement != null)
                botMovement.enabled = true;

            if (stateController != null)
                stateController.enabled = true;

            botAttack.enabled = true;
        }


    }
}