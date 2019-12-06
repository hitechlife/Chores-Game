using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraIntroComic : MonoBehaviour {
  private int currentPositionIndex = 0;
  private Vector3 targetPosition;

#pragma warning disable 0649
  [SerializeField] private bool autoScroll = true;
  [SerializeField, Range(1f, 10f)] private float autoScrollTimePerPanel = 2f;
  [SerializeField, Range(0.1f, 100f)] private float moveSpeed = 15f;

  [SerializeField] private List<Vector3> positions;
#pragma warning restore 0649

  public void MoveToNextPosition() {
    MoveToPosition(currentPositionIndex + 1);
  }

  public void MoveToPreviousPosition() {
    MoveToPosition(currentPositionIndex - 1);
  }

  private void Awake() {
    if (positions.Count == 0) {
      Debug.LogError("No positions set for CameraIntroComic");
    } else {
      targetPosition = positions[0];
      transform.position = targetPosition;
    }

    if (autoScroll) {
      StartCoroutine(AutoScrollRoutine());
    }
  }
  void Start()
  {
    FindObjectOfType<SoundManager>().Play("intromusic");
  }

  private void Update() {
    if (targetPosition != transform.position) {
      transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }
  }

  private void MoveToPosition(int positionIndex) {
    if (positionIndex < 0 || positionIndex >= positions.Count) {
      return;
    }

    currentPositionIndex = positionIndex;
    targetPosition = positions[positionIndex];
  }

  private IEnumerator AutoScrollRoutine() {
    for (int i = 0; i < positions.Count; i++) {
      MoveToPosition(i);
      yield return new WaitUntil(() => transform.position == targetPosition);
      yield return new WaitForSeconds(autoScrollTimePerPanel);
    }
  }
}