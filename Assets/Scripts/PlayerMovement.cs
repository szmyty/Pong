///-----------------------------------------------------------------
///   Class:       PlayerMovement
///   Description: This class is responsible for handling the movement of player one based upon their input.
///   Author:      Keywi
///-----------------------------------------------------------------


using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float movementSpeed;

    /// <summary>FixedUpdate is a Method in the PlayerMovement Class.
    /// <para>This method gets input from the keyboard or from the first controller and moves the paddle up or down depending on the input.</para>
    /// </summary>
    void FixedUpdate () {
        float velocity = 0.0f;
        if(Input.GetJoystickNames().Length == 0)
        {
            velocity = Input.GetAxisRaw("VerticalKeyboard");
        }
        else
        {
            float v1 = Input.GetAxisRaw("VerticalController");
            float v2 = Input.GetAxisRaw("VerticalKeyboard");
            if(v1 != 0)
            {
                velocity = v1;
            }
            else
            {
                velocity = v2;
            }
        }
        
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, velocity) * movementSpeed;
	}
}
