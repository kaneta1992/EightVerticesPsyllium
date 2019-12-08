using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePsyllium : MonoBehaviour
{
    public Material material;
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = new GameObject();
        obj.transform.SetParent(this.transform, false);
        obj.name = "Psyllium";
        MeshFilter filter = obj.AddComponent<MeshFilter>();
        MeshRenderer renderer = obj.AddComponent<MeshRenderer>();
        MeshCollider collider = obj.AddComponent<MeshCollider>();

        float height = 0.75f;
        float width = 0.35f;
        float radius = width;
        Mesh mesh = new Mesh ();
        mesh.vertices = new Vector3[] {
            new Vector3 (-width, -height, 0),  // 0
            new Vector3 (-width, -height, 0),  // 1
            new Vector3 (width , -height, 0),  // 2
            new Vector3 (width , -height, 0),  // 3

            new Vector3 (-width,  height, 0),  // 4
            new Vector3 ( width,  height, 0),  // 5

            new Vector3 (-width,  height, 0),  // 6 
            new Vector3 (width ,  height, 0),  // 7
        };

        mesh.uv = new Vector2[] {
            new Vector2 (0, 0),
            new Vector2 (0, 0.5f),
            new Vector2 (1, 0),
            new Vector2 (1, 0.5f),
            new Vector2 (0, 0.5f),
            new Vector2 (1, 0.5f),
            new Vector2 (0, 1),
            new Vector2 (1, 1),
        };

        mesh.uv2 = new Vector2[] {
            new Vector2 (-radius, 0),
            new Vector2 (0, 0),
            new Vector2 (-radius, 0),
            new Vector2 (0, 0),
            new Vector2 (0, 0),
            new Vector2 (0, 0),
            new Vector2 (radius, 0),
            new Vector2 (radius, 0),
        };

        mesh.triangles = new int[] {
            0, 1, 2, 
            1, 3, 2,
            1, 4, 3, 
            4, 5, 3,
            4, 6, 5, 
            6, 7, 5,
        };

        material.SetColor("_Color", new Color(1.0f, 0.1f, 0.01f));

        filter.sharedMesh = mesh;
        collider.sharedMesh = mesh;
        renderer.material = material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
