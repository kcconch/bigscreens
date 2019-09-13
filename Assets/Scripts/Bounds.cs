using System;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    public PhysicMaterial WallMaterial;
    public float WallThickness;

    public readonly GameObject[] Walls = new GameObject[4];

    private void Awake()
    {
        CreateWalls();
    }

    public void CreateWalls()
    {
        var cam = Camera.main;
        if (cam == null)
        {
            throw new Exception("Could not find a camera to create the boundary walls with.");
        }

        if (WallMaterial == null) throw new Exception("Boundaries must have a wall physics material set.");

        if (!cam.orthographic) throw new Exception("Boundaries must be initialized with an orthographic camera.");
        var height = cam.orthographicSize * 2;
        var width = height * cam.aspect;
        // var center = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, cam.farClipPlane));

        var halfThickness = WallThickness / 2;
        var halfHeight = height / 2;
        var halfWidth = width / 2;

        // Top
        Walls[0] = CreateWall("Top", new Vector2(0, halfHeight + halfThickness), new Vector3(width, WallThickness, WallThickness));

        // Right
        Walls[1] = CreateWall("Right", new Vector2(halfWidth + halfThickness, 0), new Vector3(WallThickness, height, WallThickness));

        // Bottom
        Walls[2] = CreateWall("Bottom", new Vector2(0, -halfHeight - halfThickness), new Vector3(width, WallThickness, WallThickness));

        // Left
        Walls[3] = CreateWall("Left", new Vector2(-halfWidth - halfThickness, 0), new Vector3(WallThickness, height, WallThickness));
        
        transform.rotation = cam.transform.rotation;
    }

    private GameObject CreateWall(string wallName, Vector2 pos, Vector3 dimensions)
    {
        var wall = new GameObject(wallName);
        wall.transform.localPosition = pos;
        wall.transform.SetParent(transform, false);

        // Add rigidbody physics to the object
        var body = wall.AddComponent<Rigidbody>();
        // Make sure the
        body.constraints = RigidbodyConstraints.FreezeAll;

        var coll = wall.AddComponent<BoxCollider>();
        coll.size = dimensions;
        coll.sharedMaterial = WallMaterial;
        return wall;
    }

}
