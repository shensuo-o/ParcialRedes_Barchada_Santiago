using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Weapon : NetworkBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Transform LHand;
    [SerializeField] private Transform RHand;

    [SerializeField] private GameObject bulletPref;
    [SerializeField] private Transform barrel;
    void  Update()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;  

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletPref, barrel.transform.position, barrel.transform.rotation);
        }

        if (player.HorizontalInput == 1)
        {
            transform.position = RHand.transform.position;
        }
        else if(player.HorizontalInput == -1)
        {
            transform.position = LHand.transform.position;
        }
    }
}
