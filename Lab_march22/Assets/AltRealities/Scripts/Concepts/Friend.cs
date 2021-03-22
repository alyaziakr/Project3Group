using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend : MonoBehaviour
{
    public static int totalAmount = 0;

    public int age;
    public Color favoriteColor;

    // a premade delegate you can use without the need of making a custom one.
    public static Action<Color> OnToldSecret;

    private string secret = "I like Enemy";

    // Start is called before the first frame update
    void Start()
    {
        totalAmount++;
    }

    public string GetSecret()
    {
        OnToldSecret?.Invoke(favoriteColor);
        return secret;
    }

    public string GetSecret(int guessAge)
    {
        if (guessAge == age)
        {
            OnToldSecret?.Invoke(favoriteColor);
            return secret;
        }
        else
        {
            EventBus.ASecretIsKept?.Invoke(Color.yellow);
            return "failed to get secret";
        }
    }
}
