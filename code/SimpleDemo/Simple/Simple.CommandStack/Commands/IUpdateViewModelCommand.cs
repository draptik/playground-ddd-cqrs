using System;

namespace Simple.CommandStack.Commands
{
    public interface IUpdateViewModelCommand
    {
        Guid Id { get; set; } 
        string Name { get; set; } 
        string Address { get; set; } 
    }
}