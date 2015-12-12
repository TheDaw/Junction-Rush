using UnityEngine;
using System.Collections;

public class CarMove : MonoBehaviour {
    public double speed = 0.25;
    bool go = true;
    bool slowDown = false;
    public bool inTrafficZone = false;
    GameObject currentZone;
    public enum Direction { North, East, South, West }

    public Direction currentDirection;

    // Use this for initialization
    void Start () {
        if (transform.position.z < -25)
            currentDirection = Direction.North;
        if (transform.position.z > 25)
            currentDirection = Direction.South;
        if (transform.position.x < -25)
        {
            currentDirection = Direction.East;
            transform.Rotate(new Vector3(0, 90, 0));
        }
        if (transform.position.x > 25)
        {
            currentDirection = Direction.West;
            transform.Rotate(new Vector3(0, -90, 0));
        }
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
            speed += 0.005;
        }

        if (inTrafficZone)
        {
            lightCheck();
        }
    }

    void lightCheck()
    {
        if (currentZone.GetComponent<TrafficLight>().straightGreen == false)
        {
            slowDown = true;
        }
        else if (currentZone.GetComponent<TrafficLight>().straightGreen == true)
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
                this.gameObject.transform.Translate(new Vector3(0, 0, (float)speed));
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
            case Direction.East:
                if (transform.position.x < -30 && speed > 0.2)
                {
                    speed -= 0.01;
                }
                else if (transform.position.x < -20 && transform.position.x > -25 && speed > 0.15)
                {
                    speed -= 0.01;
                }
                else if (transform.position.x < -12 && transform.position.x > -20 && speed > 0.10)
                {
                    speed -= 0.01;
                }
                else if (transform.position.x < -7 && transform.position.x > -12 && speed > 0.05)
                {
                    speed -= 0.01;
                }

                else if (transform.position.x > -7)
                {
                    go = false;
                }
                break;
            case Direction.West:
                if (transform.position.x > 30 && speed > 0.2)
                {
                    speed -= 0.01;
                }
                else if (this.transform.position.x > 20 && this.transform.position.x < 25 && speed > 0.15)
                {
                    speed -= 0.01;
                }
                else if (this.transform.position.x > 12 && this.transform.position.x < 20 && speed > 0.10)
                {
                    speed -= 0.01;
                }
                else if (this.transform.position.x > 7 && this.transform.position.x < 12 && speed > 0.05)
                {
                    speed -= 0.01;
                }

                else if (this.transform.position.x < 7)
                {
                    go = false;
                }
                break;
        }
    }

    Direction getCurrentDirection()
    {
        return currentDirection;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Stop")
        {
            inTrafficZone = true;
            currentZone = other.gameObject;
        }

        if (other.gameObject.tag == "Rear Ending")
        {
            if ( other.gameObject.GetComponentInParent<CarMove>().getCurrentDirection() == currentDirection)
            {
                go = false;
                speed = 0.05;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Stop")
        {
            inTrafficZone = false;
        }

        if (other.gameObject.tag == "Rear Ending")
        {
            if (other.gameObject.GetComponent<CarMove>().currentDirection == currentDirection)
            {
                go = true;
            }
        }
    }
}
