using Database.Create;
using Database.Data;
using Database.Update;
using StructureMap;
using StructureMap.Graph;

namespace Database.DependencyInjection
{
    public static class AbsFactory
    {
        public static Container GetContainer()
        {
            var container = new Container();
            container.Configure(c => c.Scan(scan =>
            {
                scan.WithDefaultConventions();
                scan.TheCallingAssembly();
                scan.LookForRegistries();
                scan.AddAllTypesOf<ICreateTask>();
                scan.AddAllTypesOf<IUpdateTask>();
                scan.ConnectImplementationsToTypesClosing(typeof (IDataList<>));
            }));

            return container;
        }
    }
}