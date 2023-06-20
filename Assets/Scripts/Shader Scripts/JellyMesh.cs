using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class JellyMesh : MonoBehaviour
{
    public float mass = 1f;
    public float stiffness = 1f;
    public float damping = 0.75f;
    [FormerlySerializedAs("intensity")] public float Intensity = 1f;
    private Mesh originalMesh, meshClone;
    private new MeshRenderer renderer;
    private JellyVertex[] jv;
    private Vector3[] vertexArray;
    void Start()
    {
        originalMesh = GetComponent<MeshFilter>().sharedMesh;
        meshClone = Instantiate(originalMesh);
        GetComponent<MeshFilter>().sharedMesh = meshClone;
        renderer = GetComponent<MeshRenderer>();
        jv = new JellyVertex[meshClone.vertices.Length];
        for (int i = 0; i < meshClone.vertices.Length; i++)
            jv[i] = new JellyVertex(i, transform.TransformPoint(meshClone.vertices[i]));
    }
    
    void FixedUpdate()
    {
        vertexArray = originalMesh.vertices;
        for (int i = 0; i < jv.Length; i++)
        {
            Vector3 target = transform.TransformPoint(vertexArray[jv[i].iD]);
           // float intensity = (1 - (renderer.bounds.max.y - target.y) / renderer.bounds.size) * Intensity;
            jv[i].Shake(target, mass, stiffness, damping);
            target = transform.InverseTransformPoint(jv[i].position);
            vertexArray[jv[i].iD] = Vector3.Lerp(vertexArray[jv[i].iD], target, Intensity);
        }
        meshClone.vertices = vertexArray;
    }

    public class JellyVertex
    {
        public int iD;
        public Vector3 position;
        public Vector3 velocity, force;

        public JellyVertex(int id, Vector3 pos)
        {
            iD = id;
            position = pos;
        }

        public void Shake(Vector3 target, float m, float s, float d)
        {
            force = (target - position) * s;
            velocity = (velocity + force / m) * d;
            position += velocity;
            if ((velocity + force + force / m).magnitude < 0.01f)
                position = target;
        }
    }
}
