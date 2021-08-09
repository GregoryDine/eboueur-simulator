using UnityEngine;

public class FootstepsController : MonoBehaviour
{
    [SerializeField] AudioClip grassSound, concreteSound, currentSound;
    [SerializeField] float volMin, volMax, pitchMin, pitchMax;

    AudioSource audioSource;

    float sprintSoundSpeed = 1.5f;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
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

                default:
                    currentSound = null;
                    break;
            }
        }

        if (PlayerController.instance.currentHorizontalSpeed > 4 && PlayerController.instance.controller.isGrounded)
        {
            if (!audioSource.isPlaying && currentSound != null)
            {
                audioSource.volume = Random.Range(volMin, volMax);
                audioSource.pitch = Random.Range(pitchMin, pitchMax);

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    audioSource.pitch *= sprintSoundSpeed;
                }

                audioSource.PlayOneShot(currentSound);
            }
        }
    }
}
