using System;
using Sample.Models;
using Sample.Views;
using UniRx;

namespace Sample.Presenter {
    /// <summary>
    /// Presenter
    /// Modelの変更をViewに反映し、ViewのアクションをModelへ反映
    /// </summary>
    public class TestPresenter
    {
        //読み取りしかしない
        private readonly TestView testView;
        private readonly TestModel upButtonModel;

        //Presenterの処理
        public TestPresenter(TestModel model, TestView view)
        {
            upButtonModel =  model ?? throw new ArgumentNullException(nameof(model));
            testView = view ? view : throw new ArgumentNullException(nameof(view));
            //ModelとViewが増えたら追記していく
            
            upButtonModel.Num.Subscribe(ViewNumUpdate); //Modelに変更があったらViewへ更新
            testView.PushButtonObservable.Subscribe(_=> CountUp());  //Viewからカウントアップ通知があったらModelを更新

        }
        
        /// <summary>
        /// Modelのカウントアップ処理を呼ぶ
        /// </summary>
        private void CountUp()
        {
            upButtonModel.CountUp();
        }

        /// <summary>
        /// ViewのTextMeshUguiSetを呼ぶ
        /// </summary>
        /// <param name="num"></param>
        private void ViewNumUpdate(int num)
        {
            testView.TextMeshUguiSet(num.ToString());
        }
    }
}
