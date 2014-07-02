#include <windows.h>
#include <stdlib.h>
#include <string.h>
#include <stdio.h>
#include "DataList.h"
#include "threadwatcher_mem.h"
int * log;
void _set_datas_worker(void *);
void _read_datas_worker(void *);
void _save_socket_data(void *);
int main(int argc, char * argv[])
{
	InitDatas();
	log = (int *) malloc( sizeof(int) );
	(*log) = 0;
	(*log)++;
	_beginthread(&_set_datas_worker,0,nullptr);
	_beginthread(&_read_datas_worker,0,nullptr);
	while(true);
	return 0;
}

int socket_data()
{
	//Sleep( rand() % 10 );
	int data = rand();
	_beginthread(&_save_socket_data , 0 , &data ); 
	return 1;
}
void _save_socket_data(void * p)
{
	static int count = 0;
	count = (count+1) %10000;
	AddData( count++ );
}
void _set_datas_worker(void * p)
{
	while( true )
	{
		socket_data();
	}
}
void _read_datas_worker(void * p)
{
	while( true )
	{
		Node * list_head = LinkedList_GetHead();
		if( list_head != nullptr )
		{
			LinkedList_RemoveNode( list_head );
			free( list_head );
		}
	}
}

