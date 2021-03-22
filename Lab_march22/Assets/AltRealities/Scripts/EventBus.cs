using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventBus
{
    // Global screen interactions
    public static Action<TapEventData> ScreenTouchStarted;
    public static Action<TapEventData> ScreenTouchEnded;
    public static Action<TapEventData> ScreenTapped;

    public static Action<DragEventData> ScreenDragStarted;
    public static Action<DragEventData> ScreenDragUpdated;
    public static Action<DragEventData> ScreenDragEnded;

    public static Action<Color> ASecretIsKept;
}