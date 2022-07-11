using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateHole : MonoBehaviour
{
    public PolygonCollider2D firstGroundHoleCollider;
    public PolygonCollider2D firstGroundCollider;
    public MeshCollider GeneratedMeshCollider;
    public GameObject forGround, forTinyGround, forSecondGround;
    Mesh GeneratedMesh;
    

    private void Start()
    {
        forGround.GetComponent<MeshCollider>().enabled          = false;
        forTinyGround.GetComponent<MeshCollider>().enabled      = false;
        forSecondGround.GetComponent<MeshCollider>().enabled    = false;
    }
    private void FixedUpdate()
    {

        if(transform.hasChanged  == true)
        {
            transform.hasChanged = false;
            firstGroundHoleCollider.transform.position = new Vector2(transform.position.x, transform.position.z);
            createHole(firstGroundCollider);
            createMeshCollider(firstGroundCollider);
        }
        
    }

    private void  createHole(PolygonCollider2D collider)
    {
        Vector2[] PointPositions = firstGroundHoleCollider.GetPath(0);

        for(int i = 0; i < PointPositions.Length; i++)
        {
            PointPositions[i] += (Vector2)firstGroundHoleCollider.transform.position;
        }

        collider.pathCount = 2;
        collider.SetPath(1, PointPositions);
        
    }

    private void createMeshCollider(PolygonCollider2D collider)
    {
        if (GeneratedMesh != null) Destroy(GeneratedMesh);
        GeneratedMesh = collider.CreateMesh(true,true);
        GeneratedMeshCollider.sharedMesh = GeneratedMesh;
        
    }

}
