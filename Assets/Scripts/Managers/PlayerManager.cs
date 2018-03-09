using UnityEngine;
using System.Collections;

namespace SurvivaLight
{

    public class PlayerManager : MonoBehaviour
    {

        [HideInInspector] public GameObject instance;

        CharacterController charControl;
        public float walkSpeed;

        private Rigidbody rb;


        public float inputDelay = 0.1f;
        public float forwardVel = 120;
        public float rotateVel = 100;

        [HideInInspector] public bool control;

        Quaternion targetRotation;
        float forwardInput, turnInput;



        void Awake()
        {
            charControl = GetComponent<CharacterController>();
        }


        void Start()
        {
            Cursor.visible = false;
            if (GetComponent<Rigidbody>())
                rb = GetComponent<Rigidbody>();
            else
                Debug.LogError("The character needs a rigidbody.");
            
            
        }


        void Update()
        {
            if (control)
            {
                MovePlayer();
            }
            
        }

        private void FixedUpdate()
        {
            if(control)
                Run();
        }



        void Run()
        {
            if (Mathf.Abs(forwardInput) > inputDelay)
            {
                rb.velocity = transform.forward;
            }
            else
                rb.velocity = Vector3.zero;
        }



        void MovePlayer()
        {
            float horiz = Input.GetAxis("Horizontal");
            float vert = Input.GetAxis("Vertical");
            Vector3 moveDirSide = transform.right * horiz * walkSpeed;
            Vector3 moveDirForward = transform.forward * vert * walkSpeed;

            charControl.SimpleMove(moveDirSide);
            charControl.SimpleMove(moveDirForward);

        }
    }
}