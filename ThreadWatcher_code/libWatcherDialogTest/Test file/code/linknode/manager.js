var file = { filename: "linknode.cpp", line: 35, type: "List" };
var variable = addWatchponitFormAPi.BreakPointInfo(file);
variable.AddCondition("_list != nullptr");
variable.SetWatchTarget({ name: "head", type: "variable" });
var result = variable;