using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public int health = 100;
	public GameObject deathEffect;
	[SerializeField]
	private EnemyAgent AI;
    public Object player;

    public void TakeDamage(int damage)
	{
		health -= damage;
		if (health <= 0)
			Die();
	}

	public void Die()
	{
		AI.score += 1;
		gameObject.SetActive(false);
		Invoke("Respawn", 3);
	}
	public void Respawn()
	{
		GameObject DummyClone = (GameObject)Instantiate(player);
		DummyClone.GetComponent<Player>().health = 100;
		DummyClone.GetComponent<TrainingWeapon>().firePoint = DummyClone.transform;
		DummyClone.GetComponent<TrainingWeapon>().bulletPrefab = GetComponent<TrainingWeapon>().bulletPrefab;
		DummyClone.GetComponent<Player>().AI = GetComponent<Player>().AI;
		DummyClone.transform.position = transform.position;
		Destroy(gameObject);
	}
    private void Start()
    {
		player = Resources.Load("Dummy");
    }
}
