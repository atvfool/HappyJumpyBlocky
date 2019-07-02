using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Camera : MonoBehaviour
{

    public Transform cameraTarget;

    public float cameraSpeed;

    public float minX;
    public float minY;
    public float maxX;
    public float maxY;

    float highestYSoFar = 0f;

    public GameObject SpriteToSpawn;
    public float spawnRate = 2f;
    public float XSpawnPosition = 1f;
    public float YSpawnPosition = 2f;

    float randX;
    float randY;
    Vector2 whereToSpawn;
    float nextSpawn = 0f;

    void FixedUpdate()
    {
        if(cameraTarget != null)
        {
            Vector2 newPos = Vector2.Lerp(transform.position, cameraTarget.position, Time.deltaTime * cameraSpeed);

            Vector3 vect3 = new Vector3(newPos.x, newPos.y, -10f);

            var clampX = Mathf.Clamp(vect3.x, minX, maxX);
            var clampY = Mathf.Clamp(vect3.y, minY, maxY);

            transform.position = new Vector3(clampX, clampY, -10f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cameraTarget != null)
        {
            if (transform.position.y > highestYSoFar && Time.time > nextSpawn)
            {
                highestYSoFar = transform.position.y;
                nextSpawn = Time.time + spawnRate;

                randX = Random.Range(cameraTarget.position.x-XSpawnPosition, cameraTarget.position.x + XSpawnPosition);
                randY = Random.Range(transform.position.y, transform.position.y + YSpawnPosition);

                whereToSpawn = new Vector2(randX, randY);

                Instantiate(SpriteToSpawn, whereToSpawn, Quaternion.identity);
                IncreaseTextUIScore();
            }
        }
    }

    void IncreaseTextUIScore()
    {
        var textUIComp = GameObject.Find("Score").GetComponent<Text>();

        int score = int.Parse(textUIComp.text);

        score++;

        textUIComp.text = score.ToString();
    }
}
