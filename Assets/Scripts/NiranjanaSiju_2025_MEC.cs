using UnityEngine;
using System.Collections.Generic;

public class NiranjanaSiju_2025_MEC : MonoBehaviour
{
    public GameObject[] flowerPrefabs;
    public Transform center;
    public GameObject onattappan;




    void Start()
    {
        GeneratePookalam();
        onattappan.SetActive(true);
    }

    void GeneratePookalam()
    {
        DrawCircle(flowerPrefabs[4], radius: 10f, count: 150, order: 0);
        DrawFilledCircle(flowerPrefabs[0], 9.7f, 0.4f, order: 0);
        DrawStar(flowerPrefabs[2], outerRadius: 9.7f, innerRadius: 5f, numPoints: 25, spacing: 0.5f, order: 1);
        DrawStar(flowerPrefabs[5], outerRadius: 8.7f, innerRadius: 4f, numPoints: 25, spacing: 0.5f, order: 2);
        DrawStar(flowerPrefabs[3], outerRadius: 7.7f, innerRadius: 6f, numPoints: 25, spacing: 0.5f, rotation: 7f, order: 3);
        DrawStar(flowerPrefabs[2], outerRadius: 6.7f, innerRadius: 5f, numPoints: 25, spacing: 0.5f, rotation: 7f, order: 4);
        DrawStar(flowerPrefabs[5], outerRadius: 5.7f, innerRadius: 4f, numPoints: 25, spacing: 0.5f, rotation: 7f, order: 5);
        DrawStar(flowerPrefabs[4], outerRadius: 4.7f, innerRadius: 3f, numPoints: 25, spacing: 0.5f, order: 6);
        DrawCircle(flowerPrefabs[0], radius: 3.5f, count: 60, order: 7);
        DrawFilledCircle(flowerPrefabs[5], 3.3f, 0.4f, order: 7);
        DrawSquare(flowerPrefabs[3], size: 4.7f, spacing: 0.5f, order: 8);
        DrawSquare(flowerPrefabs[4], size: 4f, spacing: 0.5f, order: 8);
        DrawSquare(flowerPrefabs[4], size: 4.7f, spacing: 0.5f, rotation: 45f, order: 8);
        DrawSquare(flowerPrefabs[3], size: 4f, spacing: 0.5f, rotation: 45f, order: 8);
        DrawCircle(flowerPrefabs[1], radius: 2.2f, count: 50, order: 9);
        DrawCircle(flowerPrefabs[2], radius: 1.8f, count: 35, order: 9);
        DrawFilledCircle(flowerPrefabs[7], 1.7f, 0.4f, order: 9);
        DrawCircle(flowerPrefabs[8], radius: 10.5f, count: 8, order: 0);
    }


    void DrawCircle(GameObject flowerPrefab, float radius, int count, Vector2? offset = null, int order = 0)
    {
        Vector2 off = offset ?? Vector2.zero;
        for (int i = 0; i < count; i++)
        {
            float angle = i * Mathf.PI * 2 / count;
            Vector3 pos = center.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius + (Vector3)off;
            CreateFlower(flowerPrefab, pos, order);
        }
    }

    void DrawStar(GameObject flowerPrefab, float outerRadius, float innerRadius, int numPoints, float spacing, float rotation = 0f, Vector2? offset = null, int order = 0)
    {
        Vector2 off = offset ?? Vector2.zero;
        List<Vector3> starPoints = new List<Vector3>();

        for (int i = 0; i < numPoints * 2; i++)
        {
            float angle = i * Mathf.PI / numPoints + rotation * Mathf.Deg2Rad;
            float radius = (i % 2 == 0) ? outerRadius : innerRadius;
            Vector3 point = center.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius + (Vector3)off;
            starPoints.Add(point);
        }

        for (int i = 0; i < starPoints.Count; i++)
        {
            Vector3 start = starPoints[i];
            Vector3 end = starPoints[(i + 1) % starPoints.Count];
            PlaceFlowersAlongLine(flowerPrefab, start, end, spacing, order);
        }
    }

    void DrawSquare(GameObject flowerPrefab, float size, float spacing, float rotation = 0f, Vector2? offset = null, int order = 0)
    {
        Vector2 off = offset ?? Vector2.zero;

        Vector3[] corners = new Vector3[4];
        corners[0] = new Vector3(-size / 2, -size / 2, 0);
        corners[1] = new Vector3(-size / 2, size / 2, 0);
        corners[2] = new Vector3(size / 2, size / 2, 0);
        corners[3] = new Vector3(size / 2, -size / 2, 0);

        Quaternion rot = Quaternion.Euler(0, 0, rotation);

        for (int i = 0; i < 4; i++)
        {
            Vector3 start = rot * corners[i] + center.position + (Vector3)off;
            Vector3 end = rot * corners[(i + 1) % 4] + center.position + (Vector3)off;
            PlaceFlowersAlongLine(flowerPrefab, start, end, spacing, order);
        }
    }


    void DrawFilledCircle(GameObject prefab, float radius, float spacing, Vector2? offset = null, int order = 0)
    {
        Vector2 off = offset ?? Vector2.zero;

        for (float r = 0; r <= radius; r += spacing)
        {
            int count = Mathf.Max(1, Mathf.CeilToInt(2 * Mathf.PI * r / spacing));

            for (int i = 0; i < count; i++)
            {
                float angle = i * Mathf.PI * 2f / count;
                Vector2 pos = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * r + off;
                CreateFlower(prefab, center.position + (Vector3)pos, order);
            }
        }
    }


    void CreateFlower(GameObject prefab, Vector3 position, int order)
    {
        GameObject flower = Instantiate(prefab, position, Quaternion.identity, center);
        var sr = flower.GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.sortingOrder = order;
    }
    
    void PlaceFlowersAlongLine(GameObject flowerPrefab, Vector3 start, Vector3 end, float spacing, int order = 0)
    {
        float distance = Vector3.Distance(start, end);
        int count = Mathf.CeilToInt(distance / spacing);

        for (int i = 0; i <= count; i++)
        {
            float t = i / (float)count;
            Vector3 position = Vector3.Lerp(start, end, t);
            CreateFlower(flowerPrefab, position, order);
        }
    }


}