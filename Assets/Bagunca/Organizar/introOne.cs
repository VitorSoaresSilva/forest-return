using UnityEngine;

namespace Bagunca.Organizar
{
    public class introOne : MonoBehaviour
    {
        [SerializeField] private Animator animator2;

        public void PlayIntro2()
        {
            animator2.SetTrigger("play");
        }
    }
}
