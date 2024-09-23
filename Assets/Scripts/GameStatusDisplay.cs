using UnityEngine;
using TMPro;

public class GameStatusDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private AIController aiController;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private EnemyController enemyController;

    private void Update()
    {
        if (statusText == null || aiController == null || playerController == null)
        {
            Debug.LogError("GameStatusDisplay: One or more required components are not assigned!");
            return;
        }

        string status = "Game Status:\n\n";

        // AI Status
        status += "AI:\n";
        status += $"State: {aiController.currentState}\n";
        status += $"Health: {aiController.currentHealth}/{aiController.maxHealth}\n";
        statusText.text = status;
    }
}
