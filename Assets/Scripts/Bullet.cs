using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float LifeTime;
    [SerializeField] private float Damage;
    [SerializeField] private int Layer;
    void Update()
    {
        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0)
        {
            Destroy(this.gameObject);
        }
        transform.position += transform.right * Speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == Layer)
        {
            collision.gameObject.GetComponent<Player>().HP -= Damage;
            Destroy(this.gameObject);
        }
    }
}
