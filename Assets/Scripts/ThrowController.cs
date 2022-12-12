using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ThrowController : MonoBehaviour
{
    public GameObject ball;
    public Rigidbody2D pivot;
    public float timeCutSpringJoin;
    public float timeNewGame;

    private Camera cam;
    private Rigidbody2D ballRigidBody;
    private SpringJoint2D ballSpringJoin;
    private bool isStreching;
    private GameManager gameManager;
    private Vector3 ballStartPos;

    private void Start()
    {
        ballStartPos = ball.transform.position;
        cam = Camera.main;
        ballRigidBody = ball.GetComponent<Rigidbody2D>();
        ballSpringJoin = ball.GetComponent<SpringJoint2D>();

        // Reconnect SpringJoin to pivot
        ballSpringJoin.connectedBody = pivot;

        // Access to the persistent GameManager gameObject
        gameManager = GameObject.Find(nameof(GameManager)).GetComponent<GameManager>();

        Debug.Log(gameManager.PlayScore);
    }

    private void Update()
    {
        // If the ball hasn't RigidBody, don't do anything
        if (ballRigidBody == null)
            return;

        // If not touching the screen, it means that you just throw
        if (!Touchscreen.current.primaryTouch.press.isPressed)
        {
            // If it's streching
            if (isStreching)
                ThrowBall();

            // Update isStreching to false
            isStreching = false;

            return;
        }

        // If is touchinh the screen for the first time
        isStreching = true;

        // Take the ball control with his RigidBody
        ballRigidBody.isKinematic = true;

        // Get touch position
        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

        // Get position from the world
        Vector3 worldPosition = cam.ScreenToWorldPoint(touchPosition);

        // Apply world position to ball RigidBody
        ballRigidBody.position = worldPosition;
    }

    private void ThrowBall()
    {
        // Get back the RigidBody control to the ball
        ballRigidBody.isKinematic = false;
        ballRigidBody = null;

        // Apply time to deactivate SpringJoing
        Invoke(nameof(CutSpringJoin), timeCutSpringJoin);
    }

    private void CutSpringJoin()
    {
        ballSpringJoin.enabled = false;
        //ballSpringJoin = null;

        // Time to new game
        Invoke(nameof(RestartGame), timeNewGame);
    }

    private void RestartGame()
    {
        gameManager.playNumber--;
        if (gameManager.playNumber != 0)
        {
            //SceneManager.LoadScene("Level01");
            ball.transform.position = ballStartPos;
            ballRigidBody = ball.GetComponent<Rigidbody2D>();
            ballRigidBody.velocity = Vector2.zero;
            ballRigidBody.isKinematic = true;
            ballSpringJoin.enabled = true;
            isStreching = false;
        }
        else
        {
            // mostrar score
        }
    }
}
