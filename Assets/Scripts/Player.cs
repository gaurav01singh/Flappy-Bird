using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 direction;

    public float gravity = -9.8f;
    public float strength = 5f;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        //InvokeRepeating(nameof(AnimateSprites), 0.1f, 0.1f);
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 TempPos = transform.position;
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            direction = Vector3.up * strength;
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                direction = Vector3.up * strength;
            }

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FindFirstObjectByType<GameManager>().Pause();
        }
        

        direction.y += gravity * Time.deltaTime;

        transform.position += direction * Time.deltaTime;
        float rotation = rigidBody.linearVelocity.y * 0.01f;
        if (transform.rotation.z > 0.2f && rotation > 0) rotation = 0;
        else if (transform.rotation.z < -0.2f && rotation < 0) rotation = 0;
        transform.Rotate(0, 0, rotation, Space.Self);

        Vector2 dirc = (Vector2)transform.position-TempPos;
        float angle = Mathf.Clamp(Mathf.Atan2(dirc.y,dirc.x)* Mathf.Rad2Deg,-45,45);
        Quaternion Rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), 150f * Time.deltaTime);
        transform.rotation = Rotation;
    }

    

    private void OnEnable()
    {
        Vector3 position = transform.position;  
        position.y = 0;
        transform.position = position;
        direction = Vector3.zero;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstical")
        {
            FindFirstObjectByType<GameManager>().gameOver();
        }
        else if (collision.gameObject.tag == "Scoring")
        {
            FindFirstObjectByType<GameManager>().increateScore();
        }
    }
}
