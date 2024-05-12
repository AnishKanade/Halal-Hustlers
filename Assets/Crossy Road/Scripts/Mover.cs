using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed = 1.0f;
    public float moveDirection = 0;
    public bool parentOnTrigger = true;
    public bool hitBoxOnTrigger = false;
    public GameObject moverObject = null;


    private Renderer renderer = null;
    private bool isVisible = false;

    void Start()
    {
        renderer = moverObject.GetComponent<Renderer>();
    }

    void Update()
    {
        this.transform.Translate(speed * Time.deltaTime, 0, 0);
        IsVisible();
    }

    void IsVisible()
    {
        if (renderer.isVisible) isVisible = true;
        if (!renderer.isVisible && isVisible == true)
        {
            Debug.Log("Remove object, no longer seen by camera");
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (parentOnTrigger)
            {
                Debug.Log("Enter: parent on me");
                other.transform.parent = this.transform;
            }

            if (hitBoxOnTrigger)
            {
                Debug.Log("Enter: got hit, game over");
                other.GetComponent<PlayerController>().GetHit();
            }

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (parentOnTrigger)
            {
                Debug.Log("Exit: parent on me");
                other.transform.parent = null;
            }
        }
    }
}
