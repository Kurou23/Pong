using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public string namePowerUp;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ball")
        {
            if (namePowerUp == "BonusScore")
            {
                col.GetComponent<Ball>().bonusGoal = true;
            }

            if (namePowerUp == "SpeedUp")
            {
                col.GetComponent<Ball>().speed *= 2f;
            }

            if (namePowerUp == "SpeedUp")
            {
                Ball ball =  col.GetComponent<Ball>();
                if (ball.isLastHit1)
                {
                    ball.GetComponent<Rigidbody2D>().velocity = new Vector2(1,Random.Range(-1,1));
                }
                else {
                    ball.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, Random.Range(-1, 1));
                }
            }

            Destroy(gameObject);
        }
    }
}
