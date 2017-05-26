using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayScene : MonoBehaviour {
  [SerializeField]
  private int score;

  [SerializeField]
  private ChanCore player;
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

  private void Awake() {
    timer.onValueChanged += Timer_onValueChanged;
    timer.onValueAdded += Timer_onValueAdded;
    stage.onEnemyKilled += () => {
      score += 1;
      text.text = "討伐数: " + score.ToString();
      timer.RemainingTime += timeBonusPerKill;
    };
  }

  private void Timer_onValueAdded(float value) {
    var effect = Instantiate(timeBonusEffectPrefab, mainCanvas.transform, false) as GameObject;
    var effectBehaviour = effect.GetComponent<TimeBonusEffect>();
    effectBehaviour.Text.text = "+" + ((int)(value)).ToString();
  }

  private void Timer_onValueChanged(float value) {
    if (value <= 0.0f) {
      SceneManager.LoadScene(2);
    }
  }
}
