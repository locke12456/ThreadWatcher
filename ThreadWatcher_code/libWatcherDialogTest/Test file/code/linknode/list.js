var file = { name: "linknode.cpp", line: 42, type: "Node" };
var variable = addWatchponitFormAPi.BreakPointInfo(file);
variable.AddCondition("manager->Current != node");
var result = variable;