using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int numberToSpawn; // the number of Apples to spawn each time.
    public GameObject spawnObject;
    public GameObject quad; // the area in which apples can spawn.

    // Start is called before the first frame update
    void Start()
    {
        spawnObjects();
    }

    // Update is called once per frame
    void Update()
    {
        // if there are no apples in the scene than respawn them.
        int c = 0;
        string target = FindObjectOfType<ScoreManager>().GetTarget();
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("Apple"))
        {
            GameObject child = o.transform.GetChild(0).gameObject;
            string text = child.GetComponent<TextMesh>().text;
            if (CompareTargetWithNumberType(int.Parse(text), target))
            {
                c++;
            }
        }
        if(c == 0){
            spawnObjects();
        }
    }

    // Spawning the apples.
    public void spawnObjects()
    {
        //int randomItem = 0;
        //GameObject toSpawn;
        MeshCollider c = quad.GetComponent<MeshCollider>();

        float screenX;
        float screenY;
        Vector2 pos;

        for (int i = 0; i < numberToSpawn; i++)
        {
            // randomItem = Random.Range(0,spawnPool.Count);
            // toSpawn = spawnPool[randomItem];
            spawnObject.gameObject.tag = "Apple";

            screenX = Random.Range(c.bounds.min.x,c.bounds.max.x);
            float stepSize = 0.8f;
            float numXSteps = Mathf.Floor (screenX / stepSize);
            float adjustedX = numXSteps * stepSize;
            screenY = Random.Range(c.bounds.min.y,c.bounds.max.y);
            float numYSteps = Mathf.Floor (screenY / stepSize);
            float adjustedY = numYSteps * stepSize;
            pos = new Vector2(adjustedX,adjustedY);

            while(Physics2D.OverlapCircleAll(new Vector3(pos.x,pos.y,0), 0.55f).Length > 0)
            {
                Debug.Log("Sphere check");
                screenX = Random.Range(c.bounds.min.x, c.bounds.max.x);
                numXSteps = Mathf.Floor(screenX / stepSize);
                adjustedX = numXSteps * stepSize;
                screenY = Random.Range(c.bounds.min.y, c.bounds.max.y);
                numYSteps = Mathf.Floor(screenY / stepSize);
                adjustedY = numYSteps * stepSize;
                pos = new Vector2(adjustedX, adjustedY);
            }

            GameObject apple =  Instantiate(spawnObject, pos, spawnObject.transform.rotation);

            //Create new GameObject for the text
            GameObject childObj = new GameObject();

            //Make apple to be parent of this text gameobject
            childObj.transform.parent = apple.transform;
            childObj.name = "Text Holder";

            //Create TextMesh and modify its properties
            TextMesh textMesh = childObj.AddComponent<TextMesh>();
            int number = Random.Range(1, 100); // create a random number to put to the text
            textMesh.text = number.ToString();
            textMesh.characterSize = 0.1f;

            //Set postion of the TextMesh with offset
            textMesh.anchor = TextAnchor.MiddleCenter;
            textMesh.alignment = TextAlignment.Center;
            textMesh.transform.position = new Vector3(apple.transform.position.x, apple.transform.position.y, apple.transform.position.z);

        }
    }

    private bool CompareTargetWithNumberType(int num,string target)
    {
        string numType;
        if (num % 2 == 0)
        {
            numType = "evens";
        }
        else
        {
            numType = "odds";
        }
        if (numType.Equals(target))
        {
            return true;
        }
        return false;
    }

    // destroying all the apples.
    private void destroyObjects(){
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("Spawnable"))
        {
            Destroy(o);
        }
    }
}
