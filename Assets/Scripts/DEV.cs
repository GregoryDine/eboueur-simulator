using UnityEngine;

public class DEV : MonoBehaviour
{
    [SerializeField] Transform player;
    
    void Update()
    {
        //tp player back when falling in void
        if (player.position.y < -10)
        {
            player.position = new Vector3(0, 0, 0);
        }
    }
}
