using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] Transform playerCamera;

    [SerializeField] float reach = 3.5f;

    bool pickup = false;
    bool throwInBin = false;

    void Update()
    {
        //detect inputs
        //pickup
        if (Input.GetButtonDown("Fire1"))
        {
            pickup = true;
        }
        //throw in bin
        if (Input.GetKeyDown(KeyCode.E))
        {
            throwInBin = true;
        }
    }

    void FixedUpdate()
    {
        Pickup();
        ThrowInBin();
    }

    void Pickup()
    {
        if (pickup)
        {
            //shoot raycast
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, reach))
            {
                //check if the object can be picked up
                if (hit.transform.gameObject.tag == "CanPickup")
                {
                    //get object's points value
                    PointsValue pickedUpObject = hit.transform.GetComponent<PointsValue>();
                    

                    if (pickedUpObject.pointsValue + ScoreCounter.instance.currentScore <= 100)
                    {
                        //add points to the Score Counter
                        ScoreCounter.instance.CollectPoints(pickedUpObject.pointsValue);
                        //destroy object
                        Destroy(hit.transform.gameObject);
                    }
                    else
                    {
                        //send warning
                        Debug.Log("Not enough space");
                    }
                }
            }
            pickup = false;
        }
    }

    void ThrowInBin()
    {
        if (throwInBin)
        {
            //shoot raycast
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, reach))
            {
                //check if the object is the bin
                if (hit.transform.gameObject.tag == "Bin")
                {
                    //use the bin
                    ScoreCounter.instance.IncreaseTotalScore();
                }
            }
            throwInBin = false;
        }
    }
}
