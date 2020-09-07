using UniRx;

namespace Sample.Models
{
    /// <summary>
    /// Model
    /// ビジネスロジックはmodelに書く
    /// </summary>
    public class TestModel
    {
        private readonly IntReactiveProperty num = new IntReactiveProperty();
        public IReadOnlyReactiveProperty<int> Num => num;


        private TestModel(){
            num.Value = 0; //値のリセット
        }

        // カウントアップの処理(ビジネスロジック)
        public void CountUp()
        {
            num.Value++;
        }

    }
}