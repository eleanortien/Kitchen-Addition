using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 20f;
    public Rigidbody2D rb;
    public float screenRightBoundary;
    private ShooterController shooter;
    public string bulletType;
  

    void Start()
    {
        rb.velocity = transform.right * bulletSpeed;
        shooter = FindObjectOfType<ShooterController>().GetComponent<ShooterController>();
    }


    // Update is called once per frame
    void Update()
    {
        // decrease our life timer
        if (gameObject.transform.position.x > screenRightBoundary)
        {
            shooter.decreaseBulletNumber();
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Target target = hitInfo.GetComponent<Target>();
        if (target != null && target.targetType == bulletType)
        {
           target.MakeWhole();
        }
        shooter.decreaseBulletNumber();
        Destroy(gameObject);
    }
}
