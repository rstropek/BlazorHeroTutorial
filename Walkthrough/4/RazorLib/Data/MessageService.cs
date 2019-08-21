using System.Collections.Generic;

namespace HeroesCore
{
    public class MessageService : IMessageService
    {
        public List<string> Messages { get; set; } = new List<string>();

        public void Add(string message) => Messages.Add(message);

        public void Clear() => Messages.Clear();
    }
}