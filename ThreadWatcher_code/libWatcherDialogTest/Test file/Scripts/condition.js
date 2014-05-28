var file = { name: "linknode.cpp" , line:10 };
var variable = addWatchponitFormAPi.BreakPointInfo(file);
variable.AddCondition("a < b && b > c");
var result = variable;