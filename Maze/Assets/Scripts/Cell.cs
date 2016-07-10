public class Cell
{
	public bool north;
	public bool south;
	public bool east;
	public bool west;

	public void removeWall(Direction d)
	{
		switch(d)
		{
		case (Direction.NORTH): north = true; break;
		case (Direction.SOUTH): south = true; break;
		case (Direction.EAST): east = true; break;
		case (Direction.WEST): west = true; break;
		default: break;
		}
	}

	public override string ToString()
	{
		string s = "";
		if (north) { s += "N"; }
		if (south) { s += "S"; }
		if (east) { s += "E"; }
		if (west) { s += "W"; }
		return s;
	}
}