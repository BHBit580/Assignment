using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    [SerializeField] private Line linePrefab;

    public const float RESOLUTION = 0.1f;

    private Line _currentLine;
    

    private void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if(Input.GetMouseButtonDown(0)) _currentLine = Instantiate(linePrefab, mousePosition, Quaternion.identity);
        
        if(Input.GetMouseButton(0)) _currentLine.SetPosition(mousePosition);

        if (Input.GetMouseButtonUp(0))
        {
             DestroyCircles(_currentLine.GetComponentInChildren<DetectCollisions>().circleThatCollidedList);
        }
    }

    private void DestroyCircles(List<GameObject> circleList)
    {
        foreach (GameObject circle in circleList)
        {
            Destroy(circle);
        }
        circleList.Clear();
        Destroy(_currentLine.gameObject);
    }
}
