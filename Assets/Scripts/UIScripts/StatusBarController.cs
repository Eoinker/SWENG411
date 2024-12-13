using UnityEngine;
using TMPro;

public class StatusBarController : MonoBehaviour
{
    public TMP_Text timeText;       // Reference to the Time text
    public TMP_Text xPositionText; // Reference to the X Position text
    public TMP_Text yPositionText; // Reference to the Y Position text
    public TMP_Text groundedText;  // Reference to the Grounded text

    public CharacterControl characterControl; // Reference to the CharacterControl script
    private Transform player;                 // Reference to the player's transform

    private void Start()
    {
        if (characterControl != null)
        {
            player = characterControl.transform;
        }
    }

    private void Update()
    {
        // Get simulation time from SimulationManager
        if (SimulationManager.Instance != null)
        {
            timeText.text = $"Time: {SimulationManager.Instance.GetTime():F2}";
        }

        // Update player X and Y positions
        if (player != null)
        {
            xPositionText.text = $"X: {player.position.x:F2}";
            yPositionText.text = $"Y: {player.position.y:F2}";
        }

        // Update grounded state using CharacterControl's IsGrounded
        if (characterControl != null)
        {
            groundedText.text = $"Grounded: {characterControl.isGrounded}";
        }
    }
}
