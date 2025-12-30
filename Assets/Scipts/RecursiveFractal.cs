using UnityEngine;

public class RecursiveFractal : MonoBehaviour
{
    [Header("Fractal Settings")]
    [Range(1, 7)]
    public int fractalCycles = 4;

    [Range(0.1f, 1f)]
    public float scaleFactor = 0.5f;

    public float childOffset = 1.5f;

    private int currentDepth = 1;

    void Start()
    {
        GenerateFractal(transform, currentDepth);
    }

    void GenerateFractal(Transform parent, int depth)
    {
        if (depth > fractalCycles)
            return;

        // Create cube
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        cube.transform.parent = parent;
        cube.transform.localPosition = Vector3.zero;
        cube.transform.localRotation = Quaternion.identity;
        cube.transform.localScale = Vector3.one * Mathf.Pow(scaleFactor, depth - 1);

        // Stop recursion at max depth
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
            GameObject childHolder = new GameObject("Depth_" + (depth + 1));
            childHolder.transform.parent = cube.transform;
            childHolder.transform.localPosition =
                dir * childOffset * Mathf.Pow(scaleFactor, depth - 1);
            childHolder.transform.localRotation = Quaternion.identity;

            GenerateFractal(childHolder.transform, depth + 1);
        }
    }
}
