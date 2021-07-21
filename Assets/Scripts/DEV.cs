using UnityEngine;
using UnityEngine.UI;

public class DEV : MonoBehaviour
{
    [SerializeField] Text text;

    private void Awake()
    {
        text.text = Application.version;
    }
}
