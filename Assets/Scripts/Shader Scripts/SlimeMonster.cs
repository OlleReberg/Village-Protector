using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SlimeMonster : MonoBehaviour
{
    public float time = 1f;
    public float scale = 1f;
    public float mass = 1f;
    public float stiffness = 1f;
    public float damping = 0.75f;
    public float Intensity = 1f;
    private Mesh originalMesh, meshClone;
    private MeshRenderer renderer;
    private JellyVertex[] jv;
    private Vector3[] vertexArray;
    private Vector3 initialPosition;

    void Start()
    {
        originalMesh = GetComponent<MeshFilter>().sharedMesh;
        meshClone = Instantiate(originalMesh);
        GetComponent<MeshFilter>().sharedMesh = meshClone;
        renderer = GetComponent<MeshRenderer>();
        jv = new JellyVertex[meshClone.vertices.Length];
        for (int i = 0; i < meshClone.vertices.Length; i++)
            jv[i] = new JellyVertex(i, transform.TransformPoint(meshClone.vertices[i]));
        initialPosition = transform.position;
    }

    void FixedUpdate()
    {
        vertexArray = originalMesh.vertices;
        float time = Time.time;
        for (int i = 0; i < jv.Length; i++)
        {
            jv[i].Shake(time, 1f, 0.5f, mass, stiffness, damping);
            Vector3 offsetPosition = transform.InverseTransformPoint(initialPosition);
            vertexArray[jv[i].iD] = transform.InverseTransformPoint(jv[i].position) - offsetPosition;
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

        public void Shake(float time, float scale, float intensity, float mass, float stiffness, float damping)
        {
            float noiseOffsetX = Mathf.PerlinNoise(time, position.y) - 0.5f;
            float noiseOffsetY = Mathf.PerlinNoise(position.x, time) - 0.5f;
            float noiseOffsetZ = Mathf.PerlinNoise(position.x, position.y) - 0.5f;

            Vector3 noiseOffset = new Vector3(noiseOffsetX, noiseOffsetY, noiseOffsetZ) * intensity;
            Vector3 target = position + noiseOffset;

            force = (target - position) * stiffness;
            velocity = (velocity + force / mass) * damping;
            position += velocity;

            if ((velocity + force + force / mass).magnitude < 0.01f)
            {
                position = target;
            }
        }
    }
}