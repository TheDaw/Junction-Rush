using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.green;
        GetComponentInParent<TrafficLight>().straightGreen = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (GetComponentInParent<TrafficLight>().straightGreen == true)
            {
                GetComponentInParent<TrafficLight>().straightGreen = false;
                gameObject.GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                GetComponentInParent<TrafficLight>().straightGreen = true;
                gameObject.GetComponent<Renderer>().material.color = Color.green;
            }
        }
    }
}
