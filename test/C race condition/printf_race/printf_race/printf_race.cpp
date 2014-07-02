#include <stdio.h>
#include <stdlib.h>
#include <conio.h>
#include <process.h>
void _thread(void *);
int *share;
int main(){
	int id = 1;
	share = (int *)malloc(sizeof(int));
	*share = 0;
	_beginthread(&_thread,1,(void*)&id);
	int id2 = 2;
	_beginthread(&_thread,2,(void*)&id2);
	while(1);
	return 1;
}

void _thread(void * p)
{
	//int temp = 0;
	int id = *(int *)p;
	while ((*share)++ < 10000)
	{
		printf("%d \n",*share);
	}
	free(share);
}