// From https://github.com/psholtz/Puzzles/blob/master/mazes/maze-04/java/Kruskal.java

public class Tree
{

    private Tree _parent = null;

    //
    // Build a new tree object
    //
    public Tree()
    {

    }

    // 
    // If we are joined, return the root. Otherwise, return this object instance.
    //
    public Tree root()
    {
        return _parent != null ? _parent.root() : this;
    }

    // 
    // Are we connected to this tree?
    //
    public bool connected(Tree tree)
    {
        return this.root() == tree.root();
    }

    //
    // Connect to the tree
    //
    public void connect(Tree tree)
    {
        tree.root().setParent(this);
    }

    //
    // Set the parent of the object instance
    public void setParent(Tree parent)
    {
        this._parent = parent;
    }
}
