
using Sample.Models;
using Sample.Presenter;
using Zenject;

namespace Sample.ZenjectInstaller
{
    public class SampleButtonInstaller : MonoInstaller
    {
        //zenjectでModelとPresenterのインストールする
        public override void InstallBindings()
        {
            Container.Bind<TestModel>().AsCached();
            Container.Bind<TestPresenter>().AsCached().NonLazy();
        }
    }
}