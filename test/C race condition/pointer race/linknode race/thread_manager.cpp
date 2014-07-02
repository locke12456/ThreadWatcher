#include "thread_manager.h"
int * _log ;
void DoProgress( char label[],int step, int total )
{
    //progress width
    const int pwidth = 72;

    //minus label len
    int width = pwidth - strlen( label );
    int pos = ( step * width ) / total ;

    
    int percent = ( step * 100 ) / total;

    //set green text color, only on Windows
    SetConsoleTextAttribute(  GetStdHandle( STD_OUTPUT_HANDLE ), FOREGROUND_GREEN );
    printf( "%s[", label );

    //fill progress bar with =
    for ( int i = 0; i < pos; i++ )  printf( "%c", '=' );

    //fill progress bar with spaces
    printf( "% *c", width - pos + 1, ']' );
    printf( " %3d%%\r", percent );

    //reset text color, only on Windows
    SetConsoleTextAttribute(  GetStdHandle( STD_OUTPUT_HANDLE ), 0x08 );
}
void _progress(void * p)
{
	_log = (int* )malloc(4);
	while(true)
	{
		DataStream * file = queue->front ;
		if(file != nullptr){
			char label[72];
			sprintf( label , "%s %s" , "Copying : " , file->filename );
			(*_log)++;
			DoProgress( label, file->index , file->length );
		
		}
	}
}
void InitAllThread()
{
	_beginthread( _writer_worker , 0 , nullptr );
	_beginthread( _reader_worker , 0 , nullptr );
	_beginthread( _progress		 , 0 , nullptr );
}