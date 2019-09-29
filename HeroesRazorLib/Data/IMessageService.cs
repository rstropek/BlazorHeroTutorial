using System.Collections.Generic;
using System.ComponentModel;

namespace HeroesCore
{
    public interface IMessageService : INotifyPropertyChanged
    {
        void Add(string message);

        void Clear();

        List<string> Messages { get; set; }
    }
}