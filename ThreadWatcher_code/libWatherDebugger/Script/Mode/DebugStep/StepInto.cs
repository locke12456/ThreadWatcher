
namespace libWatherDebugger.Script.Mode.DebugStep
{
    public class StepInto : DebugStep
    {
        public StepInto() : base() 
        {
            Mode = EnvDTE.dbgDebugMode.dbgBreakMode;
        }
        protected override bool _tyrToControl()
        {
            _dbg.StepInto(false);
            return true;
        }
    }
}
