///-----------------------------------------------------------------
///   Class:       PlayerTwoMovement
///   Description: This class is responsible for handling the movement of player two based upon their input.
///   Author:      Keywi
///-----------------------------------------------------------------

using UnityEngine;

public class PlayerTwoMovement : MonoBehaviour
{
    public float movementSpeed;

    /// <summary>FixedUpdate is a Method in the PlayerTwoMovement Class.
    /// <para>This method gets input from the keyboard or from the second controller and moves the paddle up or down depending on the input.</para>
    /// </summary>
    void FixedUpdate()
    {
        if (Settings.isMultiplePlayers)
        {
            float velocity = 0.0f;
            if (Input.GetJoystickNames().Length <= 1)
            {
                velocity = Input.GetAxisRaw("VerticalKeyboard2");
            }
            else
            {
                float v1 = Input.GetAxisRaw("VerticalController2");
                float v2 = Input.GetAxisRaw("VerticalKeyboard2");
                if (v1 != 0)
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
}
