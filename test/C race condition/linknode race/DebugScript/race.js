﻿//set breakpoint information
var file_info = { filename: "main.cpp", line: 18, type: "Node **" };
var watchpoint_object = addWatchponitFormAPi.BreakPointInfo(file_info);

//setting watchpoint
watchpoint_object.AddCondition("1");

watchpoint_object.SetWatchTarget({ name: "", type: "variable" });
//return
var result = watchpoint_object;