using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShooterState
{
    move, shoot
}
public class ShooterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;
    public ShooterState currentState;
    public int maxBulletsNumber;
    private int currentBulletNumber = 0;

    public float screenTopCoordinate = 3.84f;
    public float screenBottomCoordinate = -3.96f;
    private float height;

    public AudioClip shootSound;
    Animator animator;
    private bool animationBool;

    public Transform firePoint;
    public GameObject sushiBulletPrefab;
    public GameObject pizzaBulletPrefab;
    public string bulletType;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        height = GetComponent<SpriteRenderer>().bounds.size.y;
        ChangeType("sushi");
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == ShooterState.shoot)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        //Movement Input
        movement.y = Input.GetAxisRaw("Vertical");
        //Check for shoot
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
            animator.SetTrigger("shoot");
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeType("sushi");
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeType("pizza");
        }
    }

    void FixedUpdate()
    {
        //Movement
        movement.Normalize();
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        //Boundaries
        if (gameObject.transform.position.y > screenTopCoordinate)
        {
            gameObject.transform.position = new Vector2(transform.position.x, screenTopCoordinate );
        }
        if (gameObject.transform.position.y  < screenBottomCoordinate)
        {
            gameObject.transform.position = new Vector2(transform.position.x, screenBottomCoordinate );
        }
    }

    private void Shoot()
    {
        if (currentBulletNumber < maxBulletsNumber)
        {
         
            currentBulletNumber += 1;
            if (bulletType == "sushi")
            {
              
                Instantiate(sushiBulletPrefab, firePoint.position, firePoint.rotation);
            }
            else if (bulletType == "pizza")
            {
              
                Instantiate(pizzaBulletPrefab, firePoint.position, firePoint.rotation);
                   
            }
            if (shootSound != null)
            {
                AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position);
            }
            
        }
    }

    public void decreaseBulletNumber ()
    {
        if (currentBulletNumber > 0)
        { currentBulletNumber -= 1; }
    }

    public void ChangeType(string newType)
    {
        bulletType = newType;
    }
}
