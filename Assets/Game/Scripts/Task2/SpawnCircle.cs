using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnCircle : MonoBehaviour
{
    [SerializeField] private GameObject circlePrefab;
    [SerializeField] private float xSpawnPosition;
    [SerializeField] private float ySpawnPosition;
    [SerializeField] private float minDistanceAmongCircles = 2;
    public List<GameObject> circlesList;
    
    private int mininumNo = 5;
    private int maximumNo = 10;


    private void Start()
    {
        SpawnCircles();
    }

    public void SpawnCircleButton()
    {
        DestoryPreviousCircles();
        SpawnCircles();
    }
    
    private void SpawnCircles()
    {
        int numOfCircles = Random.Range(mininumNo , maximumNo);
        
        for (int i = 0; i < numOfCircles; i++)
        {
            Vector3 spawnPos;

            do
            {
                spawnPos = new Vector3(Random.Range(-xSpawnPosition, xSpawnPosition),
                    Random.Range(-ySpawnPosition, ySpawnPosition), 0);
            }
            while (CheckOverlapping(spawnPos));
            
            
            GameObject circle = Instantiate(circlePrefab, spawnPos, Quaternion.identity); 
            ChangeColor(circle);
            
            circlesList.Add(circle);
        }
    }
    
    
    private void ChangeColor(GameObject circle)
    {
        var renderer = circle.GetComponent<SpriteRenderer>();
        Color targetColor = Random.ColorHSV(0f, 1f, 0.8f, 1f, 0.8f, 1f);
        renderer.material.color = targetColor;
    }
    
    
    
    private bool CheckOverlapping(Vector3 position)
    {
        // Check if the new position overlaps with existing circles
        foreach (GameObject circle in circlesList)
        {
            float distance = Vector3.Distance(position, circle.transform.position);
            if (distance < minDistanceAmongCircles)
            {
                return true; 
            }
        }
        return false; 
    }

    private void DestoryPreviousCircles()
    {
        foreach (GameObject circle in circlesList)
        {
            Destroy(circle);
        }
        circlesList.Clear();
    }

}
