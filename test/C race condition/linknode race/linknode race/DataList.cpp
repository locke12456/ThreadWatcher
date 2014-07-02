#include "DataList.h"
extern int * log;
void InitDatas()
{
	LinkedList * list = LinkedList_GetList();
	if(list == nullptr)
		InitLinkedPointers();
	list = LinkedList_GetList();
	Node * node = NewNode( 0 );
	LinkedList_SetHead( node );
	LinkedList_SetTail( node );
}

void AddData( int data )
{
	Node * head = LinkedList_GetHead();
	(*log)++;
	Node * node = NewNode( data );
	(*log)++;
	if(head == nullptr){
		LinkedList_SetHead( node );
	}else	
		LinkedList_AddNode( node );
	node->data = data;
}
Node * SearchData( int data )
{
	Node * head = LinkedList_GetHead();
	Node ** node = &head;
	while ( (*node)->data != data && ( (*node)->next != nullptr && (node = &(*node)->next) ) );
	return ((*node)->data == data) ? *node : nullptr ; 
}