using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    bool hit = false;
    void Start()
    {
        //Responsible for moving the bullet
        float speed = 25f;
        GetComponent<Rigidbody>().velocity = transform.forward * speed;

    }

    // Update is called once per frame
    void Update()
    {
        if (hit == false)
        {
            Destroy(this.gameObject, 2.5f);
        }
    }

    //If bullet collides with the enemy it destroys the bullet and the game object it collided with
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            hit = true;
            Destroy(this.gameObject);

        }

    }
}
