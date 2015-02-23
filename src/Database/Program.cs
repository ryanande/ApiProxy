using System;
using System.Linq;
using CommandLine;
using Database.DependencyInjection;
using Database.Tasks;

namespace Database
{
    class Program
    {
        static void Main(string[] args)
        {

            var options = new Options();
            if (!Parser.Default.ParseArguments(args, options))
            {
                Console.WriteLine("Please specify an action!");
                return;
            }


            // set up the abs factory
            // objectfactory is being deprecated in 4.0
            var container = AbsFactory.GetContainer();


            switch (options.PopulationActionValue)
            {
                case PopulationAction.Create:

                    var dropDatabaseTask = container.GetInstance<DropDatabaseTask>();
                    dropDatabaseTask.Execute(Console.WriteLine);

                    var createTasks = container.GetAllInstances<ICreateTask>();
                    createTasks.ToList().ForEach(task => task.Execute(Console.WriteLine));

                    break;

                case PopulationAction.CreateForTesting:

                    var testCreateTask = container.GetAllInstances<ICreateForTestingTask>();
                    testCreateTask.ToList().ForEach(task => task.Execute(Console.WriteLine));

                    break;

                case PopulationAction.Update:

                    var updateTasks = container.GetAllInstances<IUpdateTask>();
                    updateTasks.ToList().ForEach(task => task.Execute(Console.WriteLine));

                    break;
            }
        }
    }
}
