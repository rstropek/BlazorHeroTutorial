using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MathTrainer.Model
{
    public class ResultLine
    {
        public int Base { get; set; }

        public char Operator { get; set; }

        public int Operand { get; set; }

        public int Result { get; set; }
    }
}
