using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject Bullet;
    public Transform FirePoint;
    bool dirLeft = false, dirRight = false;
    
    private void Update() {
        if (SimpleInput.GetAxis("Horizontal")>0 && !dirRight)
        {
            dirRight = true;
            dirLeft = false;
            RotateFireDirection(0);
        }
        else if (SimpleInput.GetAxis("Horizontal")<0 && !dirLeft)
        {
            dirLeft = true;
            dirRight = false;
            RotateFireDirection(180f);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(Bullet, FirePoint.position, FirePoint.rotation);
    }

    public void RotateFireDirection(float _rot) // to rotate the spawn point because it does not rotate as the player faces the opposite direction (since player uses scale * -1 instead of rotation)
    {
        FirePoint.transform.rotation = Quaternion.Euler(new Vector2(0, _rot));
    }
}
