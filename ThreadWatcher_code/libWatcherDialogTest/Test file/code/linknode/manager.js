var file = { name: "linknode.cpp", line: 30, type: "ListPointer" };
var variable = addWatchponitFormAPi.BreakPointInfo(file);
variable.AddCondition("manager != nullptr");
var result = variable;