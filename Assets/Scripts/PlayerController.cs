using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
    private Rigidbody rbPlayer;
    private Vector3 direction = Vector3.zero;
    [SerializeField]
    private float forceMultiplier = 1.0f;
    [SerializeField]
    private ForceMode forceMode;
    public GameObject[] spawnPoints;
    
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
        spawnPoints = GameObject.FindGameObjectsWithTag("Respawn");

        Respawn();
    }

    void Update()
    {
        if (!IsLocalPlayer)
        {
            return;
        }

        float horizontalVelocity = Input.GetAxis("Horizontal");
        float verticalVelocity = Input.GetAxis("Vertical");

        direction = new Vector3(horizontalVelocity, 0, verticalVelocity);
    }

    void FixedUpdate()
    {
        if (!IsLocalPlayer)
        {
            return;
        }

        if (IsServer)
        {
            Move(direction);
        }
        else
        {
            MoveRpc(direction);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, direction * 10);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, rbPlayer.velocity * 5);
    }

    private void Move(Vector3 input)
    {
        rbPlayer.AddForce(input * forceMultiplier, forceMode);

        if (transform.position.z > 38)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 38);
        }
        else if (transform.position.z < -38)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -38);
        }
    }

    [Rpc(SendTo.Server)]
    public void MoveRpc(Vector3 input)
    {
        Move(input);
    }

    private void Respawn()
    {
        int index = 0;
        while (Physics.CheckBox(spawnPoints[index].transform.position, new Vector3(1.5f, 1.5f, 1.5f)))
        {
            index++;
        }

        rbPlayer.MovePosition(spawnPoints[index].transform.position);
        rbPlayer.velocity = Vector3.zero;
    }

    void OnTriggerExit(Collider collider)
    {
        if (!IsServer)
        {
            return;
        }

        if (collider.CompareTag("Hazard"))
        {
            Respawn();
        }
    }
}
