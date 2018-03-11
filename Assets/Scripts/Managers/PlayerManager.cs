using UnityEngine;
using System.Collections;

namespace SurvivaLight
{

    public class PlayerManager : MonoBehaviour
    {

        [HideInInspector] public GameObject instance;

        CharacterController charControl;


        [HideInInspector] public bool control;




        void Awake()
        {
            charControl = GetComponent<CharacterController>();
        }


        void Start()
        {
            Cursor.visible = false;

        }


        void Update()
        {
            MovePlayer();
        }





        void MovePlayer()
        {
            float horiz = Input.GetAxis("Horizontal");
            float vert = Input.GetAxis("Vertical");
            Vector3 moveDirSide = transform.right * horiz * 10;
            Vector3 moveDirForward = transform.forward * vert * 10;


            charControl.SimpleMove(moveDirSide);
            charControl.SimpleMove(moveDirForward);


        }
    }
}