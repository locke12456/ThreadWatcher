var file = { filename: "ConsoleApplication1.cpp", line: 41 , type: "int"};
var variable = addWatchponitFormAPi.BreakPointInfo(file);
variable.AddCondition("*addd!=0&&*addd>998");
var result = variable;