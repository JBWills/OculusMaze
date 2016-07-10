using UnityEngine;
using System.Collections;

public class EnemyPlacer : MonoBehaviour {

    //places enemies of any type at a location in the world maze 
    public Eye eyePrefab;
    // Inititalization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void placeEnemies(Enemy[,] enemiesToPlace){
        foreach (Enemy enemy in enemiesToPlace){
            placeEnemy(enemy);
        }
    }

    void placeEnemy(Enemy enemy){
        Enemy newEnemy = Instantiate(enemy.prefab) as Enemy; //TODO  this is wrong
        newEnemy.name = enemy.type + " " + enemy.id;
        newEnemy.transform.parent = transform;
        newEnemy.transform.localPosition = newEnemy.startLocation;
        
            //handle rotation and scale if needed
    }

    public void makeSomeTestEyes()
    {
        Eye testEye = Instantiate(eyePrefab) as Eye;
        testEye.startLocation = new Vector3(10f, 1.0f, 10f);
        testEye.type = EnemyType.EYE;
        testEye.id = 0;
        placeEnemy(testEye);
    }


}
