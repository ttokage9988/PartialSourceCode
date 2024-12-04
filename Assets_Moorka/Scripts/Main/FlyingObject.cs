using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class FlyingObject : MonoBehaviour
{
    private const float G = 9.9f;
    [SerializeField] float _moveSpeed = 200;
    Rigidbody rb;
    
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //var velocity = new Vector3(0, 0, 0);
        //velocity.x += Input.GetAxis("Horizontal");
        //velocity.y += Input.GetAxis("Vertical"); //↑↓

        //var velocity = transform.position;

        var velocity = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
            velocity.z += 1;
        if (Input.GetKey(KeyCode.A))
            velocity.x -= 1;
        if (Input.GetKey(KeyCode.S))
            velocity.z -= 1;
        if (Input.GetKey(KeyCode.D))
            velocity.x += 1;

        
        if(Input.GetKey(KeyCode.Space)) 
        {
            velocity.y += 1.0f; //front
        }
        if(Input.GetKey(KeyCode.LeftControl))
        {
            velocity.y += -1.0f; //back
        }
        
        rb.velocity = velocity * _moveSpeed * Time.deltaTime;
        //rb.velocity = velocity * 500 * Time.deltaTime + new Vector3(0, G + Random.Range(0.0f, 10.0f), 0) * Time.deltaTime;
        /*
        var originalRotation = rb.rotation;

        
        var zEular = 0.0f;
        if (rb.velocity.x > 0)
        {
            zEular = -5.0f;
        }
        else if (rb.velocity.x < 0)
        {
            zEular = 5.0f;
        }

        var xEular = 0.0f;
        if (rb.velocity.z > 0)
        {
            xEular = 5.0f;
        }
        else if (rb.velocity.z < 0)
        {
            xEular = -5.0f;
        }
        //var eular = new Vector3(xEular, 0, zEular);
        var eular = new Vector3(xEular, this.transform.rotation, zEular);
        rb.rotation = Quaternion.Euler(eular);
        */
    }
}