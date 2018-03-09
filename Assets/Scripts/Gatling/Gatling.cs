using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatling : MonoBehaviour {

   
    public Rigidbody bullet; // Prefab of the bullet.
    float attackInput;
    public AudioSource attackAudio;               // The audio source to play.
    public AudioClip attack;            // Audio to play when the bot attack.

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Attack();
    }

    private void Audio()
    {
        attackAudio.clip = attack;
        attackAudio.Play();
    }

    void Attack()
    {
        attackInput = Input.GetAxis("Fire1");

        if (attackInput > 0)
        {

            GetComponent<Animation>().Play();
            Audio();
            Vector3 bulletOrigin = transform.position + transform.forward * 1.25f;
            // Create an instance of the bullet and store a reference to it's rigidbody.
            Rigidbody bulletInstance =
                Instantiate(bullet, bulletOrigin, transform.rotation) as Rigidbody;

            // Set the bullet's velocity to the launch force in the fire position's forward direction.
            bulletInstance.velocity = transform.forward * 100;

            // Change the clip to the firing clip and play it.
            // shootingAudio.clip = fireClip;
            // shootingAudio.Play();
        }
        else
            GetComponent<Animation>().Stop();
    }
}
