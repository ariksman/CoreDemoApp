using System.Reflection;
using Autofac;
using MediatR;
using Module = Autofac.Module;

namespace CoreDemoApp.Infrastructure
{
  public class MediatorModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
        .AsImplementedInterfaces();

      // Register the DomainEventHandler classes (they implement INotificationHandler<>) in assembly holding the Domain Events
      //builder.RegisterAssemblyTypes(typeof(ValidateOrAddBuyerAggregateWhenOrderStartedDomainEventHandler).GetTypeInfo().Assembly)
      //  .AsClosedTypesOf(typeof(INotificationHandler<>));

      // Register the Command's Validators (Validators based on FluentValidation library)
      //builder
      //  .RegisterAssemblyTypes(typeof(CreateOrderCommandValidator).GetTypeInfo().Assembly)
      //  .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
      //  .AsImplementedInterfaces();

      builder.Register<ServiceFactory>(context =>
      {
        var componentContext = context.Resolve<IComponentContext>();
        return t =>
        {
          object o;
          return componentContext.TryResolve(t, out o) ? o : null;
        };
      });

      //builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
      //builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
      //builder.RegisterGeneric(typeof(TransactionBehaviour<,>)).As(typeof(IPipelineBehavior<,>));
    }
  }
}