using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulator_MarcinJunka
{
    public class AirplaneEventArgs : EventArgs
    {
        public string Message { get; private set; }
        public string Name { get; private set; }

        public AirplaneEventArgs(string message, string name)
        {
            this.Message = message;
            this.Name = name;
        }

    }
}
