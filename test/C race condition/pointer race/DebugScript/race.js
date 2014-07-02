//set breakpoint information
var file_info = { filename: "DataQueue.cpp", line: 4, type: "Queue" };
var watchpoint_object = addWatchponitFormAPi.BreakPointInfo(file_info);

//setting watchpoint
watchpoint_object.AddCondition("1");
watchpoint_object.SetWatchTarget({ name: "front", type: "variable" });

//return
var result = watchpoint_object;