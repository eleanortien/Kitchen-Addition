using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Sprite wholeSprite;
    public float screenBottomCoordinate = -3.96f;
    private bool hasBeenShot;
    public AudioClip hitSound;
    private Animator animator;
    public Rigidbody2D rb;
    public float targetSpeed;
    private Collider2D colliderComponent;

    public int healthChange;
    public int scoreChange;
    public string targetType;

    // Start is called before the first frame update
    void Start()
    {
        hasBeenShot = false;
        animator = GetComponent<Animator>();
        targetSpeed = GameController.instance.targetSpeed;
        colliderComponent = GetComponent<Collider2D>();
    }


    public void MakeWhole()
    {
        if (hasBeenShot == false)
        {
            hasBeenShot = true;
            Destroy(colliderComponent);
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (hitSound != null)
            {
                AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position);
            }
            spriteRenderer.sprite = wholeSprite;
            GameController.instance.ScoreChange(scoreChange);

        }
    }

    void Update()
    {
        //Fall past bottom
        if (gameObject.transform.position.y < screenBottomCoordinate)
     {
            if (hasBeenShot == false)
            {
                GameController.instance.ChangeHealth(healthChange);
            }
            GameController.instance.DecreaseTargetNumber();
            Destroy(gameObject);
     }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(0, targetSpeed, 0);
    }
}
