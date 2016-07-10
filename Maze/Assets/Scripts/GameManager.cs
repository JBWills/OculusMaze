using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public MazeGenerator mazeGeneratorPrefab;
    private MazeGenerator mazeGenerator;

    public EnemyPlacer enemyplacePrefab;
    private EnemyPlacer enemyPlacer;
    // Use this for initialization
    void Start () {
        BeginGame();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            RestartGame();
        }
    }

    private void BeginGame()
    {
        mazeGenerator = Instantiate(mazeGeneratorPrefab) as MazeGenerator;
        //TODO: Offset based on the maze being placed
        mazeGenerator.instantiateMaze(20, 20, 3.0f);

        enemyPlacer = Instantiate(enemyplacePrefab) as EnemyPlacer;
        enemyPlacer.makeSomeTestEyes();


    }

    //destroy any instances
    private void RestartGame()
    {
        Destroy(mazeGenerator.gameObject);
        Destroy(enemyPlacer.gameObject);
        BeginGame();
    }

    private Enemy[] makeEnemiesForGame()
    {
        return new Enemy[2];
    }

}
