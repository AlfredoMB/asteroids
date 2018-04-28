using System;

namespace AlfredoMB.Command
{
    public interface ICommandController
    {
        void AddCommand(ICommand command);
        void AddListener<T>(Action<ICommand> listener) where T : ICommand;
        void RemoveListener<T>(Action<ICommand> listener) where T : ICommand;
    }
}