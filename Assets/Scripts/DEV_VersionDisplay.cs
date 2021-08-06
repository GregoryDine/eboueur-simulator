using UnityEngine;
using UnityEngine.UI;

public class DEV_VersionDisplay : MonoBehaviour
{
    [SerializeField] Text text;

    void Awake()
    {
        //diplay ver
        text.text = Application.version;
    }
}
