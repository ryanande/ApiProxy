
using System;

namespace Database
{
    public interface ITask
    {
        void Execute(Action<string> postAction);
    }
}
