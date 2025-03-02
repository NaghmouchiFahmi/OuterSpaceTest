using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourretMovement : MonoBehaviour
{
    void Update()
    {
        if (Input.mousePosition.x < 0 || Input.mousePosition.y < 0 ||
            Input.mousePosition.x > Screen.width || Input.mousePosition.y > Screen.height)
        {
            return;
        }

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y,
            Camera.main.transform.position.y - transform.position.y));

        Vector3 direction = mousePosition - transform.position;
        direction.y = 0; 

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

}
