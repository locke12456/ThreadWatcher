#include "DataQueue.h"
void InitQueue()
{
	queue = (Queue *) malloc( sizeof( Queue ) );
	memset( queue , 0 ,  sizeof( Queue ) );
	queue->_array = (LinkedList *) malloc( sizeof (LinkedList) );
	memset( queue->_array , 0 ,  sizeof( LinkedList ) );
}
void _pop()
{
	Queue * q = queue;
	q->front = q->_array->current;
	if(q->front != nullptr)
		q->_array->current = q->_array->current->next;
}
bool _head(DataStream * data)
{
	DataStream * bot = queue->_array->current;
	if(bot == nullptr){ 
		queue->_array->current = queue->back = data;
		return true;
	}
	return false;
}
void _push(DataStream * data)
{
	if(_head(data)) return;
	DataStream ** next = &queue->back; 
	while( *next != nullptr ) {
		next = &(*next)->next; 
	}
	*next = queue->back = data;
}
DataStream * dequque()
{
	_pop();
	DataStream * data = queue->front;
	return data;
}
void enqueue(DataStream * data )
{
	_push(data);
	queue->back->next = nullptr;

}