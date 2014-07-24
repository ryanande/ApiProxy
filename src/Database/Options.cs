using CommandLine;

namespace Database
{
    public sealed class Options
    {
        [Option('a', "action", Required = true)]
        public PopulationAction PopulationActionValue { get; set; }
    }
}
