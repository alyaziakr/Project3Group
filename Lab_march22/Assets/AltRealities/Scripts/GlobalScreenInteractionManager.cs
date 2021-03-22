using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScreenInteractionManager : Manager<GlobalScreenInteractionManager>,
ITappableObject,
IDraggableObject
{
    public void HandleTouchStart(TapEventData data)
    {
        EventBus.ScreenTouchStarted?.Invoke(data);
    }

    public void HandleTouchEnd(TapEventData data)
    {
        EventBus.ScreenTouchEnded?.Invoke(data);
    }

    public void HandleTap(TapEventData data)
    {
        EventBus.ScreenTapped?.Invoke(data);
    }

    public void HandleDragStart(DragEventData data)
    {
        EventBus.ScreenDragStarted?.Invoke(data);
    }

    public void HandleDragUpdate(DragEventData data)
    {
        EventBus.ScreenDragUpdated?.Invoke(data);
    }

    public void HandleDragEnd(DragEventData data)
    {
        EventBus.ScreenDragEnded?.Invoke(data);
    }
}