using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] Transform playerCamera;

    [SerializeField] float reach = 3.5f;

    bool pickUp = false;
    bool throwInBin = false;

    [SerializeField] GameObject leftClickUI;
    [SerializeField] GameObject EKeyUI;

    [SerializeField] Animator FullBagWarning;

    void Update()
    {
        //detect inputs
        //pickup
        if (Input.GetButtonDown("Fire1"))
        {
            pickUp = true;
        }
        //throw in bin
        if (Input.GetKeyDown(KeyCode.E))
        {
            throwInBin = true;
        }
    }

    void FixedUpdate()
    {
        PickUp();
        ThrowInBin();
    }

    void PickUp()
    {
        //shoot raycast
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, reach))
        {
            //check if the object can be picked up
            if (hit.transform.gameObject.tag == "CanPickUp")
            {
                //check if input is used
                if (pickUp)
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
                        FullBagWarning.SetTrigger("FullBagWarning");
                    }
                }

                //display input on UI
                leftClickUI.SetActive(true);
            }
            else
            {
                //disable input display on UI
                leftClickUI.SetActive(false);
            }
        }
        else
        {
            //disable input display on UI
            leftClickUI.SetActive(false);
        }

        pickUp = false;
    }

    void ThrowInBin()
    {
        //shoot raycast
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, reach))
        {
            //check if the object is the bin
            if (hit.transform.gameObject.tag == "Bin")
            {
                //check if input is used
                if (throwInBin)
                {
                    //use the bin
                    ScoreCounter.instance.IncreaseTotalScore();                 
                }

                //display input on UI
                EKeyUI.SetActive(true);
            }
            else
            {
                //disable input display on UI
                EKeyUI.SetActive(false);
            }
        }
        else
        {
            //disable input display on UI
            EKeyUI.SetActive(false);
        }

        throwInBin = false;
    }
}
