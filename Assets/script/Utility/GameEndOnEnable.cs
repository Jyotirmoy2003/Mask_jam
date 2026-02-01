using UnityEngine;

public class GameEndOnEnable : MonoBehaviour
{
    [Header("Game End UI")]
    [SerializeField] private GameObject endScreenUI;

    private void OnEnable()
    {
        // Show end screen
        if (endScreenUI != null)
        {
            endScreenUI.SetActive(true);
        }

        // Unlock and show cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Optional: pause game
        Time.timeScale = 1f; // keep 1 if you still want animations
    }
}
