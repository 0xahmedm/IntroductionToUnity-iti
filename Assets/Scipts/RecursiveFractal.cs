using UnityEngine;

public class RecursiveFractal : MonoBehaviour
{
    [Header("Fractal Settings")]
    [Range(1, 7)]
    public int fractalCycles = 4;

    [Range(0.1f, 1f)]
    public float scaleFactor = 0.5f;

    private int lastCycles = -1;
    private float lastScale = -1;

    void Update()
    {
        if (fractalCycles != lastCycles || scaleFactor != lastScale)
        {
            Regenerate();
            lastCycles = fractalCycles;
            lastScale = scaleFactor;
        }
    }

    void Regenerate()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
            Destroy(transform.GetChild(i).gameObject);

        GenerateFractal(transform, 1, Vector3.zero);
    }

    void GenerateFractal(Transform parent, int depth, Vector3 cameFrom)
    {
        if (depth > fractalCycles)
            return;

        float currentSize = Mathf.Pow(scaleFactor, depth - 1);

        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.parent = parent;
        cube.transform.localPosition = Vector3.zero;
        cube.transform.localScale = Vector3.one * currentSize;

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

            float childSize = currentSize * scaleFactor;
            float distance = (currentSize / 2f) + (childSize / 2f);

            GameObject holder = new GameObject("Depth_" + (depth + 1));
            holder.transform.parent = parent;
            holder.transform.localPosition = cube.transform.localPosition + dir * distance;

            GenerateFractal(holder.transform, depth + 1, dir);
        }
    }
}
