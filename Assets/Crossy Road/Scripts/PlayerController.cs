/*using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveDistance = 1;
    public float moveTime = 0.4f;
    public float colliderDistCheck = 1.1f;

    public bool isIdle = true;
    public bool isDead = false;
    public bool isMoving = false;
    public bool isJumping = false;
    public bool jumpStart = false;
    public ParticleSystem particle = null;
    public GameObject chick = null;

    private Renderer renderer = null;
    private bool isVisible = false;

    void Start()
    {
       renderer = chick.GetComponent<Renderer>();
    }

    void Update()
    {
        if (!Manager.instance.CanPlay()) return;

        if (isDead) return;
        CanIdle();
        CanMove();
        IsVisible();
    }

    void CanIdle()
    {
        if (isIdle)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) ||
                Input.GetKeyDown(KeyCode.DownArrow) ||
                Input.GetKeyDown(KeyCode.LeftArrow) ||
                Input.GetKeyDown(KeyCode.RightArrow))
            {
                CheckIfCanMove();
            }
        }
    }

    void CheckIfCanMove()
    {
        Physics.Raycast(this.transform.position, -chick.transform.up, out RaycastHit hit, colliderDistCheck);
        Debug.DrawRay(this.transform.position, -chick.transform.up * colliderDistCheck, Color.red, 2);

        if (hit.collider == null || (hit.collider != null && hit.collider.tag != "collider"))
        {
            SetMove();
        }
    }

    void SetMove()
    {
        isIdle = false;
        isMoving = true;
        jumpStart = true;
    }

    void CanMove()
    {
        if (isMoving)
        {
            // Handle movement and rotation
            if (Input.GetKeyUp(KeyCode.LeftArrow)) { MoveAndRotate(Vector3.left); }
            else if (Input.GetKeyUp(KeyCode.RightArrow)) { MoveAndRotate(Vector3.right); }
            else if (Input.GetKeyUp(KeyCode.UpArrow)) { MoveAndRotate(Vector3.forward); }
            else if (Input.GetKeyUp(KeyCode.DownArrow)) { MoveAndRotate(Vector3.back); }
        }
    }

    void MoveAndRotate(Vector3 direction)
    {
        Vector3 newPosition = transform.position + direction * moveDistance;
        LeanTween.move(gameObject, newPosition, moveTime).setOnComplete(MoveComplete);

        // Rotate the player to face the direction it's moving
        transform.rotation = Quaternion.LookRotation(direction);
    }

    void MoveComplete()
    {
        isIdle = true;
        isMoving = false;
        isJumping = false;
        jumpStart = false;
    }

    void IsVisible()
    {
        *//*if (renderer.isVisible) isVisible = true;
        if (!renderer.isVisible && isVisible == true)
        {
            Debug.Log("Player offscreen");
            GetHit();
        }*//*
    }

    public void GetHit()
    {
        Manager.instance.GameOver();
        isDead = true;
        ParticleSystem.EmissionModule em = particle.emission;
        em.enabled = true;
    }

    // Detect collisions with water
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Log"))
        {
            Debug.Log("log detected");
        }


        if (other.CompareTag("Water"))
        {
            Debug.Log("water detected");
            GetHit(); // Call the GetHit function to trigger game over
        }
        
    }
   
}
*/
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveDistance = 1;
    public float moveTime = 0.4f;
    public float colliderDistCheck = 1.1f;

    public bool isIdle = true;
    public bool isDead = false;
    public bool isMoving = false;
    public bool isJumping = false;
    public bool jumpStart = false;
    public ParticleSystem particle = null;

    private Renderer renderer = null;
    private bool isVisible = false;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (!Manager.instance.CanPlay()) return;

        if (isDead) return;
        CanIdle();
        IsVisible();
    }

    void CanIdle()
    {
        if (isIdle)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) ||
                Input.GetKeyDown(KeyCode.DownArrow) ||
                Input.GetKeyDown(KeyCode.LeftArrow) ||
                Input.GetKeyDown(KeyCode.RightArrow))
            {
                CheckIfCanMove();
            }
        }
    }

    void CheckIfCanMove()
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.UpArrow))
            direction = Vector3.forward;
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            direction = Vector3.back;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            direction = Vector3.left;
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            direction = Vector3.right;

        Vector3 newPosition = transform.position + direction * moveDistance;

        // Check if newPosition is valid
        RaycastHit hit;
        Physics.Raycast(newPosition, -transform.up, out hit, colliderDistCheck);
        if (hit.collider == null || (hit.collider != null && hit.collider.tag != "collider"))
        {
            // Move the player
            transform.position = newPosition;

            // Update state variables
            isIdle = false;
            isMoving = true;

            // Rotate the player to face the direction it's moving
            transform.rotation = Quaternion.LookRotation(direction);

            // Start the move complete coroutine
            StartCoroutine(MoveComplete());

            // Update distance count
            Manager.instance.UpdateDistanceCount();
        }
    }

    IEnumerator MoveComplete()
    {
        yield return new WaitForSeconds(moveTime);
        isIdle = true;
        isMoving = false;
    }

    void IsVisible()
    {
        /*if (renderer.isVisible) isVisible = true;
        if (!renderer.isVisible && isVisible == true)
        {
            Debug.Log("Player offscreen");
            GetHit();
        }*/
    }

    public void GetHit()
    {
        Manager.instance.GameOver();
        isDead = true;
        ParticleSystem.EmissionModule em = particle.emission;
        em.enabled = true;
    }

    // Detect collisions with water
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Log"))
        {
            Debug.Log("log detected");
        }

        if (other.CompareTag("Water"))
        {
            Debug.Log("water detected");
            GetHit(); // Call the GetHit function to trigger game over
        }
    }
}
