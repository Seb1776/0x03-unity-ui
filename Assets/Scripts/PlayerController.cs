using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int health = 5;
    public float speed;

    int score = 0;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    //Even though the task ask for it, this is not the best way to do a "Health Check"
    //It's faster and less performance-consuming to check this when the player takes damage
    void Update()
    {
        if (health <= 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void FixedUpdate()
    {
        Vector3 playerInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        rb.MovePosition(transform.position + playerInput * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            score++;
            Debug.Log("Score: " + score);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Trap"))
        {   
            if (health > 0)
                health--;
            
            Debug.Log("Health: " + health);
        }

        if (other.CompareTag("Goal"))
            Debug.Log("You win!");
    }
}
