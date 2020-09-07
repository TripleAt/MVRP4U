using System;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Sample.Views
{
    /// <summary>
    /// Viewの設定
    /// </summary>
    public class TestView : MonoBehaviour
    {
        [SerializeField]
        private Button countButton = null;
        [SerializeField]
        private Text text = null;

        //ボタンがタッチされたらPresenterに通知
        public IObservable<Unit> PushButtonObservable => countButton.onClick.AsObservable();

        //見た目へ変更を加える(Presenterから呼ばれる)
        public void TextMeshUguiSet(string str)
        {
            text.text = str;
        }
        
    }
}
