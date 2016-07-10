using UnityEngine;
using System.Collections;

public class Maze : MonoBehaviour {


    public MazeWall mazeWallPrefab;
    public MazeFloorCell mazeFloorCellPrefab;
    private MazeFloorCell[,] floorCells;
    private MazeWall[,] mazeWalls;
    public int yConstant = 0;

    //customization params, make sure to set all before 
    //todo: add hallway width
    private int offsetZ;
	private int offsetX;
    private float scaleTweak;
    private float scaleFactor;
    private float rotationOffset;

    private float wallHeightScale = 3.0f;

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

    //TODO scaling of maze size requires scaling of rotation offset

	public void generateWallHorizontal(int x, int z, int sizeX, int sizeZ, string name){
		createMazeWallCell (x, z, 0.0f, 90.0f,0.0f, 0.0f, rotationOffset, scaleTweak, sizeX, sizeZ, name);
	}

	public void generateWallVertical(int x, int z, int sizeX, int sizeZ, string name){
		createMazeWallCell (x, z, 0f, 0.0f,0.0f, rotationOffset, 0.0f, 0.0f, sizeX, sizeZ, name);
	}

	public void generateWallSouthHorizontal(int x, int z, int sizeX, int sizeZ, string name){
		createMazeWallCell (x, z, 0.0f, 90.0f,0.0f, 0.0f, -rotationOffset, scaleTweak, sizeX, sizeZ, name );
	}

	public void generateWallEastVertical(int x, int z, int sizeX, int sizeZ, string name){
		createMazeWallCell (x, z, 0f, 0.0f,0.0f, -rotationOffset, 0.0f, 0.0f, sizeX, sizeZ, name );
	}

    private float scaleToRotationOffset(float scale){
        float offset = scale * 0.5f; 
        return offset;
    }




	public void setOffsetAndScale(int x, int z, float scale){
		offsetX = x;
		offsetZ = z;
        rotationOffset = scaleToRotationOffset(scale);
        scaleFactor = scale;
        scaleTweak = scale * (0.066f) / 2.0f;
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
        newFloorCell.transform.localScale = new Vector3(scaleFactor, 1, scaleFactor);
		newFloorCell.transform.localPosition = new Vector3(offsetX + (x * scaleFactor), 0f, offsetZ - (z * scaleFactor));
	}


	//TODO: horizontal vs vertical
	private void createMazeWallCell(int x, int z, float rotationX, float rotationY, float rotationZ, float offsetRotationX, float offsetRotationZ, float extraScale, int sizeXw, int sizeZw, string name){
		MazeWall newWall = Instantiate(mazeWallPrefab) as MazeWall;
		mazeWalls[x, z + 1] = newWall;
		newWall.name = "(" + x + ", " + z + ") - " + name;
		newWall.transform.parent = transform;
		newWall.transform.localPosition = new Vector3(offsetX + (x * scaleFactor) - offsetRotationX, 1.0f, offsetZ - (z * scaleFactor) + offsetRotationZ);
        newWall.transform.localScale = new Vector3(1, wallHeightScale, scaleFactor + extraScale);
        newWall.transform.localRotation = Quaternion.Euler (rotationX, rotationY, rotationZ);
	}


    // some helpful utlitly methods

    public Vector3 getLocalVectorLocationForCell(int x, int z)
    {
        float xVal = x * scaleFactor;
        float yVal = yConstant * scaleFactor;
        float zVal = z * scaleFactor;
        return new Vector3(xVal,yVal,zVal);
    }

    public Vector3 getGlobalVectorLocationForCell(int x, int z)
    {
        MazeFloorCell cell = floorCells[x, z];
        return cell.transform.TransformPoint(new Vector3(0,0,0));
    }

}