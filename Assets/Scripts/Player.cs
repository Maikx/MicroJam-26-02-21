using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController cC;
    private Loader loader;

    public float speed;
    private float gravity = -9.81f;
    Vector3 velocity;

    private Transform playerCheck;
    private float distanceCheck = 2f;
    public LayerMask EnemyMask;
    public LayerMask EndMask;
    [HideInInspector]public bool isCaught = false;
    [HideInInspector] public bool isOut = false;

    private void Start()
    {
        loader = GameObject.Find("SceneManager").GetComponent<Loader>();
        cC = GetComponent<CharacterController>();
        playerCheck = GameObject.Find("PlayerCheck").GetComponent<Transform>();
    }

    void Update()
    {
        Movement();
        Gravity();
        LoseCondition();
        VictoryCondition();
    }

    /// <summary>
    /// Simple movement using the character controller.
    /// </summary>
    public void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        cC.Move(move * speed * Time.deltaTime);
    }

    /// <summary>
    /// Giving the player gravity.
    /// </summary>
    public void Gravity()
    {
        if(velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        cC.Move(velocity * Time.deltaTime);
    }

    /// <summary>
    /// This is the lose condition, aka when player get caught by the drones.
    /// </summary>
    public void LoseCondition()
    {
        isCaught = Physics.CheckSphere(playerCheck.position, distanceCheck
    , EnemyMask);

        if (isCaught)
            loader.Reload();
    }

    /// <summary>
    /// This is the win condition, aka when player arrives in the green zone.
    /// </summary>
    public void VictoryCondition()
    {
        isOut = Physics.CheckSphere(playerCheck.position, distanceCheck
    , EndMask);

        if (isOut)
            loader.Reload();
    }
}
