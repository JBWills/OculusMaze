using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class Eye : Enemy {

    public float ACCELERATION = 1;
    public Rigidbody rb;
    private Direction currentDirection;

	public Transform groundReference;
	public int EYE_HEIGHT = 3;

	private  const int X_SCALE = 60;
	private const int Z_SCALE = 60;
	private const int EYE_VISION_RANGE = 500;
	private const int DEST_REACHED_RADIUS = 3;


	public GameObject player;
	private bool playerInSight;
	private Transform lastPlayerSighting;

	private bool chasingPlayer;
	public NavMeshAgent agent;
	private bool destinationReached;
	public Vector3 goalDestination;

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
		agent = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag("Game Player");
		Vector3 goalPos = createRandomGoalDestination();
		goalDestination = goalPos;
		Debug.Log("Starting: " + goalPos.ToString());
		agent.destination = goalPos;
		chasingPlayer = false;
		destinationReached = false;

		InvokeRepeating("UpdateDestination",0,0.3f);
    }	
	
	// Update is called once per frame
	void Update () {
		//could maybe add logic for player death condition here
		if (Vector3.Distance(transform.position, agent.destination) < DEST_REACHED_RADIUS) {
			destinationReached = true;
		}
		findPlayer();
		UpdateDestination();
	}

	//this is called once per second 
	void UpdateDestination() {
		if (playerInSight){
			Debug.Log("Player in sight: :" + player.transform.position.ToString());
			agent.destination = player.transform.position;
		}
		else {
			if (destinationReached){
				destinationReached = false;
				Vector3 newGoal = createRandomGoalDestination();
				Debug.Log("Dest Reached, new: :" + newGoal.ToString());
				if (chasingPlayer){ chasingPlayer = false;}
				agent.destination = newGoal;
			}
		}
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


	Vector3 createRandomGoalDestination() {

		return new Vector3(UnityEngine.Random.Range((-1 * X_SCALE / 2), (X_SCALE / 2)), 4.0f, UnityEngine.Random.Range((-1 * Z_SCALE / 2), (Z_SCALE / 2))); 
//			
//		var xRand = groundReference.localPosition.x + (int)(UnityEngine.Random.value * (X_SCALE / 2));
//		var yPos = groundReference.localPosition.y + EYE_HEIGHT; 
//		var zRand = groundReference.localPosition.z + (int)(UnityEngine.Random.value * (Z_SCALE / 2));
//		Vector3 goalPos = new Vector3(xRand,yPos,zRand);
//		return goalPos;
	}
		
	void findPlayer() {
		RaycastHit hit;
		// ... and if a raycast towards the player hits something...
		if(Physics.Raycast(transform.position, player.transform.position, out hit, EYE_VISION_RANGE))
		{

			if(hit.collider.gameObject == player) {
				Debug.Log("PLAYER SEEN!!!");
				playerInSight = true;
			}
			// ... and if the raycast hits the player...
			else if(hit.collider.gameObject == player)
			{
				Debug.Log("PLAYER lost");
				playerInSight = true;
			}
			else {
				//this should never occur LOL
			}
		}
	}


}
