using UnityEngine;
using System.Collections;

public class MazeGenerator : MonoBehaviour {

	public Maze mazePrefab;
	private Maze mazeInstance;
    private void Start () {
	}

	private void Update () {
	}

	public void instantiateMaze (int mazeSizeX, int mazeSizeZ, float mazeScale) {
        mazeInstance = Instantiate(mazePrefab) as Maze;
        mazeInstance.setOffsetAndScale(0, 0, mazeScale);
		Cell[,] cellsForMaze;
		cellsForMaze = GenerateMaze.getMaze(mazeSizeX, mazeSizeZ);
		//mazeInstance.generateFloor(mazeSizeX, mazeSizeZ); 
		mazeInstance.generateGivenMaze(cellsForMaze);
	}
}
