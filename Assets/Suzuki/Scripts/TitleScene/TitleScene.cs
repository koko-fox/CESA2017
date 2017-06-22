using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour {
  [SerializeField]
  private Button startButton;
  [SerializeField]
  private Image fade;
  [SerializeField]
  private float duration;

  private void Start() {
    startButton.onClick.AddListener(onStartButtonClicked);
  }

  private void onStartButtonClicked() {
    StopAllCoroutines();
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
    SceneManager.LoadScene(1);
  }

}
