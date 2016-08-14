using UnityEngine;
using System.Collections;

public class EnemyPlacer : MonoBehaviour {

    //places enemies of any type at a location in the world maze 
    public Eye eyePrefab;
	public Transform groundReference;
    // Inititalization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void placeEye(Eye eye){
		//Eye newEye = Instantiate(eye.prefab) as Eye; //TODO  this is wrong - why is this wrong?
		eye.name = eye.type + " " + eye.	id;
		eye.transform.parent = transform;
		eye.transform.localPosition = eye.startLocation;
        
		//handle rotation and scale if needed
    }

    public void makeSomeTestEyes()
    {
        Eye testEye = Instantiate(eyePrefab) as Eye;
        testEye.startLocation = new Vector3(10f, 1.0f, 10f);
		testEye.groundReference = groundReference;
        testEye.type = EnemyType.EYE;
        testEye.id = 0;
        placeEye(testEye);
		Debug.Log("making eyes ");
		Eye testEye2 = Instantiate(eyePrefab) as Eye;
		testEye2.startLocation = new Vector3(20f, 1.0f, 10f);
		testEye2.groundReference = groundReference;
		testEye2.type = EnemyType.EYE;
		testEye2.id = 1;
		placeEye(testEye2);
    }


}
