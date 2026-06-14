using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float boostXforce;
    public float boostYforce;
    Rigidbody2D rb2d;
    Vector2 boostforce;

    public bool gravityswitch = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boostforce = new Vector2(boostXforce, boostYforce);
    }

    // Update is called once per frame
    void Update()
    {
        if (gravityswitch == true)
        {
            boostYforce *= -1;
            boostforce = new Vector2(boostXforce, boostYforce);
            gravityswitch = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb2d = collision.gameObject.GetComponent<Rigidbody2D>();

            rb2d.linearVelocity = new Vector2(rb2d.linearVelocityX, 0);

            rb2d.AddForce(boostforce, ForceMode2D.Impulse);
        }
    }


}
