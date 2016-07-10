using UnityEngine;
using System.Collections;

public class Maze : MonoBehaviour {


    public MazeWall mazeWallPrefab;
    public MazeFloorCell mazeFloorCellPrefab;
    private MazeFloorCell[,] floorCells;
    private MazeWall[,] mazeWalls;


	//customization params, make sure to set all before 
	//todo: add hallway width
	private int offsetZ;
	private int offsetX;


    // first method, given width and height generate floor and place prefabs of
	//the walls, this will be called a waffle
//	public void generateWaffle(int sizeX, int sizeZ) {
//		mazeWalls = new MazeWall[sizeX + 1,sizeZ + 1];
//		for (int x = 0; x < sizeX + 1; x++) {
//			for (int z = 0; z < sizeZ + 1; z++) {
//				if (x != sizeX) {
//					generateWallHorizontal(x, z, sizeX, sizeZ);
//				}
//				if (z != -1) {
//					generateWallVertical(x, z, sizeX, sizeZ);
//				}
//			}
//			if (x == sizeX - 1) {
//				//add one more vertical
//				//generateWallHorizontal(x, sizeZ, sizeX, sizeZ);
//			}
//		}
//
//
//	}

	//Cell[,] mazeCells
	public void generateGivenMaze(Cell[,] mazeCells){
		int sizeX = mazeCells.GetLength (0);
		int sizeZ = mazeCells.GetLength (1);
		//print (sizeX + "," + sizeZ);

		mazeWalls = new MazeWall[sizeX + 1,sizeZ + 1];

		for (int x = 0; x < sizeX; x++) {
			for (int z = 0; z < sizeZ; z++) {
				//print ("x,z::" + x + "," + z + "   " );
				Cell currentCell = mazeCells [x, z];
				//print ("cell: " + currentCell.ToString());
				string name = currentCell.ToString ();
				if (!currentCell.north) {
					generateWallHorizontal (x, z, sizeX, sizeZ, name);
				}

				if (!currentCell.east) {
					generateWallEastVertical (x, z, sizeX, sizeZ, name);
				}

				if (!currentCell.south) {
					generateWallSouthHorizontal(x, z, sizeX, sizeZ, name);
				}

				if (!currentCell.west) {
					generateWallVertical (x, z, sizeX, sizeZ, name);
				}

				if (x == 0) {
					//new row, add left-most (West) wall
					generateWallVertical (x, z, sizeX, sizeZ, name);
				}
				if (z == 0) {
					generateWallHorizontal (x, z, sizeX, sizeZ, name);
				}


			}

		}




	}


	public void generateWallHorizontal(int x, int z, int sizeX, int sizeZ, string name){
		createMazeWallCell (x, z, 0.0f, 90.0f,0.0f, 0.0f, 0.5f, sizeX, sizeZ, name);
	}

	public void generateWallVertical(int x, int z, int sizeX, int sizeZ, string name){
		createMazeWallCell (x, z, 0f, 0.0f,0.0f, 0.5f, 0.0f,  sizeX, sizeZ, name);
	}

	public void generateWallSouthHorizontal(int x, int z, int sizeX, int sizeZ, string name){
		createMazeWallCell (x, z, 0.0f, 90.0f,0.0f, 0.0f, -0.5f, sizeX, sizeZ, name );
	}

	public void generateWallEastVertical(int x, int z, int sizeX, int sizeZ, string name){
		createMazeWallCell (x, z, 0f, 0.0f,0.0f, -0.5f, 0.0f,  sizeX, sizeZ, name );
	}




	public void setOffset(int x, int z){
		offsetX = x;
		offsetZ = z;
	}

	//x and z plane floor
	public void generateFloor (int sizeX, int sizeZ) {
		floorCells = new MazeFloorCell[sizeX, sizeZ];
		for (int x = 0; x < sizeX; x++) {
			for (int z = 0; z < sizeZ; z++) {
				createMazeFloorCell(x, z, sizeX, sizeZ);
			}
		}
	}

	private void createMazeFloorCell (int x, int z, int sizeX, int sizeZ) {
		MazeFloorCell newFloorCell = Instantiate(mazeFloorCellPrefab) as MazeFloorCell;
		floorCells[x, z] = newFloorCell;
		newFloorCell.name = "Maze Floor Cell " + x + ", " + z;
		newFloorCell.transform.parent = transform;
		newFloorCell.transform.localPosition = new Vector3(offsetX + x, 0f, offsetZ - z);
	}


	//TODO: horizontal vs vertical
	private void createMazeWallCell(int x, int z, float rotationX, float rotationY, float rotationZ, float offsetRotationX, float offsetRotationZ, int sizeXw, int sizeZw, string name){
		MazeWall newWall = Instantiate(mazeWallPrefab) as MazeWall;
		mazeWalls[x, z + 1] = newWall;
		newWall.name = "(" + x + ", " + z + ") - " + name;
		newWall.transform.parent = transform;
		newWall.transform.localPosition = new Vector3(offsetX + x - offsetRotationX, 0.5f, offsetZ - z + offsetRotationZ);
		newWall.transform.localRotation = Quaternion.Euler (rotationX, rotationY, rotationZ);
	}




}
