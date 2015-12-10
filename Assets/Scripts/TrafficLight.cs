using UnityEngine;
using System.Collections;

public class TrafficLight : MonoBehaviour
{

    public bool green = false;
    double destroy = 5;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (green)
            destroy -= Time.deltaTime;

        if (destroy < 0)
            Destroy(this.gameObject);
    }

    void onMouseDown()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Car")
            green = true;
    }
}
