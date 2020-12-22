using System.Diagnostics;
using Xunit;

namespace _2020.Tests.Attributes
{
    public sealed class FactRunnableInDebugOnlyAttribute : FactAttribute
    {
        public FactRunnableInDebugOnlyAttribute()
        {
            if (!Debugger.IsAttached)
            {
                Skip = "Only running in interactive mode.";
            }
        }
    }

    public sealed class TheoryRunnablenDebugOnlyAttribute : TheoryAttribute
    {
        public TheoryRunnablenDebugOnlyAttribute()
        {
            if (!Debugger.IsAttached)
            {
                Skip = "Only running in interactive mode.";
            }
        }
    }
}
