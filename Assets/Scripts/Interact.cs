using UnityEngine;

public class Interact : MonoBehaviour
{
    [HideInInspector] public bool canInteract;

    [SerializeField] Transform playerCamera;

    [SerializeField] float reach = 3.5f;

    [SerializeField] GameObject leftClickUI;
    [SerializeField] GameObject EKeyUI;

    [SerializeField] Animator FullBagWarning;

    public static Interact instance;

    void Awake()
    {
        //create instance for the script
        if (instance != null)
        {
            Debug.LogWarning("There is multiple Interact instances!");
            return;
        }

        instance = this;
    }

    void Start()
    {
        canInteract = true;
    }

    void Update()
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
                if (Input.GetButtonDown("Fire1") && canInteract)
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
                if (Input.GetKeyDown(KeyCode.E) && canInteract)
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
    }
}
