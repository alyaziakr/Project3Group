using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TapEventData
{
    public Vector2 position;
    public RaycastHit hit;
}

public class DragEventData
{
    public Vector2 position;
    public Vector2 startPosition;
    public RaycastHit startRaycastHit;
    public Camera raycastCamera;
}

public class InteractionManager : MonoBehaviour
{
    private static InteractionManager _instance;
    public static InteractionManager Instance
    {
        get { return _instance; }
    }

    public LayerMask interactableLayer;
    public float dragThreshold = 10f;

    private Camera mainCamera;
    private bool isDragging;

    private ITappableObject initialTapObject;
    private IDraggableObject initialDragObject;

    private RaycastHit initialDragRaycastHit;
    private Vector2 initialDragPosition;


    private void Awake()
    {
        // Singleton pattern: make sure there's only one instance of InteractionManager exists
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
#if UNITY_EDITOR
        // Simulate touch with mouse in editor
        if (Input.GetMouseButtonDown(0))    // first frame down
        {
            HandleSingleTouch(Input.mousePosition, TouchPhase.Began);
        }
        if (Input.GetMouseButton(0))    // when it's down
        {
            HandleSingleTouch(Input.mousePosition, TouchPhase.Moved);
        }
        if (Input.GetMouseButtonUp(0))  // first frame up
        {
            HandleSingleTouch(Input.mousePosition, TouchPhase.Ended);
        }
#endif
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            HandleSingleTouch(touch.position, touch.phase);
        }
    }

    private void HandleSingleTouch(Vector2 position, TouchPhase phase)
    {
        // Raycast
        Ray ray = mainCamera.ScreenPointToRay(position);
        RaycastHit hit;
        GameObject hitObject = null;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactableLayer))
        {
            hitObject = hit.transform.gameObject;
            //Debug.Log("hit " + hitObject);
        }

        // Tap event recognition
        if (phase == TouchPhase.Began)
        {
            initialTapObject = FindTappableObject(hitObject);
            initialTapObject.HandleTouchStart(new TapEventData
            {
                position = position,
                hit = hit
            });
        }
        else if (phase == TouchPhase.Ended)
        {
            TapEventData tapEventData = new TapEventData
            {
                position = position,
                hit = hit
            };

            initialTapObject.HandleTouchEnd(tapEventData);

            ITappableObject endObject = FindTappableObject(hitObject);
            if (endObject == initialTapObject) endObject.HandleTap(tapEventData);
        }



        // Drag event recognition
        if (phase == TouchPhase.Began)
        {
            initialDragPosition = position;
            initialDragObject = FindDraggableObject(hitObject);
            initialDragRaycastHit = hit;
        }
        else if (phase == TouchPhase.Moved)
        {
            if (!isDragging)
            {
                // Decide if the drag starts
                isDragging = (position - initialDragPosition).magnitude > dragThreshold;
                if (isDragging)
                {
                    Debug.Log("Drag start");
                    Debug.Log("Raycast hit " + initialDragObject + ", distance: " + hit.distance);
                    initialDragObject.HandleDragStart(new DragEventData
                    {
                        position = position,
                        startPosition = position,
                        startRaycastHit = initialDragRaycastHit,
                        raycastCamera = mainCamera
                    });
                }
            }
            else
            {
                // Decide if the drag updates
                initialDragObject.HandleDragUpdate(new DragEventData
                {
                    position = position,
                    startPosition = initialDragPosition,
                    startRaycastHit = initialDragRaycastHit,
                    raycastCamera = mainCamera
                });
            }
        }
        else if (phase == TouchPhase.Canceled || phase == TouchPhase.Ended)
        {
            if (isDragging)
            {
                isDragging = false;
                initialDragObject.HandleDragEnd(new DragEventData
                {
                    position = position,
                    startPosition = initialDragPosition,
                    startRaycastHit = initialDragRaycastHit,
                    raycastCamera = mainCamera
                });
            }
        }
    }


    private ITappableObject FindTappableObject(GameObject o)
    {
        if (o == null) return GlobalScreenInteractionManager.Instance;

        ITappableObject tappableObject = o.GetComponentInParent<ITappableObject>();
        if (tappableObject != null) return tappableObject;
        else return GlobalScreenInteractionManager.Instance;
    }

    private IDraggableObject FindDraggableObject(GameObject o)
    {
        if (o == null) return GlobalScreenInteractionManager.Instance;

        IDraggableObject draggableObject = o.GetComponentInParent<IDraggableObject>();
        if (draggableObject != null) return draggableObject;
        else return GlobalScreenInteractionManager.Instance;
    }
}