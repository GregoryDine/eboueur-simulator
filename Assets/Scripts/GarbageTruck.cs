using UnityEngine;

public class GarbageTruck : MonoBehaviour
{
    [SerializeField] AudioSource engine;

    void Update()
    {
        if (PauseManager.instance.gameIsPaused)
        {
            engine.Pause();
        }
        else if (!engine.isPlaying)
        {
            engine.Play();
        }

        transform.Translate(Vector3.forward * Time.deltaTime);

        if (transform.position.z > 45)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -45);
        }
    }
}
