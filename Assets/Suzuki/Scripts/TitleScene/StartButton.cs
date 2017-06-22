using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using System;

public class StartButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
  [SerializeField]
  private RectTransform bar;

  public void OnPointerEnter(PointerEventData eventData) {
    bar.DOLocalMoveX(25.0f, 0.5f);
  }

  public void OnPointerExit(PointerEventData eventData) {
    bar.DOLocalMoveX(480.0f, 0.5f);
  }
}
