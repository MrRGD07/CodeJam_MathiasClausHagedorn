using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] float leftSideOfScreen;
    [SerializeField] float rightSideOfScreen;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        leftSideOfScreen = Camera.main.transform.position.x - Camera.main.orthographicSize * Screen.width / Screen.height;
        rightSideOfScreen = Camera.main.transform.position.x + Camera.main.orthographicSize * Screen.width / Screen.height;
    }

    private void FixedUpdate()
    {
        Movement(GetPhoneRotation());
        RotatePlayer(GetPhoneRotation());
    }

    public float GetPhoneRotation()
    {
        return Input.acceleration.x;
    }

    public void RotatePlayer(float input)
    {
        if (!GameManager.instance.GetGameStatus()) return;
        if (input > 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(0, 0, -50 * input, 100), Time.deltaTime * rb.velocity.x);
        }
        else if(input < 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(0, 0, 50 * -input, 100), Time.deltaTime * -rb.velocity.x);
        }
    }

    public void Movement(float input)
    {
        if (transform.position.x > leftSideOfScreen && GetPhoneRotation() < 0)
        {
            rb.velocity = new Vector2(speed * input, 0);
        }
        else if (transform.position.x < rightSideOfScreen && GetPhoneRotation() > 0)
        {
            rb.velocity = new Vector2(speed * input, 0);
        }
        else rb.velocity = Vector2.right * 0;
    }
}
