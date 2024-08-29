using InfimaGames.LowPolyShooterPack;
using UnityEngine;
using UnityEngine.EventSystems;

public class FixedTouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Character character;

    [SerializeField] private float sensivity;

    [HideInInspector]
    public Vector2 TouchDist;
    [HideInInspector]
    public Vector2 PointerOld;
    [HideInInspector]
    protected int PointerId;
    [HideInInspector]
    public bool Pressed;

    // Use this for initialization
    void Start()
    {
        LevelGameManager.OnAfterStateChanged += OnPlayerSpawned;
    }
    private void OnPlayerSpawned(LevelGameState levelGameState)
    {
        if (levelGameState == LevelGameState.SpawningHero)
            character = Environment.Instance.Player.GetComponentInChildren<Character>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Pressed)
        {
            if (PointerId >= 0 && PointerId < Input.touches.Length)
            {
                TouchDist = Input.touches[PointerId].position - PointerOld;
                PointerOld = Input.touches[PointerId].position;
            }
            else
            {
                TouchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - PointerOld;
                PointerOld = Input.mousePosition;
            }
            
        }
        else
        {
            TouchDist = new Vector2();
        }
        character.OnLook(TouchDist * sensivity);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
        PointerId = eventData.pointerId;
        PointerOld = eventData.position;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
    }
    private void OnDestroy()
    {
        LevelGameManager.OnAfterStateChanged -= OnPlayerSpawned;
    }
}