﻿using Database.Data;
using Database.Tasks;
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
                scan.AddAllTypesOf<ICreateForTestingTask>();
                scan.ConnectImplementationsToTypesClosing(typeof (IDataList<>));
            }));

            return container;
        }
    }
}