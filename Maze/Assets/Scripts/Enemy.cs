using UnityEngine;
using System.Collections;

public enum EnemyType
{
    EYE
}


public class Enemy : MonoBehaviour {

    //What all enemies have
    public Vector3 startLocation;
    public Vector3 location;

    public EnemyType type;
    public int id;
    public Enemy prefab;

    //movement script

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
    //what all enemies do:
    //
    //

	void Update () {}
}
