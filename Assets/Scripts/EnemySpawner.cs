using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject SpriteToSpawn;
    public float spawnRate = 2f;

    Vector2 whereToSpawn;
    float nextSpawn = 0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;

            whereToSpawn = new Vector2(this.transform.position.x, this.transform.position.y);

            Instantiate(SpriteToSpawn, whereToSpawn, Quaternion.identity);
        }
    }
}
