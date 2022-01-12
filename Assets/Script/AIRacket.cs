using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRacket : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Npc Setting")]
    public float speed;
    public float delayMove;

    private bool isMoveAI;
    private float randomPos;
    private bool triggerDelay;
    private bool isUp;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameData.instance.isSinglePlayer)
        {
            if (!isMoveAI && !triggerDelay)
            {
                triggerDelay = true;
                StartCoroutine("DelayAIMove");
            }

            if (isMoveAI)
            {
                MoveAI();
            }

        }
    }

    private IEnumerator DelayAIMove()
    {
        yield return new WaitForSeconds(delayMove);
        isMoveAI = true;
        randomPos = Random.Range(-1f, 1f);
        triggerDelay = false;
        if (transform.position.y < randomPos)
        {
            Debug.Log("atas");
            isUp = true;
        }
        else
        {
            Debug.Log("bawah");
            isUp = false;
        }
    }

    private void MoveAI()
    {
        if (!isUp)
        {
            rb.velocity = new Vector2(0, -1) * speed;
            if (transform.position.y <= randomPos)
            {
                Debug.Log("Diem");
                rb.velocity = Vector2.zero;
                isMoveAI = false;
            }
        }

        if (isUp)
        {
            rb.velocity = new Vector2(0, 1) * speed;
            if (transform.position.y >= randomPos)
            {
                Debug.Log("Diem");
                rb.velocity = Vector2.zero;
                isMoveAI = false;
            }

        }
    }
}
