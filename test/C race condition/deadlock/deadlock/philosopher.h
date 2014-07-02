#ifndef _PHILOSOPHER_
#define _PHILOSOPHER_
#include <boost\thread\mutex.hpp>
#include <boost\thread.hpp>
extern void philosopher(int id , boost::mutex * mutex_left , boost::mutex * mutex_right );

#endif