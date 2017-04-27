using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour {
  private float jumpWait;
  [SerializeField]
  private JellyMesh jelly;
  [SerializeField]
  private float initialJumpWait;
  [SerializeField]
  private float minJumpPower;
  [SerializeField]
  private float maxJumpPower;
  [SerializeField]
  private float minTorquePower;
  [SerializeField]
  private float maxTorquePower;
  [SerializeField]
  private Button startButton;
  [SerializeField]
  private Image fade;
  [SerializeField]
  private float duration;

  private void Start() {
    jumpWait = initialJumpWait;
    startButton.onClick.AddListener(onStartButtonClicked);
    StartCoroutine(JumpJelly());
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

  private IEnumerator JumpJelly() {
    while (true) {
      yield return new WaitForSeconds(jumpWait);

      Vector3 jumpVector = Vector3.up * Random.Range(minJumpPower, maxJumpPower);

      Vector3 torqueVector = Vector3.zero;
      torqueVector.x = Random.Range(-1.0f, 1.0f);
      torqueVector.y = Random.Range(-1.0f, 1.0f);
      torqueVector.z = Random.Range(-1.0f, 1.0f);
      torqueVector.Normalize();
      torqueVector *= Random.Range(minTorquePower, maxTorquePower);

      jelly.AddForce(jumpVector, true);
      jelly.AddTorque(torqueVector, false);

      jumpWait = Random.Range(2.0f, 4.0f);
    }
  }

}
