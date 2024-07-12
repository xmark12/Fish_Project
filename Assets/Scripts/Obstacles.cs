using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;

    private float timer;

    private GameManager gm;
    
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.speedMultiplier < gm.maxSpeedMultiplier)
        {
            rb.velocity = Vector2.left * (speed + gm.speedMultiplier);
        }
        else
        {
            rb.velocity = Vector2.left * (speed + gm.maxSpeedMultiplier);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }
    }
}
