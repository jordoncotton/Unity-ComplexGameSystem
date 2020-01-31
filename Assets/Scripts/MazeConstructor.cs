using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeConstructor : MonoBehaviour
{
    public bool showDebug;
    private MazeDataGenerator dataGenerator;
    private MazeMeshGenerator meshGenerator;

    [SerializeField] private Material mazeMat1;
    [SerializeField] private Material mazeMat2;
    [SerializeField] private Material startMat;
    [SerializeField] private Material treasureMat;

    public int[,] data
    {
        get; private set;
    }

	void Awake()
    {
        dataGenerator = new MazeDataGenerator();
        meshGenerator = new MazeMeshGenerator();

        data = new int[,]
        {
            {1, 1, 1 },
            {1, 0, 1 },
            {1, 1, 1 }
        }; 
    }

    private void DisplayMaze()
    {
        GameObject go = new GameObject();
        go.transform.position = Vector3.zero;
        go.name = "Procedural Maze";
        go.tag = "Generated";

        MeshFilter mf = go.AddComponent<MeshFilter>();
        mf.mesh = meshGenerator.FromData(data);

        MeshCollider mc = go.AddComponent<MeshCollider>();
        mc.sharedMesh = mf.mesh;

        MeshRenderer mr = go.AddComponent<MeshRenderer>();
        mr.materials = new Material[2] { mazeMat1, mazeMat2 };
    }

    public void GenerateNewMaze(int sizeRows, int sizeCols)
    {
        if (sizeRows % 2 == 0 && sizeCols % 2 == 0)
        {
            Debug.LogError("Odd numbers work better for dungeon size.");
        }
        data = dataGenerator.FromDimensions(sizeRows, sizeCols);

        DisplayMaze();
    }

    void OnGUI()
    {
        if (!showDebug)
        {
            return;
        }

        int[,] maze = data;
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        string msg = "";

        for (int i = rMax; i >= 0; i--)
        {
            for (int j = 0; j <= cMax; j++)
            {
                if (maze[i, j] == 0)
                {
                    msg += "....";
                }
                else
                {
                    msg += "==";
                }
            }
            msg += "\n";
        }
        GUI.Label(new Rect(20, 20, 500, 500), msg);
    }
}
