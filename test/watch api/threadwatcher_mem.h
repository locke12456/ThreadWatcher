#ifndef ____MEM_
#define ____MEM_
#include "____watch_alloc.h"
//#undef calloc
//#undef realloc
//#undef _recalloc
//#undef _aligned_free
//#undef _aligned_malloc
//#undef _aligned_offset_malloc
//#undef _aligned_realloc
//#undef _aligned_recalloc
//#undef _aligned_offset_realloc
//#undef _aligned_offset_recalloc
//#undef _aligned_msize
#define free ____watch_free
#define malloc ____watch_malloc
//#define new ____watch_new
#endif