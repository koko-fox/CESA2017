using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultScene : MonoBehaviour {
  [SerializeField]
  private Button startButton;
  [SerializeField]
  private Image fade;
  [SerializeField]
  private float duration;
  [SerializeField]
  private Text score;
  [SerializeField]
  private Text level;

  private void Awake() {
    score.text += ScoreHolder.score;
    level.text += ScoreHolder.level;
  }

  private void Start() {
    startButton.onClick.AddListener(onStartButtonClicked);
  }

  private void onStartButtonClicked() {
    StartCoroutine(GoPlayScene());
  }

  private IEnumerator GoPlayScene() {
    fade.gameObject.SetActive(true);
    var color = fade.color;
    while (fade.color.a < 1.0f) {
      color.a += (1.0f / duration) * Time.deltaTime;
      fade.color = color;
      yield return null;
    }
    SceneManager.LoadScene(0);
  }
}
