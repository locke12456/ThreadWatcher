var file = { filename: "linknode.cpp", line: 47, type: "Node" };
var variable = addWatchponitFormAPi.BreakPointInfo(file);
variable.AddCondition("node->counter>0");
variable.SetWatchTarget({ name: "counter", type: "variable" });
var result = variable;