#ifndef _WRITEWORKER_H

#define _WRITEWORKER_H
#include <windows.h>
#include <direct.h>
#ifndef LINUX
#define GetCurrentDir _getcwd
#endif
#include <stdlib.h>
#include <stdio.h>
#include <iostream>
#include <fstream>
#include <string>
#include <io.h>
#include "DataQueue.h"
void _writer_worker(void *);

#endif