using UnityEngine;

public class RecursiveFractal : MonoBehaviour
{
    [Header("Fractal Settings")]
    [Range(1, 7)]
    public int fractalCycles = 4;

    [Range(0.1f, 1f)]
    public float scaleFactor = 0.5f;

    public float childOffset = 1.5f;

    void Start()
    {
        Regenerate();
    }

    void Regenerate()
    {
        // Clear old fractal
        for (int i = transform.childCount - 1; i >= 0; i--)
            Destroy(transform.GetChild(i).gameObject);

        // Root has no incoming direction
        GenerateFractal(transform, 1, Vector3.zero);
    }

    void GenerateFractal(Transform parent, int depth, Vector3 cameFrom)
    {
        if (depth > fractalCycles)
            return;

        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.parent = parent;
        cube.transform.localPosition = Vector3.zero;
        cube.transform.localRotation = Quaternion.identity;
        cube.transform.localScale =
            Vector3.one * Mathf.Pow(scaleFactor, depth - 1);

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
            // Skip going back where we came from
            if (dir == -cameFrom)
                continue;

            GameObject childHolder = new GameObject("Depth_" + (depth + 1));
            childHolder.transform.parent = cube.transform;
            childHolder.transform.localPosition =
                dir * childOffset * Mathf.Pow(scaleFactor, depth - 1);
            childHolder.transform.localRotation = Quaternion.identity;

            GenerateFractal(childHolder.transform, depth + 1, dir);
        }
    }
}
