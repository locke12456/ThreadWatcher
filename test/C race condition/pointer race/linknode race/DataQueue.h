#ifndef _DATAQUEUE_
#define _DATAQUEUE_
#include <malloc.h>
#include <memory.h>
#include "shared_variable.h"
#include "threadwatcher_mem.h"
extern void InitQueue();
extern DataStream * dequque();
extern void enqueue(DataStream *);
#endif