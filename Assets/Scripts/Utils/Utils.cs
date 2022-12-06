using UnityEngine;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;

static class Utils {

  public static async void DoItAfter(UnityAction action, int time) {
    await UniTask.Delay(time);
    action();
  }

}