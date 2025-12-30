using UnityEngine;
using System.Collections.Generic;

public class GPURecursiveFractal : MonoBehaviour
{
    [Header("Fractal Settings")]
    [Range(1, 12)]
    public int fractalCycles = 6;

    [Range(0.1f, 0.9f)]
    public float scaleFactor = 0.5f;

    Mesh cubeMesh;
    Material material;

    List<Matrix4x4> matrices = new();

    int lastCycles = -1;
    float lastScale = -1f;

    void Start()
    {
        cubeMesh = Resources.GetBuiltinResource<Mesh>("Cube.fbx");

        // Works in Built-in + URP
        material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        material.enableInstancing = true;

        Regenerate();
    }

    void Update()
    {
        if (fractalCycles != lastCycles || !Mathf.Approximately(scaleFactor, lastScale))
        {
            Regenerate();
            lastCycles = fractalCycles;
            lastScale = scaleFactor;
        }

        // GPU draw call
        Graphics.DrawMeshInstanced(
            cubeMesh,
            0,
            material,
            matrices
        );
    }

    void Regenerate()
    {
        matrices.Clear();

        GenerateFractal(
            position: Vector3.zero,
            cameFrom: Vector3.zero,
            depth: 1,
            size: 1f
        );
    }

    void GenerateFractal(
        Vector3 position,
        Vector3 cameFrom,
        int depth,
        float size
    )
    {
        if (depth > fractalCycles)
            return;

        matrices.Add(
            Matrix4x4.TRS(
                position,
                Quaternion.identity,
                Vector3.one * size
            )
        );

        if (depth == fractalCycles)
            return;

        Vector3[] directions =
        {
            Vector3.up,
            Vector3.down,
            Vector3.left,
            Vector3.right,
            Vector3.forward,
            Vector3.back
        };

        foreach (Vector3 dir in directions)
        {
            if (dir == -cameFrom)
                continue;

            float childSize = size * scaleFactor;
            float distance = (size / 2f) + (childSize / 2f);

            GenerateFractal(
                position + dir * distance,
                dir,
                depth + 1,
                childSize
            );
        }
    }
}
