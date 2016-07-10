using UnityEngine;
using System.Collections;

public class MazeGenerator : MonoBehaviour {

	public Maze mazePrefab;
	private Maze mazeInstance;
	private Maze mazeInstance2;

	private void Start () {
		BeginGame();
	}

	private void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			RestartGame();
		}
	}

	private void BeginGame () {
		int mazeSize = 55; 
		mazeInstance = Instantiate(mazePrefab) as Maze;
		mazeInstance2 = Instantiate (mazePrefab) as Maze;
		mazeInstance.setOffset (0, 0);
		mazeInstance2.setOffset (4, 4);
		Cell[,] cellsForMaze;
		cellsForMaze = GenerateMaze.getMaze(mazeSize,mazeSize + 2);
		mazeInstance.generateFloor(mazeSize,mazeSize + 2);
		mazeInstance.generateGivenMaze(cellsForMaze);

	}

	private void RestartGame () {
		Destroy(mazeInstance.gameObject);
		BeginGame();
	}
}
