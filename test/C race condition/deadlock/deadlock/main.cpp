#include <windows.h>
#include <stdlib.h>
#include <string.h>
#include <stdio.h>
#include "philosopher.h"
#include <boost/thread/thread.hpp>
int * lock_log;
int main(int argc, char * argv[])
{
	boost::mutex * mutexs = new boost::mutex[5];
	lock_log = (int *)malloc(4);
	(*lock_log) = 0;
	(*lock_log)++;
	Sleep(100);
	boost::thread( &philosopher ,1 , &mutexs[0] , &mutexs[1] );  
	boost::thread( &philosopher ,2 , &mutexs[1] , &mutexs[2] );  
	boost::thread( &philosopher ,3 , &mutexs[2] , &mutexs[3] );  
	boost::thread( &philosopher ,4 , &mutexs[3] , &mutexs[4] );  
	boost::thread( &philosopher ,5 , &mutexs[4] , &mutexs[0] );  
	while(true);
	return 0;
}
