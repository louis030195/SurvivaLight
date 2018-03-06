using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SurvivaLight
{
    public class GameManager : MonoBehaviour
    {
        enum GameState
        {
            Won,
            Lost,
            Playing
        };


        public int startingAi;    // Think to use a scriptableobject to param instead
        public GameObject[] aiPrefabs;        // prefabs
        public GameObject groundPrefab;
        public GameObject playerPrefab;
        public Text GUI;
        public CameraControl cameraControl;       // Reference to the CameraControl script for control during different phases.
        [HideInInspector]
        public List<AiManager> bots;     // A collection of managers for enabling and disabling different aspects of the bots.
        [HideInInspector]public PlayerManager player;

        public Text messageText;                  // Reference to the overlay Text to display winning text, etc.
        public int numRoundsToWin = 5;            // The number of rounds a single player has to win to win the game.
        public float startDelay = 3f;             // The delay between the start of RoundStarting and RoundPlaying phases.
        public float endDelay = 3f;               // The delay between the end of RoundPlaying and RoundEnding phases.

        private bool roundWon;

        private GameState gameState;
        private int roundNumber;                  // Which round the game is currently on.
        private WaitForSeconds startWait;         // Used to have a delay whilst the round starts.
        private WaitForSeconds endWait;           // Used to have a delay whilst the round or game ends.


        private void Awake()
        {
            cameraControl.player = Instantiate(playerPrefab, new Vector3(500, 0, 500), new Quaternion(0, 0, 0, 0));

        }

        // Use this for initialization
        void Start()
        {
            // Create the delays so they only have to be made once.
            startWait = new WaitForSeconds(startDelay);
            endWait = new WaitForSeconds(endDelay);

            SpawnAllAi();
            gameState = GameState.Playing;

            player = playerPrefab.GetComponent<PlayerManager>();

            // Once the ai have been created, start the game.
            StartCoroutine(GameLoop());
        }

        public void SpawnAllAi()
        {
            for (int i = 0; i < startingAi; i++)
            {
                AiManager bot = new AiManager();
                bot.instance = Instantiate(aiPrefabs[Random.Range(0, aiPrefabs.Length)], new Vector3(i * 80, 1, i * 80), new Quaternion(0, 0, 0, 0)) as GameObject; // TODO : random circle spawn
                bot.SetupAI();
                bots.Add(bot);

            }
        }

        // This is called from start and will run each phase of the game one after another.
        private IEnumerator GameLoop()
        {
            // Start off by running the 'RoundStarting' coroutine but don't return until it's finished.
            yield return StartCoroutine(RoundStarting());

            // Once the 'RoundStarting' coroutine is finished, run the 'RoundPlaying' coroutine but don't return until it's finished.
            yield return StartCoroutine(RoundPlaying());

            // Once execution has returned here, run the 'RoundEnding' coroutine, again don't return until it's finished.
            yield return StartCoroutine(RoundEnding());

            // This code is not run until 'RoundEnding' has finished.  At which point, check if a game winner has been found.
            switch (gameState)
            {
                case GameState.Won:
                    // If there is a game winner, restart the level.
                    messageText.text = "CONGRATULATION";
                    SceneManager.LoadScene(0);
                    break;
                case GameState.Lost:
                    // If game is lost, restart the level.
                    messageText.text = "LOSER !!";
                    SceneManager.LoadScene(0);
                    break;
                case GameState.Playing:
                    // If there isn't a winner yet, restart this coroutine so the loop continues.
                    // Note that this coroutine doesn't yield.  This means that the current version of the GameLoop will end.
                    StartCoroutine(GameLoop());
                    break;
            }
        }

        private IEnumerator RoundStarting()
        {
            DisableControl();
            
            // Increment the round number and display text showing the players what round it is.
            roundNumber++;
            messageText.text = "ROUND " + roundNumber;

            // Wait for the specified length of time until yielding control back to the game loop.
            yield return startWait;
        }


        private IEnumerator RoundPlaying()
        {
            roundWon = false;
            // As soon as the round begins
            EnableControl();

            // Clear the text from the screen.
            messageText.text = string.Empty;
            
            while (!RoundFinished())
            {
                // ... return on the next frame.
                yield return null;
            }
        }

        private bool RoundFinished()
        {
            return bots.Count == 0 || !player; // All AIs are dead or player is dead
        }


        private IEnumerator RoundEnding()
        {
            // Stop from moving.
            DisableControl();

            if (bots.Count == 0) gameState = GameState.Won; else gameState = GameState.Lost; // If we killed all AI = won, else we lost

            // Wait for the specified length of time until yielding control back to the game loop.
            yield return endWait;
        }



        private void EnableControl()
        {
            for (int i = 0; i < bots.Count; i++)
            {
                bots[i].EnableControl();
            }
            player.control = true;
        }


        private void DisableControl()
        {
            for (int i = 0; i < bots.Count; i++)
            {
                bots[i].DisableControl();
            }
            player.control = false;
        }




        public IEnumerator FadeTextToFullAlpha(float t, Text i)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
            while (i.color.a < 1.0f)
            {
                i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
                yield return null;
            }
        }

        public IEnumerator FadeTextToZeroAlpha(float t, Text i)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
            while (i.color.a > 0.0f)
            {
                i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
                yield return null;
            }
        }
    }
}