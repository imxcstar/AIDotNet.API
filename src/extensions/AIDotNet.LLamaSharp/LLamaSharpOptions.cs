using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDotNet.LLamaSharp
{
    public sealed class LLamaSharpOptions
    {
        public const string ServiceName = "LLamaSharp";

        public IServiceProvider ServiceProvider { get; set; }
    }
}
