using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed = 3f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))//左
        {
            rb.AddForce(-transform.right * speed, ForceMode.VelocityChange);
        }

        if (Input.GetKey(KeyCode.D))//右
        {
            rb.AddForce(transform.right * speed, ForceMode.VelocityChange);
        }

        if (Input.GetKey(KeyCode.W))//前
        {
            rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);
        }

        if (Input.GetKey(KeyCode.S))//後
        {
            rb.AddForce(-transform.forward * speed, ForceMode.VelocityChange);
        }

        if (Input.GetMouseButton(0)) //上昇
        {
            rb.AddForce(transform.up * speed, ForceMode.VelocityChange);
        }
        if (Input.GetMouseButton(1)) //下降
        {
            rb.AddForce(-transform.up * speed, ForceMode.VelocityChange);
        }
    }
}
