using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SuzukiEditor {
  [MenuItem("GameObject/Sensor", false, 10)]
  static void CreateCustomGameObject(MenuCommand menuCommand) {
    Vector3 pos = SceneView.GetAllSceneCameras()[0].ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));
    GameObject go = new GameObject("Sensor", typeof(Sensor), typeof(SphereCollider));
    go.transform.position = pos;
    go.transform.rotation = Quaternion.identity;
    var collider = go.GetComponent<Collider>();
    collider.isTrigger = true;
    GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
    Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
    Selection.activeObject = go;
  }
}
