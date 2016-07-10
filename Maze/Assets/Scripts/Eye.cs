using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class Eye : Enemy {

    public float ACCELERATION = 1;
    public Rigidbody rb;
    private Direction currentDirection;

    public Transform goal;

    enum Direction
    {
        FORWARD,
        BACKWARD,
        LEFT,
        RIGHT
    }

    Vector3 getVector(Direction d)
    {
        Vector3 v;
        switch(d)
        {
            case Direction.FORWARD:
                v = Vector3.forward;
                break;
            case Direction.BACKWARD:
                v = Vector3.back;
                break;
            case Direction.LEFT:
                v = Vector3.left;
                break;
            case Direction.RIGHT:
                v = Vector3.right;
                break;
            default:
                v = Vector3.forward;
                break;
        }

        return v;
    }

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;

        currentDirection = Direction.FORWARD;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    /*
     * 
     * 
    void FixedUpdate()
    {
        List<Direction> a = availableDirections();
        if (a.Count == 0)
        {
            List<Direction> allDirs = getAllDirections();
            currentDirection = allDirs[((int)UnityEngine.Random.value * allDirs.Count)];
        }
        else if (!a.Contains(currentDirection))
        {
            currentDirection = a[((int)UnityEngine.Random.value * a.Count)];
            Debug.Log(Enum.GetName(typeof(Direction), currentDirection));
        }

        rb.AddForce(getVector(currentDirection));
    }
     */

    List<Direction> availableDirections()
    {
        List<Direction> availableDirections = new List<Direction>();
        float distance = 1.5f;

        foreach (Direction d in getAllDirections())
        {
            if (!Physics.Raycast(transform.position, getVector(d), distance))
            {
                availableDirections.Add(d);
            }
        }

        return availableDirections;
    }

    List<Direction> getAllDirections()
    {
        return Enum.GetValues(typeof(Direction)).Cast<Direction>().ToList<Direction>();
    }
}
