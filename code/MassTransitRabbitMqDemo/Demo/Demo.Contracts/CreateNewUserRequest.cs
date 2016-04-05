namespace Demo.Contracts
{
    public class CreateNewUserRequest
    {
        public CreateNewUserRequest(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
    }
}