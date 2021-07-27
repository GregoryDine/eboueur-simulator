using UnityEngine;

public class GarbageTruck : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime);

        if (transform.position.z > 45)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -45);
        }
    }
}
