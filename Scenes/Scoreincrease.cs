using UnityEngine;

public class Scoreincrease : MonoBehaviour
{
    Logicscript logic;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = FindFirstObjectByType<Logicscript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            logic.Addscore(1);
            Destroy(gameObject);
        }
    }
}
