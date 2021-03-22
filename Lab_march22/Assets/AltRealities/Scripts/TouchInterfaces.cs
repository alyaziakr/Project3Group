using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITappableObject
{
    void HandleTouchStart(TapEventData data);
    void HandleTouchEnd(TapEventData data);
    void HandleTap(TapEventData data);
}

public interface IDraggableObject
{
    void HandleDragStart(DragEventData data);
    void HandleDragUpdate(DragEventData data);
    void HandleDragEnd(DragEventData data);
}