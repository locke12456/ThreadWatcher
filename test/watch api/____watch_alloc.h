#ifndef _WATCHLIB_
#define _WATCHLIB_

extern bool ____watch_malloc_active;

extern bool ____watch_free_active;

extern void * __cdecl ____watch_malloc(size_t);
extern void __cdecl ____watch_free(void *);
//extern void* operator ____watch_new(size_t size);
#endif

