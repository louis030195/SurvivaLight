using UnityEngine;
using System.Collections;

namespace SurvivaLight
{

    public class PlayerManager : MonoBehaviour
    {


        private Rigidbody rb;


        public float inputDelay = 0.1f;
        public float forwardVel = 120;
        public float rotateVel = 100;
        [HideInInspector]public bool control;

        Quaternion targetRotation;
        float forwardInput, turnInput, attackInput;

        public Quaternion TargetRotation
        {
            get { return targetRotation; }
        }

        void Start()
        {
            targetRotation = transform.rotation;
            if (GetComponent<Rigidbody>())
                rb = GetComponent<Rigidbody>();
            else
                Debug.LogError("The character needs a rigidbody.");

            forwardInput = turnInput = 0;
        }

        void GetInput()
        {
            forwardInput = Input.GetAxis("Vertical");
            turnInput = Input.GetAxis("Horizontal");
            attackInput = Input.GetAxis("Fire1");
        }

        void Update()
        {
            if (control)
            {
                GetInput();
                Attack();
                Turn();
            }
        }

        private void FixedUpdate()
        {
            if(control)
                Run();
        }

        void Attack()
        {


            if (attackInput > 0)
            {
                RaycastHit hit;


                Vector3 castOrigin = transform.position - transform.forward * 10 * 2; // the origin of the spherecast need to start behind

                if (Physics.SphereCast(castOrigin, 10, transform.forward, out hit, 10, LayerMask.GetMask("AI")))
                {
                    Debug.Log("Attack");
                    BotAttack BotAttack = GetComponent<BotAttack>();
                    BotAttack.Attack(1, hit);
                }
            }
        }

        void Run()
        {
            if (Mathf.Abs(forwardInput) > inputDelay)
            {
                rb.velocity = transform.forward * forwardInput * forwardVel;
            }
            else
                rb.velocity = Vector3.zero;
        }
        void Turn()
        {
            if (Mathf.Abs(turnInput) > inputDelay)
            {
                targetRotation *= Quaternion.AngleAxis(rotateVel * turnInput * Time.deltaTime, Vector3.up);
            }
            transform.rotation = targetRotation;
        }
    }
}