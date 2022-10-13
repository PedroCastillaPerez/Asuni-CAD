using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;

public class StackGenerator : EditorWindow
{

    public enum StackShape
    {
        box,
        pyramid

    }

    static GameObject prefab;

    static int numMaterials;

    static Material[] materials;

    static StackShape shape;

    static int boxDepth = 4;
    static int boxWidth = 4;
    static int boxHeight = 4;

    static int pyramidSide;

    static bool createPressed;

    static float distanceX = 0.75f;
    static float distanceY = 1;
    static float distanceZ = 0.75f;

    static float rotationX = -90;
    static float rotationY = 0;
    static float rotationZ = 0;


    static float randomPositionX;
    static float randomPositionY;
    static float randomPositionZ;

    static float randomRotationX;
    static float randomRotationY;
    static float randomRotationZ;

    static float simplifyFactor = 0;
    static int simplifyMaxTries = 3;

    static int stackRows = 1;
    static int stackColumns = 1;
    static float stackSeparation = 20;



    [MenuItem("Tools Pedro/StackGenerator")]
    public static void OpenBuilder()
    {
        EditorWindow w = EditorWindow.GetWindow(typeof(StackGenerator));
        w.minSize = new Vector2(60, 600);


    }
    public void OnGUI()
    {
        bool check = true;

        if(materials == null) { materials = new Material[0]; }

        EditorGUILayout.LabelField("Stacks set");

        EditorGUILayout.HelpBox("To generate multiple stacks select more than one row or column", MessageType.None);        

        EditorGUILayout.Space();        

        stackRows = EditorGUILayout.IntField("StackRows", stackRows);
        stackColumns = EditorGUILayout.IntField("StackColumns", stackColumns);
        stackSeparation = EditorGUILayout.FloatField("StackSeparation", stackSeparation);

        EditorGUILayout.Space();        

        EditorGUILayout.LabelField("Stack shape");

        EditorGUILayout.Space();        

        shape = (StackShape)EditorGUILayout.EnumPopup("Shape", shape);

        EditorGUILayout.Space();

        if(shape == StackShape.box)
        {
            boxWidth = EditorGUILayout.IntField("Width", boxWidth);
            boxDepth = EditorGUILayout.IntField("Depth", boxDepth);
            boxHeight = EditorGUILayout.IntField("Height", boxHeight);
        }
        else if(shape == StackShape.pyramid)
        {
            pyramidSide = EditorGUILayout.IntField("Side", pyramidSide);

        }

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Stack elements");

        EditorGUILayout.Space();

        prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), false);

        if(prefab == null)
        {
            EditorGUILayout.HelpBox("You must set a prefab", MessageType.Warning);
            check = false;

        }        

        EditorGUILayout.Space();

        distanceX = EditorGUILayout.FloatField("DistanceX", distanceX);
        distanceY = EditorGUILayout.FloatField("DistanceY", distanceY);
        distanceZ = EditorGUILayout.FloatField("DistanceZ", distanceZ);        

        EditorGUILayout.Space();

        EditorGUILayout.HelpBox("A random material will be chosen for each stack element", MessageType.None);        


        numMaterials = EditorGUILayout.IntField("NumMaterials", numMaterials);

        if(numMaterials == 0)
        {
            EditorGUILayout.HelpBox("You must at least set one material", MessageType.Warning);
            check = false;
        }


        if(materials.Length != numMaterials) { materials = new Material[numMaterials]; }

        for(int i = 0; i < numMaterials; i ++)
        {
            materials[i] = (Material)EditorGUILayout.ObjectField(String.Format("Material{0:0}", i), materials[i], typeof(Material), true);

        }

        EditorGUILayout.Space();      

        randomPositionX = EditorGUILayout.FloatField("RandomPositionX", randomPositionX);
        randomPositionY = EditorGUILayout.FloatField("RandomPositionY", randomPositionY);
        randomPositionZ = EditorGUILayout.FloatField("RandomPositionZ", randomPositionZ);

        EditorGUILayout.Space();

        randomRotationX = EditorGUILayout.FloatField("RandomRotationX", randomRotationX);
        randomRotationY = EditorGUILayout.FloatField("RandomRotationY", randomRotationY);
        randomRotationZ = EditorGUILayout.FloatField("RandomRotationZ", randomRotationZ);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Stack simplification");

        EditorGUILayout.Space();

        EditorGUILayout.HelpBox("Set a simplification factor greater than zero to randomly remove some elements from the top of each stack. As randomization can fail with current algorithm a retry limit is required", MessageType.None);

        EditorGUILayout.Space();

        simplifyFactor = EditorGUILayout.FloatField("SimplifyFactor", simplifyFactor);
        simplifyMaxTries = EditorGUILayout.IntField("SimplifyMaxTries", simplifyMaxTries);

        EditorGUILayout.Space();



        if (GUILayout.Button("Generate"))
        {
            if(check)
            {
                for(int row = 0; row < stackRows; row ++)
                {
                    for(int column = 0; column < stackColumns; column ++)
                    {
                        GenerateStack(column * stackSeparation, row * stackSeparation);
                    }
                }
            }
            else
            {
                EditorUtility.DisplayDialog("Stack builder", "You must set some parameters to generate the stack. Check the warnings.", "Ok");
            }


        }

    }

    void GenerateStack(float positionX, float positionZ)
    {
        GameObject parent = new GameObject("Stack");

        GameObject[][][] grid;
        int numObjects = 0;

        if (shape == StackShape.box)
        {
            grid = CreateGrid(boxWidth, boxDepth, boxHeight);

            Quaternion baseRotation = Quaternion.Euler(rotationX, rotationY, rotationZ);

            for (int iX = 0; iX < boxWidth; iX++)
            {
                for (int iZ = 0; iZ < boxDepth; iZ++)
                {
                    for (int iY = 0; iY < boxHeight; iY++)
                    {
                        GameObject go;
                        Vector3 basePosition = Vector3.right * distanceX * iX + Vector3.forward * distanceZ * iZ + Vector3.up * distanceY * iY;
                        Vector3 position = RandomizePosition(basePosition);
                        Quaternion rotation = Quaternion.Euler(RandomizeRotation(baseRotation.eulerAngles));
                        go = GameObject.Instantiate(prefab, position, rotation);
                        grid[iY][iX][iZ] = go;
                        go.transform.parent = parent.transform; 

                        if(numMaterials > 0) { go.GetComponent<Renderer>().sharedMaterial = materials[UnityEngine.Random.Range(0, numMaterials)]; }
                            
                    }

                }
            }

            numObjects = boxWidth * boxHeight * boxDepth;
        }
        else // shape == StackShape.pyramid
        {
            grid = CreateGrid(pyramidSide, pyramidSide, pyramidSide);

            Quaternion baseRotation = Quaternion.Euler(rotationX, rotationY, rotationZ);

            int i1 = 0;
            int i2 = pyramidSide - 1;

            while (i1 <= i2)
            {
                for (int iX = i1; iX <= i2; iX++)
                {
                    for (int iZ = i1; iZ <= i2; iZ++)
                    {
                        GameObject go;
                        Vector3 basePosition = Vector3.right * distanceX * iX + Vector3.forward * distanceZ * iZ + Vector3.up * distanceY * i1;
                        Vector3 position = RandomizePosition(basePosition);
                        Quaternion rotation = Quaternion.Euler(RandomizeRotation(baseRotation.eulerAngles));
                        go = GameObject.Instantiate(prefab, position, rotation);
                        grid[i1][iX][iZ] = go;
                        go.transform.parent = parent.transform;                             
                        if(numMaterials > 0) { go.GetComponent<Renderer>().sharedMaterial = materials[UnityEngine.Random.Range(0, numMaterials)]; }

                        numObjects ++; // Probablemente hay una fórmula para calcular el total directamente

                    }

                }

                i1++;
                i2--;

            }

        }

        if(simplifyFactor > 0)
        {
            SimplifyGrid(grid, (int)(simplifyFactor * numObjects), simplifyMaxTries);
        }

        parent.transform.position = new Vector3(positionX, 0, positionZ);

    }

    void SimplifyGrid(GameObject[][][] grid, int amount, int maxTries)
    {
        for(int i = 0; i < amount; i ++)
        {
            int tries = 0;
            bool done = false;

            while (tries < maxTries && !done)
            {
                int posX;
                int posZ;

                posX = UnityEngine.Random.Range(0, grid[0].Length);
                posZ = UnityEngine.Random.Range(0, grid[0][0].Length);

                int posY = grid.Length - 1;


                while(!done && posY >= 0)
                {
                    if(grid[posY][posX][posZ] != null)
                    {
                       UnityEngine.Object.DestroyImmediate(grid[posY][posX][posZ]); 
                       grid[posY][posX][posZ] = null;

                       done = true;
                    }
                    else
                    {
                        posY --;
                    }

                }

                tries ++;
            }

        }

    }



    Vector3 RandomizePosition(Vector3 position)
    {
        position += Vector3.right * UnityEngine.Random.Range(-randomPositionX / 2, randomPositionX / 2);
        position += Vector3.up * UnityEngine.Random.Range(-randomPositionY / 2, randomPositionY / 2);
        position += Vector3.forward * UnityEngine.Random.Range(-randomPositionZ / 2, randomPositionZ / 2);

        return position;
    }

    Vector3 RandomizeRotation(Vector3 rotation)
    {
        rotation += Vector3.right * UnityEngine.Random.Range(-randomRotationX / 2, randomRotationX / 2);
        rotation += Vector3.up * UnityEngine.Random.Range(-randomRotationY / 2, randomRotationY / 2);
        rotation += Vector3.forward * UnityEngine.Random.Range(-randomRotationZ / 2, randomRotationZ / 2);

        return rotation;

    }

    GameObject [][][] CreateGrid(int width, int depth, int height)
    {
        GameObject[][][] grid = new GameObject[height][][];
        for (int i = 0; i < height; i++)
        {
            grid[i] = new GameObject[width][];
            for (int j = 0; j < width; j++)
            {
                grid[i][j] = new GameObject[depth];
            }
        }

        return grid;
    }
}
