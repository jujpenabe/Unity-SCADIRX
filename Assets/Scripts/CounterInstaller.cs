using Reflex.Core;
using UnityEngine;
namespace SCA
{
    // Dependency Injection
    // You can call static methods in this class to get object for your dependency injection.
    // This design pattern is known as "Service Locator Design Pattern"
    public class CounterInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerDescriptor descriptor)
        {
            // Instantiate objects
            var gateway = new CountDBGateway();
            var usecase = new CountUsecase(gateway);
            var presenter = gameObject.AddComponent<CountPresenter>();
            presenter.Initialize(usecase); // since presenter inheritates Monobihavior, you can't inject the dependency by constructor

            // And assign for injection
            descriptor.AddInstance(gateway, typeof(ICountDBGateway));
            descriptor.AddInstance(usecase, typeof(ICountUsecase));
            descriptor.AddInstance(presenter, typeof(ICountPresenter));
        }
    }
}