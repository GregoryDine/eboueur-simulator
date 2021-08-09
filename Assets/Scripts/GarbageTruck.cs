using UnityEngine;

public class GarbageTruck : MonoBehaviour
{
    [SerializeField] AudioSource engine;
    float reference;

    void Update()
    {
        engine.volume = Mathf.SmoothDamp(engine.volume, 1f, ref reference, 2.5f);

        if (PauseManager.instance.gameIsPaused)
        {
            engine.Pause();
        }
        else if (!engine.isPlaying)
        {
            engine.Play();
        }

        transform.Translate(Vector3.forward * Time.deltaTime);
    }
}
