using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayScene : MonoBehaviour {
  private ChanHealthMod chanHealth;

  [SerializeField]
  private int score;
  [SerializeField]
  private Timer timer;
  [SerializeField]
  private Stage stage;
  [SerializeField]
  private float timeBonusPerKill;
  [SerializeField]
  private GameObject timeBonusEffectPrefab;
  [SerializeField]
  private Canvas mainCanvas;
  [SerializeField]
  private Text text;
  private ChanGrowthMod chanGrowth;

  private void Awake() {
    chanGrowth = FindObjectOfType<ChanGrowthMod>();
    chanHealth = FindObjectOfType<ChanHealthMod>();
    timer.onValueChanged += Timer_onValueChanged;
    timer.onValueAdded += Timer_onValueAdded;
    stage.onEnemySpawned += enemy => {
      enemy.onDied += info => {
        score += 1;
        text.text = "討伐数: " + score.ToString();
        timer.RemainingTime += timeBonusPerKill;
      };
    };
  }

  private void Start() {
    chanHealth.onHealthChanged += () => {
      if (chanHealth.health > 0.0f) return;
        GameOver();
    };
  }

  private void Timer_onValueAdded(float value) {
    var effect = Instantiate(timeBonusEffectPrefab, mainCanvas.transform, false) as GameObject;
    var effectBehaviour = effect.GetComponent<TimeBonusEffect>();
    effectBehaviour.Text.text = "+" + value.ToString("F1");
  }

  private void Timer_onValueChanged(float value) {
    if (value <= 0.0f) {
      GameOver();
    }
  }

  private void GameOver() {
    ScoreHolder.score = score;
    ScoreHolder.level = chanGrowth.level;
    SceneManager.LoadScene(2);
  }
}
