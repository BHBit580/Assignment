using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    public List<GameObject> circleThatCollidedList = new List<GameObject>();
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Circle"))
        {
            circleThatCollidedList.Add(col.gameObject);
        }
    }
}
