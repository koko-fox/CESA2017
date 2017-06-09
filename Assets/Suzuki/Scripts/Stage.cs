using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Stage : MonoBehaviour {
  public delegate void EnemyKilledEvent();
  public event EnemyKilledEvent onEnemyKilled = delegate { };
  public delegate void EnemySpawnedEvent(EnemyCore enemy);
  public event EnemySpawnedEvent onEnemySpawned = delegate { };

  [SerializeField]
  private Spawn[] spawnSettings;
  [SerializeField]
  private float spawnWait;
  [SerializeField]
  private GameObject[] spawnPoints;
  [SerializeField]
  private int enemies;

  private void Start() {
    Debug.Assert(enemies < spawnPoints.Length);
    for (int i = 0; i < enemies; ++i) {
      SpawnEnemy();
    }
  }

  private void SpawnEnemy() {
    while (true) {
      var index = Random.Range(0, spawnPoints.Length);
      var spawnPoint = spawnPoints[index];
      if (spawnPoint.transform.childCount == 0) {
        var position = spawnPoint.transform.position;
        var rotation = spawnPoint.transform.rotation;

        float total = 0.0f;
        foreach(var elem in spawnSettings) {
          total += elem.Rate;
        }
        float rnd = Random.Range(0, total);

        foreach(var elem in spawnSettings) {
          rnd -= elem.Rate;
          if (rnd <= 0.0f) {
            var newEnemy = Instantiate(elem.Enemy, position, rotation, spawnPoint.transform) as GameObject;
            var enemyBehavior = newEnemy.GetComponent<EnemyCore>();
            onEnemySpawned(enemyBehavior);
            enemyBehavior.onDied += (EnemyCore.DiedFactor factor) => {
              StartCoroutine(Spawning());
              onEnemyKilled();
            };
            break;
          }
        }
        break;
      }
    }
  }

  private IEnumerator Spawning() {
    yield return new WaitForSeconds(spawnWait);
    SpawnEnemy();
  }

  [System.Serializable]
  private class Spawn {
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private int rate;

    public GameObject Enemy { get { return enemy; } }

    public int Rate { get { return rate; } }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(Spawn))]
    public class LineDrawer : PropertyDrawer {
      private Rect wholeRect;
      private float partialSum;
      private SerializedProperty prop;

      public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        var amountRect = new Rect(position.x, position.y, position.width * 0.6f, position.height);
        var unitRect = new Rect(position.x + position.width * 0.6f, position.y, position.width * 0.4f, position.height);

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUIUtility.labelWidth = Mathf.Clamp(40, 0, amountRect.width - 20);
        EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("enemy"), new GUIContent("Enemy"));
        EditorGUIUtility.labelWidth = Mathf.Clamp(40, 0, unitRect.width - 20);
        EditorGUI.PropertyField(unitRect, property.FindPropertyRelative("rate"), new GUIContent("Rate"));

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
      }
    }
#endif
  }

}
