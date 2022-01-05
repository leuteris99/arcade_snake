using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class text : MonoBehaviour
{

    public text label;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 labelPos = Camera.main.WorldToScreenPoint(this.transform.position);
        label.transform.position = labelPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
