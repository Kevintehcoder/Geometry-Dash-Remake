using UnityEngine;
using UnityEngine.InputSystem;

public class Fist : MonoBehaviour
{
    Animator animator;
    Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Attacks();

    }




    void Attacks()
    {
        if (Keyboard.current.eKey.isPressed)
        {

            animator.SetBool("Punching", true);
            player.attacking = true;
        }
        else if (Keyboard.current.eKey.wasReleasedThisFrame)
        {

            animator.SetBool("Punching", false);
            player.attacking = false;
        }


        if (Keyboard.current.rKey.isPressed)
        {

            animator.SetBool("UpPunching", true);
            player.attacking = true;
        }
        else if (Keyboard.current.rKey.wasReleasedThisFrame)
        {

            animator.SetBool("UpPunching", false);
            player.attacking = false;
        }

    }
}
