﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using CoreDemoApp.Core.CQS;

namespace CoreDemoApp.Infrastructure
{
  public class HandlerAutoFacModule : Autofac.Module
  {
    private readonly IEnumerable<Assembly> _assembliesToScan;

    public HandlerAutoFacModule(IEnumerable<Assembly> assembliesToScan)
    {
      _assembliesToScan = assembliesToScan;
    }

    public HandlerAutoFacModule(params Assembly[] assembliesToScan) : this((IEnumerable<Assembly>)assembliesToScan) { }

    protected override void Load(ContainerBuilder builder)
    {
      var assembliesToScan = _assembliesToScan as Assembly[] ?? _assembliesToScan.ToArray();

      var allAssemblies = assembliesToScan
        .Where(a => !a.IsDynamic)
        .Distinct()
        .ToArray();

      foreach (var assembly in allAssemblies)
      {
        builder.RegisterAssemblyTypes(assembly)
          .AsClosedTypesOf(typeof(ICommandHandler<,>));
        builder.RegisterAssemblyTypes(assembly)
          .AsClosedTypesOf(typeof(IQueryHandler<,>));
      }

      builder.RegisterType<QueryDispatcher>().As<IQueryDispatcher>();
      builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>();
    }
  }
}