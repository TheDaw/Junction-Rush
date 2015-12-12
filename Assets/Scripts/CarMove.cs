using UnityEngine;
using System.Collections;

public class CarMove : MonoBehaviour {
    public double speed = 0.25;
    bool go = true;
    bool slowDown = false;
    public bool inTrafficZone = false;
    GameObject currentZone;
    public enum Direction { North, East, South, West }

    public Direction currentDirection = Direction.North;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (go)
        {
            driveToDestination();
        }            
        if (slowDown == true)
        {
            slowSpeed(currentDirection);
        }
        if (slowDown == false && speed <= 0.25)
        {
            speed += 0.01;
        }

        if (inTrafficZone)
        {
            lightCheck();
        }
    }

    void lightCheck()
    {
        if (currentZone.GetComponent<TrafficLight>().straightGreen == true)
            slowDown = true;
        else if(currentZone.GetComponent<TrafficLight>().straightGreen == false)
        {
            slowDown = false;
            go = true;
        }
    }

    void driveToDestination()
    {
        switch (currentDirection)
        {
            case Direction.North:
                this.gameObject.transform.Translate(new Vector3(0, 0, (float)speed));
                break;
            case Direction.South:
                this.gameObject.transform.Translate(new Vector3(0, 0, -(float)speed));
                break;
            case Direction.East:
                this.gameObject.transform.Translate(new Vector3(0, 0, -(float)speed));
                break;
            case Direction.West:
                this.gameObject.transform.Translate(new Vector3(0, 0, (float)speed));
                break;
        }
    }
    
    void slowSpeed(Direction dir)
    {
        switch(dir)
        {
            case Direction.North:
                if (this.transform.position.z < -30 && speed > 0.2)
                {
                    speed -= 0.01;
                }
                else if (this.transform.position.z < -20 && this.transform.position.z > -25 && speed > 0.15)
                {
                    speed -= 0.01;
                }
                else if (this.transform.position.z < -12 && this.transform.position.z > -20 && speed > 0.10)
                {
                    speed -= 0.01;
                }
                else if (this.transform.position.z < -7 && this.transform.position.z > -12 && speed > 0.05)
                {
                    speed -= 0.01;
                }

                else if (this.transform.position.z > -7)
                {
                    go = false;
                }
                break;
            case Direction.South:
                if (this.transform.position.z > 30 && speed > 0.2)
                {
                    speed -= 0.01;
                }
                else if (this.transform.position.z > 20 && this.transform.position.z < 25 && speed > 0.15)
                {
                    speed -= 0.01;
                }
                else if (this.transform.position.z > 12 && this.transform.position.z < 20 && speed > 0.10)
                {
                    speed -= 0.01;
                }
                else if (this.transform.position.z > 7 && this.transform.position.z < 12 && speed > 0.05)
                {
                    speed -= 0.01;
                }

                else if (this.transform.position.z < 7)
                {
                    go = false;
                }
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Stop")
        {
            inTrafficZone = true;
            currentZone = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Stop")
        {
            inTrafficZone = false;
        }
    }
}
