using UnityEngine;

public class Cameraposition : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] float Displace;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + Displace);
    }
}
