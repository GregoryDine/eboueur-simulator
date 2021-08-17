using UnityEngine;

public class GarbageTruck : MonoBehaviour
{
    [SerializeField] AudioSource engine;
    [SerializeField] AudioSource crash;
    float reference;
    bool crashed;

    void Update()
    {
        engine.volume = Mathf.SmoothDamp(engine.volume, 1f, ref reference, 2.5f);

        if (PauseManager.instance.gameIsPaused)
        {
            engine.Pause();
        }
        else if (!engine.isPlaying &! GameOverManager.instance.gameIsOver)
        {
            engine.Play();
        }

        if (transform.position.z > 75.4)
        {
            engine.Stop();
            if (!crash.isPlaying &! crashed)
            {
                crash.PlayOneShot(crash.clip);
                crashed = true;
            }
        }
        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
    }
}
