using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Http.Dispatcher;

namespace EdFiValidation.ApiProxy.Helpers
{

    public class WebApiStructureMapResolver : StructureMapDependencyScope, IDependencyResolver, IHttpControllerActivator
    {
        private readonly IContainer _container;

        public WebApiStructureMapResolver(IContainer container): base(container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            _container = container;

            _container.Inject(typeof(IHttpControllerActivator), this);
        }

        public IDependencyScope BeginScope()
        {
            return new StructureMapDependencyScope(_container.GetNestedContainer());
        }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            return _container.GetNestedContainer().GetInstance(controllerType) as IHttpController;
        }
    }


    public class StructureMapDependencyScope : IDependencyScope
    {
        private IContainer _container;

        public StructureMapDependencyScope(IContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            _container = container;
        }

        public object GetService(Type serviceType)
        {
            if (_container == null)
                throw new ObjectDisposedException("this", "This scope has already been disposed.");

            return _container.TryGetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (_container == null)
                throw new ObjectDisposedException("this", "This scope has already been disposed.");

            return _container.GetAllInstances(serviceType).Cast<object>();
        }

        public void Dispose()
        {
            if (_container != null)
                _container.Dispose();

            _container = null;
        }
    }
}