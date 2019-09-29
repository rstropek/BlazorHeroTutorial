using System.Collections.Generic;
using System.ComponentModel;

namespace HeroesCore
{
    public class MessageService : IMessageService
    {
        public List<string> Messages { get; set; } = new List<string>();

        public void Add(string message)
        {
            Messages.Add(message);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Messages)));
        }

        public void Clear() => Messages.Clear();

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}