using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public string axis = "Vertical";

    private void Update()
    {
        // Buat Disable Player 2 Input Kalo lagi Single Player
        if (axis == "Vertical2" && GameData.instance.isSinglePlayer)
        {
            return;
        }

        // Ngambil Variable Dari Axing yang udah di seting di Uinty Input dengan output (-1,1)
        float v = Input.GetAxis(axis);
        rb.velocity = new Vector2(0, v) * speed;


        // Biar Gk Keluar Batas Atas
        if (transform.position.y > 1f )
        {
            transform.position = new Vector2(transform.position.x, 1f);
        }

        // Biar Gk Kelua Batas Bawah
        if (transform.position.y < -1f)
        {
            transform.position = new Vector2(transform.position.x, -1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ball")
        {
            Debug.Log("Hit");
            gameObject.GetComponent<Animator>().SetTrigger("Shoot");

        }
    }
}
