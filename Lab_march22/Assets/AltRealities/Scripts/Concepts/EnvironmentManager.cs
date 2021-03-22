using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : Manager<EnvironmentManager>
{
    private void OnEnable()
    {
        Friend.OnToldSecret += ChangeEnvColor;
        EventBus.ASecretIsKept += ChangeEnvColor;
    }

    private void OnDisable()
    {
        Friend.OnToldSecret -= ChangeEnvColor;
        EventBus.ASecretIsKept -= ChangeEnvColor;
    }

    void ChangeEnvColor(Color color)
    {
        RenderSettings.ambientLight = color;
    }
}
