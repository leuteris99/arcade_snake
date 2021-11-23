using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int numberToSpawn;
    public List<GameObject> spawnPool;
    public GameObject quad;
    // Start is called before the first frame update
    void Start()
    {
        spawnObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnObjects()
    {
        int randomItem =0;
        GameObject toSpawn;
        MeshCollider c = quad.GetComponent<MeshCollider>();

        float screenX;
        float screenY;
        Vector2 pos;

        for (int i = 0; i < numberToSpawn; i++)
        {
            randomItem = Random.Range(0,spawnPool.Count);
            toSpawn = spawnPool[randomItem];

            screenX = Random.Range(c.bounds.min.x,c.bounds.max.x);
            float stepSize = 0.8f;
            float numXSteps = Mathf.Floor (screenX / stepSize);
            float adjustedX = numXSteps * stepSize;
            screenY = Random.Range(c.bounds.min.y,c.bounds.max.y);
            float numYSteps = Mathf.Floor (screenY / stepSize);
            float adjustedY = numYSteps * stepSize;
            pos = new Vector2(adjustedX,adjustedY);

            Instantiate(toSpawn,pos,toSpawn.transform.rotation);
        }
    }

    private void destroyObjects(){
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("Spawnable"))
        {
            Destroy(o);
        }
    }
}