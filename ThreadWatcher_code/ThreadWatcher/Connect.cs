using System;
using Extensibility;
using EnvDTE;
using Microsoft.VisualStudio.CommandBars;
using Microsoft.VisualStudio.Debugger.Interop;
using Microsoft.VisualStudio.Shell.Interop;
using System.Diagnostics;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio;
using EnvDTE80;
using System.Threading;
using libWatherDebugger.Memory;
using System.Collections.Generic;
using libWatherDebugger.Breakpoint;
using libWatherDebugger.Stack;
using libWatcherDialog;
using libWatherDebugger.Thread;
using libWatcherDialog.PropertyItem.Thread;
using libWatherDebugger.Message;
using libWatherDebugger.Script;
using libWatcherDialog.PropertyItem.Log;
using libWatcherDialog.PropertyItem.Logger;
using libWatcherDialog.PropertyItem.BreakPoint;
namespace ThreadWatcher
{
    /// <summary>用於實作增益集的物件。</summary>
    /// <seealso class='IDTExtensibility2' />

    public class Connect : IDTExtensibility2, IDTCommandTarget, IDebugEventCallback2
    {

        /// <summary>針對增益集物件實作建構函式。請將初始化程式碼置於此方法中。</summary>
        public Connect()
        {
            addressExpressionString = "";
        }

        /// <summary>實作 IDTExtensibility2 介面的 OnConnection 方法。會收到正在載入增益集的告知。</summary>
        /// <param term='application'>主應用程式的根物件。</param>
        /// <param term='connectMode'>描述如何載入增益集。</param>
        /// <param term='addInInst'>代表此增益集的物件。</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnConnection(object application, ext_ConnectMode connectMode, object addInInst, ref Array custom)
        {
            _applicationObject = (DTE2)application;
            _addInInstance = (AddIn)addInInst;
            if (connectMode == ext_ConnectMode.ext_cm_UISetup)
            {
                object[] contextGUIDS = new object[] { };
                Commands2 commands = (Commands2)_applicationObject.Commands;
                string toolsMenuName = "Tools";

                //將命令放在 [工具] 功能表上。
                //尋找 MenuBar 命令列，其為包含所有主要功能表項目的最上層命令列:
                Microsoft.VisualStudio.CommandBars.CommandBar menuBarCommandBar = ((Microsoft.VisualStudio.CommandBars.CommandBars)_applicationObject.CommandBars)["MenuBar"];

                //在 MenuBar 命令列上尋找 Tools 命令列:
                CommandBarControl toolsControl = menuBarCommandBar.Controls[toolsMenuName];
                CommandBarPopup toolsPopup = (CommandBarPopup)toolsControl;

                //如果您要加入多個由增益集處理的命令，可以複製此 try/catch 區塊，
                //  但務必同時更新 QueryStatus/Exec 方法，以包含新命令的名稱。
                try
                {
                    //將命令加入至 Commands 集合:
                    Command command = commands.AddNamedCommand2(_addInInstance, "ThreadWatcher", "ThreadWatcher", "Executes the command for ThreadWatcher", true, 59, ref contextGUIDS, (int)vsCommandStatus.vsCommandStatusSupported + (int)vsCommandStatus.vsCommandStatusEnabled, (int)vsCommandStyle.vsCommandStylePictAndText, vsCommandControlType.vsCommandControlTypeButton);

                    //將命令的控制項加入至 [工具] 功能表:
                    if ((command != null) && (toolsPopup != null))
                    {
                        command.AddControl(toolsPopup.CommandBar, 1);
                    }

                    DTE2 applicationObject = (DTE2)application;
                    AddIn addInInstance = (AddIn)addInInst;
                    ServiceProvider serviceProvider = new ServiceProvider((Microsoft.VisualStudio.OLE.Interop.IServiceProvider)application);
                    //IDebugComPlusSymbolProvider
                    IVsDebugger vsDebugger = (IVsDebugger)serviceProvider.GetService(typeof(SVsShellDebugger));
                    int hResult = vsDebugger.AdviseDebugEventCallback(this);
                    if (VSConstants.S_OK == hResult)
                    {
                        _dbg = Watcher.Debugger.Debugger.getInstance();
                        _dbg.Init(applicationObject.Debugger);
                    }
                    //CommandBar menuBar = ((CommandBars)applicationObject.CommandBars)["MenuBar"];
                    //CommandBarPopup toolsPopup = (CommandBarPopup)menuBar.Controls["Tools"];
                    //cmdBarButton = (CommandBarButton)toolsPopup.Controls.Add(MsoControlType.msoControlButton, Missing.Value, Missing.Value, 1, true);
                    //cmdBarButton.Caption = "Get StackFrame Info";
                    //cmdBarButton.Style = MsoButtonStyle.msoButtonCaption;
                    //cmdBarButton.Click += new _CommandBarButtonEvents_ClickEventHandler(OnCmdBarButtonClick);

                    //IVsTextManager textManager = (IVsTextManager)serviceProvider.GetService(typeof(SVsTextManager));
                    //hResult = textManager.GetRegisteredMarkerTypeID(ref currentStmtMarkerGuid, out currentStmtMarkerID);
                }
                catch (System.ArgumentException)
                {
                    //如果執行到此處，則發生例外狀況的原因可能是因為已經有
                    //  相同名稱的命令。如果是這樣，就不需要重新建立命令，
                    //  並且可以安全無虞地忽略此例外狀況。
                }
            }
        }

        private void DebuggerEvents_OnEnterBreakMode(dbgEventReason Reason, ref dbgExecutionAction ExecutionAction)
        {
            Debug.WriteLine("1");
        }

        private void watch_AddEvent(object sender)
        {
            bool handled = true;
            object obj1 = this, obj2 = sender;
            Exec((sender as VariablesWatcher).QueryType, vsCommandExecOption.vsCommandExecOptionDoDefault, ref obj1, ref obj2, ref handled);
        }
        public int Event(IDebugEngine2 pEngine, IDebugProcess2 pProcess, IDebugProgram2 pProgram, IDebugThread2 thread, IDebugEvent2 pEvent, ref Guid riidEvent, uint dwAttrib)
        {
            DTE2 _dte = _applicationObject;
            //note .trigger on program created . need to implements 
            //pProcess.EnumThreads(IEnumDebugThreads2)
            if (pEvent is IDebugProcessCreateEvent2)
            {
                breakpoints = new BreakPoints();
                threads = new libWatcherDialog.Threads();
                _dte.Debugger.Breakpoints.Add("", "____watch_alloc.cpp", 7, 1);
                _dte.Debugger.Breakpoints.Add("", "____watch_alloc.cpp", 12, 1);
                //_dte.Debugger.Breakpoints.Add("", "dbgdel.cpp", 42, 1);
                // Debug.WriteLine(pEvent);
            }
            //note .trigger on program destory . need to implements 
            if (pEvent is IDebugProgramDestroyEvent2)
            {
                List<Breakpoint> bps = new List<Breakpoint>();
                foreach (Breakpoint bp in _dte.Debugger.Breakpoints)
                {
                    //if (bp.File != "")
                    //{
                    //    FileInfo file = new FileInfo(bp.File);
                    //    if (file.Name == "____watch_alloc.cpp") bps.Add(bp);
                    //}
                    bps.Add(bp);
                }
                foreach (Breakpoint bp in bps)
                    bp.Delete();
                breakpoints.Close();
                threads.Close();
            }
            if (pEvent is IDebugBreakpointEvent2)
            {
                try
                {
                    if (thread != null)
                    {
                        _dbg.InitStackFrame(thread);
                        DebugStackFrame stack = _dbg.CurrentStackFrame as DebugStackFrame;
                        _dbg.Locals(stack);
                        DebugBreakpointFactory bpFactory = new DebugBreakpointFactory(pEvent as IDebugBreakpointEvent2);
                        bpFactory.CreateProduct();
                        DebugBreakpoint breakpoint = bpFactory.Product as DebugBreakpoint;
                        breakpoints.BreakPointTriggered(breakpoint);
                        return 1;
                    }
                }
                catch (Exception fail)
                {
                    Debug.WriteLine(fail.Message);
                }
            }
            if (pEvent is IDebugThreadCreateEvent2)
            {
                if (thread != null)
                {
                    DebugThreadFactory factory = new DebugThreadFactory(thread);
                    factory.CreateProduct(thread);
                    //bug : need sync
                    threads.AddThread(factory.Product as DebugThread);
                }
                return 1;
            }
            if (pEvent is IDebugMessageEvent2)
            {
                IDebugMessageEvent2 events = pEvent as IDebugMessageEvent2;
                DebugMessageFactory factory = new DebugMessageFactory(events);
                DebugThreadFactory threadfactory = new DebugThreadFactory(thread);
                //factory.CreateProduct(thread);
                //ThreadItem item = ThreadsManagement.getInstance().GetItem(thread) as ThreadItem;
                //ThreadsManagement.getInstance().SetCurrentItem(item);
                if (VSConstants.S_OK == factory.CreateProduct())
                {
                    string msg = (factory.Product as DebugMessage).Message;
                    threadfactory.CreateProduct(thread);
                    _dbg.InitStackFrame(threadfactory.Product);
                    ThreadLog log = new ThreadLog();
                    log.Name = msg;
                    log.Key = threadfactory.Product.ID;
                    LogManagement.getInstance().AddItem(log);
                    BreakpointItem target = BreakpointsManagement.getInstance().GetItem(log.Name);
                    target.HitLocations.BreakpointHit(threadfactory.Product);
                    //item.WriteLog(msg);
                }
                return 1;
            }
            if (pEvent is IDebugReturnValueEvent2)
            {
                IDebugReturnValueEvent2 e = pEvent as IDebugReturnValueEvent2;
                if (DebugScript.HasASyncScript())
                    DebugScript.FinishSync();
                return 1;
            }
            if (addressExpressionString == "")
            {

                //Debug.WriteLine(riidEvent);

                //if(pEvent is IDebugStepCompleteEvent2)              
                //    Debug.WriteLine("1");
                return 1;
            }
            if (riidEvent == typeof(IDebugCurrentThreadChangedEvent100).GUID)
            {
                if (thread != null)
                {

                    Debug.WriteLine("enter event");
                    _dbg.InitStackFrame(thread);
                    MemoryInfo memory = _dbg.Query(addressExpressionString, thread) as MemoryInfo;
                    //watch.LoadAddress(memory);
                    memory.Init(thread);
                    addressExpressionString = "";
                }
                //}
            }
            return 1;
        }
        /// <summary>實作 IDTExtensibility2 介面的 OnDisconnection 方法。會收到正在卸載增益集的告知。</summary>
        /// <param term='disconnectMode'>描述如何卸載增益集。</param>
        /// <param term='custom'>主應用程式專屬參數的陣列。</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnDisconnection(ext_DisconnectMode disconnectMode, ref Array custom)
        {
        }

        /// <summary>實作 IDTExtensibility2 介面的 OnAddInsUpdate 方法。增益集的集合變更時會收到告知。</summary>
        /// <param term='custom'>主應用程式專屬參數的陣列。</param>
        /// <seealso class='IDTExtensibility2' />		
        public void OnAddInsUpdate(ref Array custom)
        {
        }

        /// <summary>實作 IDTExtensibility2 介面的 OnStartupComplete 方法。會收到主應用程式載入完成的告知。</summary>
        /// <param term='custom'>主應用程式專屬參數的陣列。</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnStartupComplete(ref Array custom)
        {
        }

        /// <summary>實作 IDTExtensibility2 介面的 OnBeginShutdown 方法。會收到正在卸載主應用程式的告知。</summary>
        /// <param term='custom'>主應用程式專屬參數的陣列。</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnBeginShutdown(ref Array custom)
        {
        }

        /// <summary>實作 IDTCommandTarget 介面的 QueryStatus 方法。這會在命令的可用性更新時呼叫</summary>
        /// <param term='commandName'>要判斷其狀態的命令名稱。</param>
        /// <param term='neededText'>命令所需的文字。</param>
        /// <param term='status'>命令在使用者介面中的狀態。</param>
        /// <param term='commandText'>neededText 參數要求的文字。</param>
        /// <seealso class='Exec' />
        public void QueryStatus(string commandName, vsCommandStatusTextWanted neededText, ref vsCommandStatus status, ref object commandText)
        {

            if (neededText == vsCommandStatusTextWanted.vsCommandStatusTextWantedNone)
            {
                if (commandName == "ThreadWatcher.Connect.ThreadWatcher")
                {
                    status = (vsCommandStatus)vsCommandStatus.vsCommandStatusSupported | vsCommandStatus.vsCommandStatusEnabled;
                    return;
                }
            }
            else
                return;
        }

        /// <summary>實作 IDTCommandTarget 介面的 Exec 方法。這會在叫用命令時呼叫。</summary>
        /// <param term='commandName'>要執行的命令名稱。</param>
        /// <param term='executeOption'>描述如何執行命令。</param>
        /// <param term='varIn'>從呼叫端傳遞至命令處理常式的參數。</param>
        /// <param term='varOut'>從命令處理常式傳遞至呼叫端的參數。</param>
        /// <param term='handled'>通知呼叫端是否已處理命令。</param>
        /// <seealso class='Exec' />
        public void Exec(string commandName, vsCommandExecOption executeOption, ref object varIn, ref object varOut, ref bool handled)
        {
            handled = false;
            if (executeOption == vsCommandExecOption.vsCommandExecOptionDoDefault)
            {
                DTE2 _dte = _applicationObject;
                if (commandName == "ThreadWatcher.Connect.ThreadWatcher")
                {

                    breakpoints.Show();
                    threads.Show();
                    //if (watch != null)
                    //{
                    //    watch.Close();
                    //    watch.Dispose();
                    //}

                    //watch = new VariablesWatcher();
                    //watch.AddEvent += watch_AddEvent;
                    //watch.Show();

                }
                if (commandName == "query")
                {
                    try
                    {
                        _dte.Debugger.Break();
                        //watch.LoadVariable(_dbg.Query);
                        _dte.Debugger.Go();
                    }
                    catch (Exception fail)
                    {

                    }


                    return;
                }
                if (commandName == "address")
                {


                    try
                    {
                        addressExpressionString = watch.QueryString;
                        _dte.Debugger.Break();
                        _dte.Debugger.Go();
                    }
                    catch (Exception fail)
                    {

                    }
                }

            }
        }

        private IDebugProperty2 DebugProperty;
        private DTE2 _applicationObject;
        private AddIn _addInInstance;
        private static AutoResetEvent sync;
        private static libWatcherDialog.Threads threads;
        private static BreakPoints breakpoints;
        private static VariablesWatcher watch;
        private static Watcher.Debugger.Debugger _dbg = null;

        public static string addressExpressionString { get; set; }
    }
}