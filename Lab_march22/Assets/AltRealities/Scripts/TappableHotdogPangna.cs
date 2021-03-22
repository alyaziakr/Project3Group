using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TappableHotdogPangna : MonoBehaviour, ITappableObject
{
    public GameObject olive;
    public Light mainLight;

    private Material skyboxMaterial;
    private Color originalSkyTopColor;
    private Color originalSkyHorizontalColor;
    private Color originalSkyBottomColor;

    void Start()
    {
        skyboxMaterial = RenderSettings.skybox;
        originalSkyTopColor = skyboxMaterial.GetColor("_Color1");
        originalSkyHorizontalColor = skyboxMaterial.GetColor("_Color2");
        originalSkyBottomColor = skyboxMaterial.GetColor("_Color3");
    }

    void OnDestroy()
    {
        skyboxMaterial.SetColor("_Color1", originalSkyTopColor);
        skyboxMaterial.SetColor("_Color2", originalSkyHorizontalColor);
        skyboxMaterial.SetColor("_Color3", originalSkyBottomColor);
    }


    public void HandleTouchStart(TapEventData data)
    {
        //
    }

    public void HandleTouchEnd(TapEventData data)
    {
        //
    }

    public void HandleTap(TapEventData data)
    {
        // change skybox color + light + environment color
        Color mainColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        skyboxMaterial.SetColor("_Color1", Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f));
        skyboxMaterial.SetColor("_Color2", mainColor);
        skyboxMaterial.SetColor("_Color3", Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f));
        mainLight.color = mainColor;
        RenderSettings.ambientLight = mainColor;

        LeanTween.moveLocalY(olive, .4f, .5f).setLoopPingPong(1).setEaseInBounce();
    }
}
