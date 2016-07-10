using UnityEngine;
using System.Collections.Generic;

public static class GenerateMaze {

    public enum Direction
    {
        UNSET,
        NORTH,
        SOUTH,
        EAST,
        WEST
    }

    private static int dx(Direction d)
    {
        switch(d)
        {
            case (Direction.NORTH): return 0;
            case (Direction.SOUTH): return 0;
            case (Direction.EAST): return 1;
            case (Direction.WEST): return -1;
            default: return 0;
        }
    }

    private static int dy(Direction d)
    {
        switch (d)
        {
            case (Direction.NORTH): return -1;
            case (Direction.SOUTH): return 1;
            case (Direction.EAST): return 0;
            case (Direction.WEST): return 0;
            default: return 0;
        }
    }

    private static Direction opposite(Direction d)
    {
        switch (d)
        {
            case (Direction.NORTH): return Direction.SOUTH;
            case (Direction.SOUTH): return Direction.NORTH;
            case (Direction.EAST): return Direction.WEST;
            case (Direction.WEST): return Direction.EAST;
            default: return 0;
        }
    }

    private class Edge
    {
        public int x;
        public int y;
        public Direction dir;

        public Edge(int x, int y, Direction dir)
        {
            this.x = x;
            this.y = y;
            this.dir = dir;
        }

        public override string ToString()
        {
            return System.Enum.GetName(typeof(Direction), dir) + " (" + x + ", " + y + ")";
        }
    }

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

	public static Cell[,] getMaze(int w, int l)
    {
        int numCells = w * l;
        Tree[,] sets = getSets(w, l);
        HashSet<int> wallsDown = new HashSet<int>();
        List<Edge> walls = getWalls(w, l);
        Shuffle<Edge>(walls);

        Cell[,] grid = new Cell[w, l];

        while (walls.Count > 0)
        {
            Edge wall = walls[0];
            walls.RemoveAt(0);

            int newx = wall.x + dx(wall.dir);
            int newy = wall.y + dy(wall.dir);

            Tree set1 = sets[wall.x, wall.y];
            Tree set2 = sets[newx, newy];
            if (!set1.connected(set2))
            {
                set1.connect(set2);


                if (grid[wall.x, wall.y] == null)
                {
                    grid[wall.x, wall.y] = new Cell();
                }

                if (grid[newx, newy] == null)
                {
                    grid[newx, newy] = new Cell();
                }

                grid[wall.x, wall.y].removeWall(wall.dir);
                grid[newx, newy].removeWall(opposite(wall.dir));
            }
        }

        return grid;
    }

    static List<Edge> getWalls(int length, int width)
    {
        List<Edge> edges = new List<Edge>();

        for (int x = 0; x < length; x++)
        {
            for (int z = 0; z < width; z++)
            {
                if (z > 0)
                {
                    edges.Add(new Edge(x, z, Direction.NORTH));
                }

                if (x > 0)
                {
                    edges.Add(new Edge(x, z, Direction.WEST));
                }
            }
        }

        return edges;
    }

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            int k = (int)(Random.value * n + 1);
            n--;
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static Tree[,] getSets(int length, int height)
    {
        Tree[,] sets = new Tree[length, height];
        for (int x = 0; x < length; x++)
        {
            for (int y = 0; y < height; y++)
            {
                sets[x, y] = new Tree();
            }
        }

        return sets;
    }
}
