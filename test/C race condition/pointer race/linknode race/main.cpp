
#include <stdio.h>
#include <sys/stat.h>
#include "DataQueue.h"
#include "thread_manager.h"
#include "threadwatcher_mem.h"
int main(int argc, char * argv[])
{
	InitQueue();
	InitAllThread();
	while(true);
	return 0;
}
