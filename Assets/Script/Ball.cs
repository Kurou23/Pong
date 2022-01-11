using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        int random = Random.Range(0,2);
        Debug.Log(random);
        if (random == 0)
        {
            rb.velocity = Vector2.right * speed;
        }
        else {
            rb.velocity = Vector2.left * speed;
        }
    }

    float hitFactor(Vector2 ballPos, Vector2 racketPos,float racketHeight)
    {
        
        return (ballPos.y - racketPos.y) / racketHeight;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Kena");
        if (col.gameObject.tag == "RacketLeft")
        {
            float y = hitFactor(transform.position, col.transform.position, col.collider.bounds.size.y);
            Debug.Log(y);
            Vector2 dir = new Vector2(1, y).normalized;
            rb.velocity = dir * speed;
        }

        if (col.gameObject.tag == "RacketRight")
        {
            float y = hitFactor(transform.position, col.transform.position, col.collider.bounds.size.y);
            Debug.Log(y);
            Vector2 dir = new Vector2(-1, y).normalized;
            rb.velocity = dir * speed;
        }
    }
}
