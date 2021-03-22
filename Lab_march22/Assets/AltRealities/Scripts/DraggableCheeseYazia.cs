using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableCheeseYazia : MonoBehaviour, IDraggableObject
{
    public float speed = 6f;
    public float maxSpeed = 15f;

    private Vector3 dragOffset;
    private Vector3 targetPosition;
    private Rigidbody rigidbody;
    private bool isDragging;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // move when it's been dragged
        if (rigidbody != null & isDragging)
        {
            // V.1 move kinematic rigidbody
            //rigidbody.MovePosition(targetPosition);

            // V.2
            // Calc velocity necessary to foloow the mouse pointer
            Vector3 vel = (targetPosition - transform.position) * speed;
            // Limit max velocity to avoid pass through objects
            if (vel.sqrMagnitude > maxSpeed * maxSpeed) vel *= (maxSpeed * maxSpeed / vel.sqrMagnitude);
            // Set object's velocity
            rigidbody.velocity = vel;
        }
    }

    public void HandleDragStart(DragEventData data)
    {
        Vector3 startDragScreenPoint = new Vector3(data.position.x, data.position.y, data.startRaycastHit.distance);
        Vector3 startDragPosition = data.raycastCamera.ScreenToWorldPoint(startDragScreenPoint);
        dragOffset = startDragPosition - transform.position;

        //if (rigidbody != null) rigidbody.isKinematic = true;
    }

    public void HandleDragUpdate(DragEventData data)
    {
        Vector3 currentDragScreenPoint = new Vector3(data.position.x, data.position.y, data.startRaycastHit.distance);
        Vector3 currentDragPosition = data.raycastCamera.ScreenToWorldPoint(currentDragScreenPoint);

        targetPosition = currentDragPosition - dragOffset;

        if (rigidbody == null)
            transform.position = targetPosition;

        isDragging = true;
    }

    public void HandleDragEnd(DragEventData data)
    {
        //if (rigidbody != null) rigidbody.isKinematic = false;

        isDragging = false;
    }
}
