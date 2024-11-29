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

        // Método para mover la cámara a la posición correspondiente al Canvas A
        public void Position1()
        {
            CameraObject.SetInteger("Animate", 0);
        }

        // Método para mover la cámara a la posición correspondiente al Canvas B
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
