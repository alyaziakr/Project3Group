using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend1 : MonoBehaviour
{
	public static int totalAmount;

	public Color favoriteColor;
	private string secret = "i like enemy";

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("enemy start");
        totalAmount++;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
