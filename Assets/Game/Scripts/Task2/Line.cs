using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private EdgeCollider2D edgeCollider2D;

    private List<Vector2> _pointsList = new List<Vector2>();

    private void Start()
    {
        edgeCollider2D.transform.position -= transform.position;
    }

    public void SetPosition(Vector2 pos)
    {
        if(!CanAppend(pos)) return;

        _pointsList.Add(pos);
        
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount-1 , pos);
        edgeCollider2D.points = _pointsList.ToArray();
    }

    private bool CanAppend(Vector2 pos)
    {
        if (lineRenderer.positionCount == 0) return true;

        return Vector2.Distance(lineRenderer.GetPosition(lineRenderer.positionCount - 1), pos) > DrawManager.RESOLUTION;

    }
}
