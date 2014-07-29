using System;

namespace Database.Tasks
{
    public interface ITask
    {
        void Execute(Action<string> postAction);
    }
}
