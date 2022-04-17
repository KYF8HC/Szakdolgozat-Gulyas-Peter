using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public int health = 100;
	public GameObject deathEffect;
	[SerializeField]
	private EnemyAgent AI;
    
    public void TakeDamage (int damage)
	{
		health -= damage;
		if (health <= 0)
			Die();
	}

	void Die ()
	{
		AI.score += 1;
		Destroy(gameObject);
	}
}
