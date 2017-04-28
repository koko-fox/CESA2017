using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[DisallowMultipleComponent]
public class EnemyItemDropperModule : EnemyModuleBase {
  [SerializeField]
  private List<Drop> drops = new List<Drop>();

  protected override void DoDied() {
    DropItem();
  }

  private void DropItem() {
    foreach (var drop in drops) {
      if (Random.Range(0, 100) < drop.Rate) {
        Instantiate(drop.Item, transform.position, transform.rotation);
      }
    }
  }

  [System.Serializable]
  private class Drop {
    [SerializeField]
    private GameObject item;
    [SerializeField]
    private int rate;

    public GameObject Item { get { return item; } }

    public int Rate { get { return rate; } }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(Drop))]
    public class SomeClassDrawer : PropertyDrawer {
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
        EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("item"), new GUIContent("Item"));
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
