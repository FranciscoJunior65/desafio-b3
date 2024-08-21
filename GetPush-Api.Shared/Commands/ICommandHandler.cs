﻿namespace GetPush_Api.Shared.Commands
{
    public interface ICommandHandler<T> where T : ICommand
    {
        ICommandResult Handler(T command);
    }
}
