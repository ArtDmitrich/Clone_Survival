using UnityEngine;
using UnityEngine.UI;

public class BufferCanvas : MonoBehaviour
{
    [SerializeField] private Slider _loadingBar;

    public void SetSliderValue(float value)
    {
        _loadingBar.value = value;
    }
}
