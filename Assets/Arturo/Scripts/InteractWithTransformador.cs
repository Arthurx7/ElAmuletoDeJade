using UnityEngine;

public class InteractWithTransformador : MonoBehaviour
{
    public GameObject playerCamera;           // Cámara del jugador
    public MonoBehaviour playerMovementScript; // Script de movimiento del jugador
    public float transitionDuration = 2.0f;   // Duración de la transición
    public Transform cameraPuzzlePosition;    // Posición del puzzle (donde debe moverse la cámara)
    
    private bool isInZone = false;            // Determina si el jugador está en la zona del transformador
    private bool inPuzzleMode = false;        // Determina si el jugador está en el modo puzzle
    
    private Vector3 savedPlayerCameraPosition;  // Guarda la posición original de la cámara
    private Quaternion savedPlayerCameraRotation; // Guarda la rotación original de la cámara
    
    private float transitionTime = 0.0f;      // Tiempo de la transición
    private bool isTransitioning = false;     // Para saber si la cámara está en transición
    private bool transitioningToPuzzle = false; // Para saber la dirección de la transición (hacia el puzzle o hacia el jugador)

    void Update()
    {
        // Si estamos en la zona y se hace clic, activamos el modo puzzle
        if (isInZone && Input.GetMouseButtonDown(0) && !inPuzzleMode)
        {
            ActivatePuzzleMode();
        }
        
        // Si estamos en el modo puzzle y se presiona "E", volvemos al modo normal
        if (inPuzzleMode && Input.GetKeyDown(KeyCode.E))
        {
            ExitPuzzleMode();
        }

        // Transición suave de la cámara cuando estamos en el puzzle
        if (isTransitioning)
        {
            if (transitioningToPuzzle)
            {
                SmoothCameraTransition(savedPlayerCameraPosition, savedPlayerCameraRotation, cameraPuzzlePosition.position, cameraPuzzlePosition.rotation);
            }
            else
            {
                SmoothCameraTransition(cameraPuzzlePosition.position, cameraPuzzlePosition.rotation, savedPlayerCameraPosition, savedPlayerCameraRotation);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInZone = false;
        }
    }

    private void ActivatePuzzleMode()
    {
        // Guardamos la posición y rotación originales de la cámara del jugador
        savedPlayerCameraPosition = playerCamera.transform.position;
        savedPlayerCameraRotation = playerCamera.transform.rotation;

        // Deshabilitamos el control del jugador
        playerMovementScript.enabled = false;

        // Habilitamos el cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Iniciamos la transición hacia el puzzle
        inPuzzleMode = true;
        transitionTime = 0.0f;
        isTransitioning = true;
        transitioningToPuzzle = true;
    }

    private void ExitPuzzleMode()
    {
        // Iniciamos la transición de vuelta a la posición original
        transitionTime = 0.0f;
        isTransitioning = true;
        transitioningToPuzzle = false;
    }

    private void SmoothCameraTransition(Vector3 fromPosition, Quaternion fromRotation, Vector3 toPosition, Quaternion toRotation)
    {
        transitionTime += Time.deltaTime;

        float t = transitionTime / transitionDuration;
        if (t >= 1.0f)
        {
            t = 1.0f;
            isTransitioning = false;

            // Si hemos terminado la transición hacia el puzzle, habilitamos la lógica del puzzle
            if (transitioningToPuzzle)
            {
                inPuzzleMode = true;
            }
            else
            {
                inPuzzleMode = false;

                // Devolvemos el control del jugador
                playerMovementScript.enabled = true;

                // Desactivamos el cursor
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        // Interpolamos la posición y la rotación basados en el tiempo
        playerCamera.transform.position = Vector3.Lerp(fromPosition, toPosition, t);
        playerCamera.transform.rotation = Quaternion.Lerp(fromRotation, toRotation, t);
    }
}
