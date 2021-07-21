using UnityEngine;

public class PickupObjects : MonoBehaviour
{
    [SerializeField] float pickupReach = 3.0f;

    bool pickup = false;

    void Update()
    {
        //detect input
        if (Input.GetButtonDown("Fire1"))
        {
            pickup = true;
        }
    }

    void FixedUpdate()
    {
        //shoot raycast
        if (pickup)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, pickupReach))
            {
                //detect object tag
                if (hit.transform.gameObject.tag == "10pts")
                {
                    Debug.Log("10 Points !");
                    Destroy(hit.transform.gameObject);
                }
                else if (hit.transform.gameObject.tag == "20pts")
                {
                    Debug.Log("20 Points !");
                    Destroy(hit.transform.gameObject);
                }
                else if (hit.transform.gameObject.tag == "50pts")
                {
                    Debug.Log("50 Points !");
                    Destroy(hit.transform.gameObject);
                }
            }
            pickup = false;
        }
    }
}
