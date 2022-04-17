using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardForAIForDistance : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        collider2D.GetComponent<EnemyAgent>().score += 1;
    }
}
