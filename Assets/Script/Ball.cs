using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public bool isBounce;
    public bool bonusGoal;
    public bool isLastHit1;
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

    private void Update()
    {
        if (transform.position.x > 12 || transform.position.x < -12 || transform.position.y > 8 || transform.position.y < -8)
        {
            GameManager.instance.SpwanBall();
            Destroy(gameObject);
        }
    }

    float hitFactor(Vector2 ballPos, Vector2 racketPos,float racketHeight)
    {
        
        return (ballPos.y - racketPos.y) / racketHeight;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        SoundManager.instance.BallBounceSfx();
        if (col.gameObject.tag == "RacketLeft" && !isBounce)
        {
            float y = hitFactor(transform.position, col.transform.position, col.collider.bounds.size.y);
            Debug.Log(y);
            Vector2 dir = new Vector2(1, y).normalized;
            rb.velocity = dir * speed;
            StartCoroutine("DelayBounce");
            isLastHit1 = true;
        }

        if (col.gameObject.tag == "RacketRight" && !isBounce)
        {
            float y = hitFactor(transform.position, col.transform.position, col.collider.bounds.size.y);
            Debug.Log(y);
            Vector2 dir = new Vector2(-1, y).normalized;
            rb.velocity = dir * speed;
            StartCoroutine("DelayBounce");
            isLastHit1 = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Goal 1")
        {
            SoundManager.instance.GoalSfx();
            GameManager.instance.player2Score++;
            if (bonusGoal)
            {
                GameManager.instance.player2Score++;
            }
            GameManager.instance.SpwanBall();
            Destroy(gameObject);
            if (GameManager.instance.goldenGoal)
            {
                GameManager.instance.GameOver();
            }
        }

        if (col.gameObject.tag == "Goal 2")
        {
            SoundManager.instance.GoalSfx();
            GameManager.instance.player1Score++;
            if (bonusGoal)
            {
                GameManager.instance.player2Score++;
            }
            GameManager.instance.SpwanBall();
            Destroy(gameObject);
            if (GameManager.instance.goldenGoal)
            {
                GameManager.instance.GameOver();
            }
        }
    }

    private IEnumerator DelayBounce() {
        isBounce = true;
        yield return new WaitForSeconds(1f);
        isBounce = false;
    }
}

