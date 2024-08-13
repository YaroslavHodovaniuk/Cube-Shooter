using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameViewPanel : MonoBehaviour
{
    [Title("Variable")]
    [Tooltip("In seconds")]
    [SerializeField] private int _timeScaler;

    [Title("References")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _moneyText;
    public void Init()
    {
        var hero = Environment.Instance.Player;

        int score = Mathf.FloorToInt((LevelGameManager.Instance.GetGameInProgressTimeDuration() / _timeScaler) * hero.Stats.Score);
        int money = score / 10;

        _scoreText.text = "Score: " + score.ToString();
        _moneyText.text = "Earned money: " + money.ToString();

        Environment.Instance.Player.UpdateMoneyCountOnDeath(money);

    }

    public void OnStartLevelClicked()
    {
        LevelGameManager.OnLevelDestroy?.Invoke();
        SceneManager.LoadScene("Level");
    }
    public void OnMenuLevelClicked()
    {
        SceneManager.LoadScene("StartScene");
    }
}
