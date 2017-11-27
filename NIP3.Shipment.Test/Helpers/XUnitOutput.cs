using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PactNet.Infrastructure.Outputters;
using Xunit.Abstractions;

namespace NIP3.Shipment.Test.Helpers
{
    public class XUnitOutput: IOutput
    {
        private readonly ITestOutputHelper output;

        public XUnitOutput(ITestOutputHelper output)
        {
            this.output = output;
        }

        public void WriteLine(string line)
        {
            output.WriteLine(line);
        }
    }
}
