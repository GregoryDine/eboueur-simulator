using UnityEngine;
using UnityEngine.UI;

public class DEV : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] Transform player;

    void Awake()
    {
        //diplay ver
        text.text = Application.version;
    }

    void Update()
    {
        //tp player back when falling in void
        if (player.position.y < -10)
        {
            player.position = new Vector3(0, 0, 0);
        }
    }
}
