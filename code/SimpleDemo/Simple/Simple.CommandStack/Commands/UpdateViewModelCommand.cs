using System;

namespace Simple.CommandStack.Commands
{
    public class UpdateViewModelCommand : IUpdateViewModelCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}