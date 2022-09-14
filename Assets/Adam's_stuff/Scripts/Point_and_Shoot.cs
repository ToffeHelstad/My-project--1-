using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point_and_Shoot : MonoBehaviour
{

    public GameObject crosshairs;
    public GameObject player;

    public float pizzaSpeed = 60f;

    private Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        target = transform.GetComponent<Camera>().ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        crosshairs.transform.position = new Vector3(target.x, target.y);

        Vector3 difference = target - player.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        player.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        if (Input.GetButtonDown(0))
        {
            float distance = difference.magnitude;
            Vector3 direction = difference / distance;
            direction.Normalize();
            firePizza(direction, rotationZ);
        }
    }

    void firePizza(Vector3 direction, float rotationZ)
    {
        GameObject b = Instantiate(pizzaPrefab) as GameObject;
        b.transform.position = player.transform.position;
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        b.GetComponent<Rigidbody>().velocity = direction * pizzaSpeed; 
    }
}
