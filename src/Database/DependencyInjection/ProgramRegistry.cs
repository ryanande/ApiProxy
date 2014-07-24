using StructureMap.Configuration.DSL;

namespace Database.DependencyInjection
{
    public class ProgramRegistry : Registry
    {
        public ProgramRegistry()
        {
            For<IConfig>().Use<Config>();
        }
    }
}