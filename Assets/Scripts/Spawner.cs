using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int numberToSpawn; // the number of Apples to spawn each time.
    public GameObject spawnObject; // the object to spawn to the screen. Aka the apple.
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
        int c = 0; // the number of apples that have the same type as the target. (having the same type as target means, both been odds or evens).
        string target = FindObjectOfType<ScoreManager>().GetTarget(); // get the type of the target.
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("Apple")) // get all the apples and for each of them ...
        {
            GameObject child = o.transform.GetChild(0).gameObject; // get the child text of the apple
            string text = child.GetComponent<TextMesh>().text; // extract it's value
            if (CompareTargetWithNumberType(int.Parse(text), target)) // find the type of the child's value and compare it with the target's.
            {
                c++; // if they are the same than add one to the counter
            }
        }
        if(c == 0){ // if there are no apples in the screen with the same type of the target then spawn new apples.
            spawnObjects();
        }
    }

    // Spawning the apples.
    public void spawnObjects()
    {
        MeshCollider c = quad.GetComponent<MeshCollider>(); // mesh collider of the game object that represents the area of spawning.

        float screenX;
        float screenY;
        Vector2 pos;

        for (int i = 0; i < numberToSpawn; i++) // spawn numberToSpawn apples
        {
            spawnObject.gameObject.tag = "Apple"; // give the spawning apple tag

            screenX = Random.Range(c.bounds.min.x,c.bounds.max.x); // create a random x axis value inside the spawning area
            float stepSize = 0.8f;
            float numXSteps = Mathf.Floor (screenX / stepSize);
            float adjustedX = numXSteps * stepSize;
            screenY = Random.Range(c.bounds.min.y,c.bounds.max.y); // create a random y axis value inside the spawning area
            float numYSteps = Mathf.Floor (screenY / stepSize);
            float adjustedY = numYSteps * stepSize;
            pos = new Vector2(adjustedX,adjustedY);

            // check if the apple overlap with another object. If it does change it's coordinates.
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
            // add the apple on the scene.
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

    // finds if the given number is even or odd, then it compares it with the targets value.
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
