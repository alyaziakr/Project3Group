using UnityEngine;

public class Enemy : MonoBehaviour
{
    // a private variables, fields.
    private int _experience;

    //Experience is a basic property
    public int Experience
    {
        get
        {
            return _experience;
        }
        set
        {
            _experience = value;
        }
    }

    //Level is a read-only property that converts experience
    //points into the leve of a player automatically
    public int Level
    {
        get
        {
            return _experience / 1000;
        }
    }


    public float age;
    public Color favoriteColor;
    public Friend friend;
    public int guessNumber = 0;


    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(Friend.totalAmount);
        //Debug.Log(friend.secret);
        //string friendSecret = friend.GetSecret(2);
        //Debug.Log(friendSecret);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("g"))
        {
            string friendSecret = friend.GetSecret(guessNumber);
            Debug.Log(friendSecret);
        }
    }
}
