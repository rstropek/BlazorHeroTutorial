using System.Collections.Generic;

namespace HeroesCore
{
    public interface IMessageService
    {
        void Add(string message);

        void Clear();

        List<string> Messages { get; set; }
    }
}