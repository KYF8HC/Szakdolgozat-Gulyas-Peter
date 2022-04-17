using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 200f;
    [SerializeField] private Transform levelPart_Start;
    [SerializeField] private EnemyAgent Player;
    [SerializeField] private List<Transform> levelPartList;
    private Vector3 lastEndPosition;
    public Transform parent;
    //[SerializeField] private List<Transform> levelKEKWaitPartList;
    //[SerializeField] private List<Transform> levelHappyPartList;
    //[SerializeField] private List<Transform> levelKEKWPartList;
   // public double valence;


    public void Awake() {
        lastEndPosition = levelPart_Start.Find("EndPosition").position;
        parent = GameObject.Find("GameArea").transform;
        int startingSpawnLevelParts = 5;
        for (int i = 0; i < startingSpawnLevelParts; i++)
            SpawnLevelPart();
    }
    private void Update() {
        if (Vector3.Distance(Player.transform.position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART) 
            SpawnLevelPart();
    }
    private void SpawnLevelPart()
    {
        Transform chosenLevelPart;
        chosenLevelPart = levelPartList[Random.Range(0, levelPartList.Count)];
        Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, lastEndPosition);
        lastLevelPartTransform.transform.parent = parent;
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
        //Transform chosenLevelPart = levelPartList[Random.Range(0, levelPartList.Count)];
        // if (valence <= .25)
        // {
        // }
        //else if (valence > .25 && valence <= .5)
        //{
        //    chosenLevelPart = levelKEKWaitPartList[Random.Range(0, levelKEKWaitPartList.Count)];
        //    Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, lastEndPosition);
        //    lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
        //}
        //else if (valence > .5 && valence <= .75)
        //{
        //    chosenLevelPart = levelHappyPartList[Random.Range(0, levelHappyPartList.Count)];
        //    Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, lastEndPosition);
        //    lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
        //}
        //else
        //{
        //    chosenLevelPart = levelKEKWPartList[Random.Range(0, levelKEKWPartList.Count)];
        //    Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, lastEndPosition);
        //    lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
        //}
        
    }
    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition) {
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }
}
