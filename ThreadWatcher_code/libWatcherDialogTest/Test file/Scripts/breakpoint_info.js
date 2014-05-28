var file = { name: "linknode.cpp", line: 30, code: "manager	 = (ListPointer *)malloc(sizeof(ListPointer));" };
var variable = addWatchponitFormAPi.BreakPointInfo(file);
variable.AddCondition("a < b && b > c");
var result = variable;