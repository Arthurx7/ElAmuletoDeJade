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

        // Método para mover la cámara a la posición correspondiente al Canvas A
        public void Position1()
        {
            CameraObject.SetFloat("Animate", 0);
        }

        // Método para mover la cámara a la posición correspondiente al Canvas B
        public void Position2()
        {
            CameraObject.SetFloat("Animate", 1);
        }
    }
}
