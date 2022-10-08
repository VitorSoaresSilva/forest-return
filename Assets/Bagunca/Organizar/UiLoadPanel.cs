using UnityEngine;
using UnityEngine.UI;

namespace Bagunca.Organizar
{
    public class UiLoadPanel : MonoBehaviour
    {
        public Slider loadSlider;
        public Sprite[] Sprites;
        [SerializeField] private Image image;

        public void SetRandomImage()
        {
            image.sprite = Sprites[Random.Range(0, Sprites.Length)];
        }
    }
}
