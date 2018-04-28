using UnityEngine;
using System.Collections.Generic;
using System;

namespace AlfredoMB.Command
{
    public class CommandController : ICommandController
    {
        private Dictionary<Type, Action<ICommand>> _commandListeners = new Dictionary<Type, Action<ICommand>>();
        private List<ICommand> _commands = new List<ICommand>();

        public void AddListener<T>(Action<ICommand> commandListener) where T : ICommand
        {
            var type = typeof(T);
            if (!_commandListeners.ContainsKey(type))
            {
                _commandListeners.Add(type, commandListener as Action<ICommand>);
            }
            else
            {
                _commandListeners[type] += commandListener as Action<ICommand>;
            }
        }

        public void RemoveListener<T>(Action<ICommand> commandListener) where T : ICommand
        {
            var type = typeof(T);
            if (!_commandListeners.ContainsKey(type))
            {
                Debug.LogWarningFormat("[CommandController] Listener not found to be removed: {0} - {1}", type, commandListener);
            }
            else
            {
                _commandListeners[type] -= commandListener as Action<ICommand>;
            }
        }

        public void AddCommand(ICommand command)
        {
            _commands.Add(command);

            // TODO: for now we'll just re-router the command without any logic:
            var type = command.GetType();
            if (_commandListeners.ContainsKey(type))
            {
                _commandListeners[type](command);
            }
        }
    }
}
