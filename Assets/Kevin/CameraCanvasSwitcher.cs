using UnityEngine;

namespace SlimUI.ModernMenu
{
    public class CameraCanvasSwitcher : MonoBehaviour
    {
        private Animator CameraObject;

        void Start()
        {
            CameraObject = transform.GetComponent<Animator>();
        }

        // M�todo para mover la c�mara a la posici�n correspondiente al Canvas A
        public void Position1()
        {
            CameraObject.SetFloat("Animate", 0);
        }

        // M�todo para mover la c�mara a la posici�n correspondiente al Canvas B
        public void Position2()
        {
            CameraObject.SetFloat("Animate", 1);
        }
    }
}
