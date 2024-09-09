using InfimaGames.LowPolyShooterPack;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class FixedTouchField : Joystick
{
    private Character character;

    public InputActionReference lookAction;  // Reference to the look action from Input System
    public float sensitivity = 0.1f;         // Sensitivity for looking around

    private Vector2 lookInput = Vector2.zero; // Default look input set to zero
    private bool isTouching = false;          // Flag to track whether the user is touching the screen

    protected override void Start()
    {
        base.Start();
        // Subscribe to the player spawning event
        LevelGameManager.OnAfterStateChanged += OnPlayerSpawned;
    }

    private void OnPlayerSpawned(LevelGameState levelGameState)
    {
        if (levelGameState == LevelGameState.SpawningHero)
        {
            // Get the player character from the environment
            character = Environment.Instance.Player.GetComponentInChildren<Character>();
        }
    }

    protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        base.HandleInput(magnitude, normalised, radius, cam);
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
    }
}