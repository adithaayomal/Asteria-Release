using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    public void SetVisibility(bool visible)
    {
        GetComponent<Image>().enabled = visible;
    }
}
