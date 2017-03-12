// A simple tool for creating circles from gameobjects
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode] 
public class RotatorMaker : MonoBehaviour {

    public GameObject blockType;
    public int blockAmount;
    private Vector2 location;
    public float radius;
    private float angle;
    private float o_radius;
    private float o_angle;
    private int o_amount;
    private float angleGap;
    private GameObject blockCopy;
    private List<GameObject> blocks = new List<GameObject>();
	private bool dirty;


    // Use this for initialization
    void Start () {
		dirty = true;
	}
	
	void Update()
	{
		if(dirty)
		{
            RemoveBlocks();
			angle = 0;
            if (blockAmount != 0)
            {
                angleGap = 360 / blockAmount;
                setupBlocks();
            }
            o_angle = angle;
            o_radius = radius;
            o_amount = blockAmount;
			dirty = false;
		}
        if (o_angle != angle || o_radius != radius || o_amount != blockAmount)
            dirty = true;
	}

    void RemoveBlocks()
    {
        foreach (GameObject o in blocks)
        {
            DestroyImmediate(o);
        }
        blocks.Clear();
    }

	//Setting up the blocks in a circle
    void setupBlocks() {
        for (int i = 0; i < blockAmount; i++) 
		{
           float  x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = radius * Mathf.Sin(angle * Mathf.Deg2Rad);
            location = new Vector2(x,y);
            blockCopy = Instantiate(blockType,location,Quaternion.identity) as GameObject;
            blockCopy.transform.parent = this.transform;         
            blocks.Add(blockCopy);
            if (angle < 360){angle += angleGap;}
        }
    }

}
