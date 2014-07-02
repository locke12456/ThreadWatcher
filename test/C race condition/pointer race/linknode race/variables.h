#ifndef _VARIABLE_H

#define _VARIABLE_H

#define DATA_MAX_SIZE 1024 * 1024 * 20 
#define NAME_SIZE 80
typedef struct DataStream{
	char _buf[DATA_MAX_SIZE] ;
	char filename[NAME_SIZE];
	unsigned int length , index;
	DataStream * next;
};

typedef struct LinkedList
{
	DataStream * current ,* head , * end;
};

typedef struct Queue
{
	LinkedList * _array;
	DataStream * front , * back;
};

#endif