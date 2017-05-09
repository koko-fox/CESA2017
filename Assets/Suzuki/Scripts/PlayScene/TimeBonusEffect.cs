using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBonusEffect : MonoBehaviour {
  private Text text;
  [SerializeField]
  private float riseSpeed;

  public Text Text { get; set; }

  private void Awake() {
    Text = GetComponent<Text>();
  }

  private void Start() {
    StartCoroutine(Up());
    Destroy(gameObject, 1.0f);
  }

  private IEnumerator Up() {
    while (true) {
      var position = transform.position;
      position.y += riseSpeed;
      transform.position = position;
      yield return null;
    }
  }
}
