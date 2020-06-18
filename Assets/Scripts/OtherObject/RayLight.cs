using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RayColor
{
    NONE,
    BLUE,
    YELLOW,
}
[ExecuteInEditMode]
[RequireComponent(typeof(LineRenderer))]
public class RayLight : MonoBehaviour
{
    public RayColor rayColor;

    const int Infinity = 99;

    int maxReflections = 5;
    int currentReflections = 0;

    [SerializeField]
    Vector2 startPoint, direction;
    List<Vector3> Points = new List<Vector3>();
    int defaultRayDistance = 30;

    LineRenderer lr;

    public LayerMask mask;
    public LayerMask refmask;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        Color color = Color.white;
        switch (rayColor)
        {
            case RayColor.BLUE:
                color = Color.blue;
                break;
            case RayColor.YELLOW:
                color = Color.yellow;
                break;
        }
        GetComponent<SpriteRenderer>().color = color;
        lr.startColor = color;
        lr.endColor = color;


    }

    void Update()
    {
        startPoint = this.transform.position;
        direction = startPoint - new Vector2(transform.up.x, transform.up.y);
        var hitData = Physics2D.Raycast(startPoint, (direction - startPoint).normalized, defaultRayDistance, mask);

        currentReflections = 0;
        Points.Clear();
        Points.Add(startPoint);

        if (hitData)
        {
            ReflectFurther(startPoint, hitData);
        }
        else
        {
            Points.Add(startPoint + (direction - startPoint).normalized * Infinity);
        }

        lr.positionCount = Points.Count;
        lr.SetPositions(Points.ToArray());
    }

    void ReflectFurther(Vector2 origin, RaycastHit2D hitData)
    {
        if (currentReflections > maxReflections) return;

        Points.Add(hitData.point);
        currentReflections++;

        hitData.collider.GetComponent<Door>()?.RayDoor(this.rayColor);

        if (1 << hitData.collider.gameObject.layer != refmask)
            return;

        Vector2 inDirection = (hitData.point - origin).normalized;
        Vector2 newDirection = Vector2.Reflect(inDirection, hitData.normal);

        var newHitData = Physics2D.Raycast(hitData.point + (newDirection * 0.0001f), newDirection * 100, defaultRayDistance, mask);
        if (newHitData)
        {
            ReflectFurther(hitData.point, newHitData);
        }
        else
        {
            Points.Add(hitData.point + newDirection * defaultRayDistance);
        }
    }

}
