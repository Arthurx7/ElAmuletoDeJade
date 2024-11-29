using UnityEngine;

namespace SlimUI.ModernMenu
{
    public class CameraCanvasSwitcher : MonoBehaviour
    {
        private Animator CameraObject;

        void Start()
        {
            CameraObject = transform.GetComponent<Animator>();
            CameraObject.SetInteger("Animate", 3);
        }

        // M�todo para mover la c�mara a la posici�n correspondiente al Canvas A
        public void Position1()
        {
            CameraObject.SetInteger("Animate", 0);
        }

        // M�todo para mover la c�mara a la posici�n correspondiente al Canvas B
        public void Position2()
        {
            CameraObject.SetInteger("Animate", 1);
        }
        public void Position3()
        {
            CameraObject.SetInteger("Animate", 2);
        }
    }
}
