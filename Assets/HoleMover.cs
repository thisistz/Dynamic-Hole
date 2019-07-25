//enable read/write on mesh import settings before applying script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleMover : MonoBehaviour
{
    [Header("Detection")]
    public float detectionRadius;
    
    [Header("Mesh")]
    public MeshFilter filter;
    public MeshCollider meshCollider;
    Mesh mesh;
    List<int> vertIndexes; 
    List<Vector3> offsets;
    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        vertIndexes = new List<int>();
        offsets = new List<Vector3>();
        mesh = filter.mesh;
        // get vertices on hole rim
        for(int i = 0; i < mesh.vertices.Length; i++){
            float distance = Vector3.Distance(transform.position, mesh.vertices[i]);
            if(distance < detectionRadius){
                vertIndexes.Add(i);
                offsets.Add(mesh.vertices[i] - transform.position);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3[] vertices = mesh.vertices;
        //hole manipulation
        for(int i = 0; i < vertIndexes.Count; i++){

            vertices[vertIndexes[i]] = transform.position + offsets[i].normalized * detectionRadius;

            //make hole big
            // dir = (transform.position - vertices[vertIndexes[i]]).normalized;
            // vertices[vertIndexes[i]] -= dir * Time.deltaTime * 0.5f;

            //move height
            //vertices[vertIndexes[i]].z += Time.deltaTime;
        }

        mesh.vertices = vertices;
        filter.mesh = mesh;
        meshCollider.sharedMesh = mesh;
    }
    

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
