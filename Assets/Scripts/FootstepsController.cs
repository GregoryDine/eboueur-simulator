using UnityEngine;

public class FootstepsController : MonoBehaviour
{
    [SerializeField] AudioClip grassSound, concreteSound, currentSound;
    [SerializeField] float volMin, volMax, pitchMin, pitchMax;
    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (PlayerController.instance.currentHorizontalSpeed > 4 && PlayerController.instance.controller.isGrounded)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.volume = Random.Range(volMin, volMax);
                audioSource.pitch = Random.Range(pitchMin, pitchMax);
                audioSource.PlayOneShot(currentSound);
            }
        }
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 0.1f))
        {
            switch (hit.transform.tag)
            {
                case "Grass":
                    currentSound = grassSound;
                    break;

                case "Concrete":
                    currentSound = concreteSound;
                    break;
            }
        }
    }
}
